// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ClipboardEncryptForm.cs" company="Open Cipher">
//   Open Cipher is free software distributed under GPL version 3 license
//   http://www.gnu.org/licenses/gpl-3.0.html
// </copyright>
// <summary>
//   Defines the ClipboardEncryptForm type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OpenCipher
{
    using System;
    using System.Diagnostics;
    using System.Text;
    using System.Windows.Forms;

    using OpenCipher.Properties;

    using OpenCipher;

    /// <summary>
    /// The clipboard encrypt form.
    /// </summary>
    public sealed partial class ClipboardEncryptForm : Form
    {
        /// <summary>
        /// The action.
        /// </summary>
        private readonly CipherAction action;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClipboardEncryptForm"/> class.
        /// </summary>
        /// <param name="action">
        /// The action.
        /// </param>
        public ClipboardEncryptForm(CipherAction action)
        {
            this.InitializeComponent();

            this.action = action;

            if (this.action == CipherAction.Encrypt)
            {
                this.Text = String.Format("{0} - {1}", Resources.OpenCipherTitle, Resources.EncryptClipboardMessage);
                this.InfoLbl.Text = Resources.EncryptClipboardMessage;
                this.Icon = Resources.LockClosedIcon;
            }
            else
            {
                this.Text = String.Format("{0} - {1}", Resources.OpenCipherTitle, Resources.DecryptClipboardMessage);
                this.InfoLbl.Text = Resources.DecryptClipboardMessage;
                this.Icon = Resources.LockOpenIcon;
            }
        }

        /// <summary>
        /// The clipboard encrypt form shown.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void ClipboardEncryptFormShown(object sender, EventArgs e)
        {
            // get focus
            this.Activate();
        }

        /// <summary>
        /// The ok button click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void OkBtnClick(object sender, EventArgs e)
        {
            const String Algorithm = "aes-256-cbc";

            // key
            String key = this.KeyTxt.Text;

            // openssl process
            Process p = new Process
            {
                StartInfo =
                {
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    FileName = Resources.OpenSslBinary,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true
                }
            };

            // building the openssl arguments string
            StringBuilder argumentsBuilder = new StringBuilder();
            argumentsBuilder.AppendFormat("{0} ", Algorithm);
            argumentsBuilder.Append(this.action == CipherAction.Encrypt ? "-e " : "-d ");
            argumentsBuilder.Append("-a ");
            argumentsBuilder.Append(Settings.Default.UseKeySalt ? "-salt  " : "-nosalt ");
            argumentsBuilder.AppendFormat("-k \"{0}\" ", key);

            p.StartInfo.Arguments = argumentsBuilder.ToString();

            // execute process
            p.Start();

            // if clipboard is empty, exit
            if (!Clipboard.ContainsText(TextDataFormat.Text))
            {
                MessageBox.Show(Resources.EmptyClipboardErrorMessage, Resources.OpenCipherTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);

                // close form
                this.Close();
                return;
            }

            // get clipboard data
            String data = Clipboard.GetText(TextDataFormat.Text);

            // send data to the process
            p.StandardInput.Write(data);
            p.StandardInput.Close();

            // get output
            String processedData = p.StandardOutput.ReadToEnd();
            String errorOutputLine = p.StandardError.ReadLine();

            // show result message
            if (!String.IsNullOrWhiteSpace(errorOutputLine))
            {
                if (errorOutputLine.Equals(@"error reading input file"))
                {
                    MessageBox.Show(Resources.ClipboardContentErrorMessage, Resources.OpenCipherTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (errorOutputLine.Equals(@"bad magic number"))
                {
                    MessageBox.Show(Resources.WrongMagicNumberErrorMessage, Resources.OpenCipherTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (errorOutputLine.Equals(@"bad decrypt"))
                {
                    MessageBox.Show(Resources.WrongKeyErrorMessage, Resources.OpenCipherTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // close form
                this.Close();
                return;
            }

            // set the clipboard data
            Clipboard.SetText(processedData, TextDataFormat.Text);

            // inform user
            MessageBox.Show(
                this.action == CipherAction.Encrypt ?
                "Clipboard has been encrypted." :
                "Clipboard has been decrypted.",
                Resources.OpenCipherTitle,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            // close form
            this.Close();
        }

        /// <summary>
        /// The key text changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void KeyTxtTextChanged(object sender, EventArgs e)
        {
            // enable the button if key has been provided
            this.OkBtn.Enabled = !String.IsNullOrEmpty(this.KeyTxt.Text);
        }

        /// <summary>
        /// The key txt key down.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void KeyTxtKeyDown(object sender, KeyEventArgs e)
        {
            // close on escape
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
