// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SelectionEncryptForm.cs" company="Open Cipher">
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
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Windows.Forms;

    using OpenCipher.Properties;

    /// <summary>
    /// The clipboard encrypt form.
    /// </summary>
    public sealed partial class SelectionEncryptForm : Form
    {
        /// <summary>
        /// The action.
        /// </summary>
        private CipherAction action;

        /// <summary>
        /// The data.
        /// </summary>
        private String data;

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectionEncryptForm"/> class.
        /// </summary>
        public SelectionEncryptForm()
        {
            this.InitializeComponent();

            this.action = CipherAction.Encrypt;
            
            this.UpdateForm();
        }

        /// <summary>
        /// The selection encrypt form load.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void SelectionEncryptFormLoad(object sender, EventArgs e)
        {
            // sleep to wait the hot keys to be released
            Thread.Sleep(250);

            // copy selected text
            SendKeys.SendWait("^(c)");

            // if clipboard is empty, exit
            if (!Clipboard.ContainsText(TextDataFormat.Text))
            {
                MessageBox.Show(Resources.EmptyClipboardErrorMessage, Resources.OpenCipherTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);

                // close form
                this.Close();
                return;
            }

            // get clipboard data
            this.data = Clipboard.GetText(TextDataFormat.Text);

            // check if encrypted data
            Match match = Regex.Match(this.data, @"^\s*U2Fsd", RegexOptions.None);

            if (match.Success)
            {
                this.DecryptRad.Checked = true;
            }
            else
            {
                this.EncryptRad.Checked = true;
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

            try
            {
                // execute process
                p.Start();
            }
            catch
            {
                MessageBox.Show(@"Can't execute the encryption process.", Resources.ErrorTitle, MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }

            // send data to the process
            p.StandardInput.Write(this.data);
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

            // hide the form before pasting data
            this.Hide();

            // sleep before pasting to give some time to the window to get focus
            Thread.Sleep(100);

            // paste
            SendKeys.SendWait("^(v)");

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

        /// <summary>
        /// The update form.
        /// </summary>
        private void UpdateForm()
        {
            if (this.EncryptRad.Checked)
            {
                this.action = CipherAction.Encrypt;
                this.Text = String.Format("{0} - {1}", Resources.OpenCipherTitle, Resources.EncryptSelectionMessage);
                this.InfoLbl.Text = Resources.EncryptSelectionMessage;
                this.Icon = Resources.LockClosedIcon;
                this.LockPct.Image = Resources.LockClosed;
            }
            else
            {
                this.action = CipherAction.Decrypt;
                this.Text = String.Format("{0} - {1}", Resources.OpenCipherTitle, Resources.DecryptSelectionMessage);
                this.InfoLbl.Text = Resources.DecryptSelectionMessage;
                this.Icon = Resources.LockOpenIcon;
                this.LockPct.Image = Resources.LockOpen;
            }
        }

        /// <summary>
        /// The radio buttons checked changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void RadioButtonsCheckedChanged(object sender, EventArgs e)
        {
            this.UpdateForm();
        }
    }
}
