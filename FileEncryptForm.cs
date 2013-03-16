// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileEncryptForm.cs" company="Open Cipher">
//   Open Cipher is free software distributed under GPL version 3 license
//   http://www.gnu.org/licenses/gpl-3.0.html
// </copyright>
// <summary>
//   The key form.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Text.RegularExpressions;

namespace OpenCipher
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;

    using OpenCipher.Properties;

    /// <summary>
    /// The key form.
    /// </summary>
    public sealed partial class FileEncryptForm : Form
    {
        /// <summary>
        /// The action.
        /// </summary>
        private readonly CipherAction action;

        /// <summary>
        /// The input file.
        /// </summary>
        private readonly String inputFile;

        /// <summary>
        /// The output file.
        /// </summary>
        private String outputFile;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileEncryptForm"/> class.
        /// </summary>
        /// <param name="inputFile">
        /// The input file.
        /// </param>
        public FileEncryptForm(String inputFile)
        {
            this.InitializeComponent();

            this.inputFile = inputFile;
            String fileName = Path.GetFileName(this.inputFile);
            String inputFileExtension = Path.GetExtension(this.inputFile);

            if (inputFileExtension == null
                || (!inputFileExtension.Equals(Resources.AesFileExtension, StringComparison.InvariantCultureIgnoreCase)
                && !inputFileExtension.Equals(Resources.AscFileExtension, StringComparison.InvariantCultureIgnoreCase)))
            {
                this.action = CipherAction.Encrypt;

                this.outputFile = String.Format("{0}{1}", this.inputFile, Resources.AesFileExtension);
                this.Text = String.Format("{0} - {1}", Resources.OpenCipherTitle, Resources.EncryptFileTitle);
                this.InfoLbl.Text = String.Format(Resources.EncryptFileMessage, fileName);
                this.Icon = Resources.LockClosedIcon;
            }
            else
            {
                this.action = CipherAction.Decrypt;

                // detect if file is base64 encoded based on file extension
                this.Base64Chk.Checked = inputFileExtension.Equals(Resources.AscFileExtension, StringComparison.InvariantCultureIgnoreCase);

                this.outputFile = Path.Combine(Path.GetDirectoryName(this.inputFile), Path.GetFileNameWithoutExtension(this.inputFile));
                this.Text = String.Format("{0} - {1}", Resources.OpenCipherTitle, Resources.DecryptFileTitle);
                this.InfoLbl.Text = String.Format(Resources.DecryptFileMessage, fileName);
                this.Icon = Resources.LockOpenIcon;
            }

            this.UpdatePicture();
        }

        /// <summary>
        /// The file encrypt form shown.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void FileEncryptFormShown(object sender, EventArgs e)
        {
            // get focus
            this.Activate();
        }

        /// <summary>
        /// The ok button click event.
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

            // convert to base 64
            Boolean base64 = this.Base64Chk.Checked;

            // key
            String key = this.KeyTxt.Text;

            // output file name
            if (this.action == CipherAction.Encrypt)
            {
                this.outputFile = String.Format(
                    "{0}{1}",
                    this.inputFile,
                    base64 ? Resources.AscFileExtension : Resources.AesFileExtension);
            }

            // openssl process
            Process p = new Process
                            {
                                StartInfo =
                                    {
                                        UseShellExecute = false,
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
            argumentsBuilder.Append(base64 ? "-a " : String.Empty);
            argumentsBuilder.Append(Settings.Default.UseKeySalt ? "-salt  " : "-nosalt ");
            argumentsBuilder.AppendFormat("-k \"{0}\" ", key);
            argumentsBuilder.AppendFormat("-in \"{0}\" -out \"{1}\"", this.inputFile, this.outputFile);

            p.StartInfo.Arguments = argumentsBuilder.ToString();

            // execute process
            p.Start();

            // get output
            String output = p.StandardOutput.ReadToEnd();
            String errorOutputLine = p.StandardError.ReadLine();

            // skip OpenSSL warning
            if (errorOutputLine != null && Regex.Match(errorOutputLine, @"^WARNING", RegexOptions.IgnoreCase).Success)
            {
                // read next line
                errorOutputLine = p.StandardError.ReadLine();
            }

            // show result message
            if (!String.IsNullOrWhiteSpace(errorOutputLine))
            {
                if (errorOutputLine.Equals(@"bad magic number"))
                {
                    MessageBox.Show(Resources.WrongMagicNumberErrorMessage, Resources.OpenCipherTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (errorOutputLine.Equals(@"bad decrypt"))
                {
                    MessageBox.Show(Resources.WrongKeyErrorMessage, Resources.OpenCipherTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(errorOutputLine, Resources.OpenCipherTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (!String.IsNullOrWhiteSpace(output))
            {
                MessageBox.Show(output, Resources.OpenCipherTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (this.action == CipherAction.Encrypt)
                {
                    MessageBox.Show(
                        base64
                            ? String.Format(Resources.FileEncryptedToUsingBase64Message, this.outputFile)
                            : String.Format(Resources.FileEncryptedToMessage, this.outputFile),
                        Resources.OpenCipherTitle,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(String.Format(Resources.FileDecryptedToMessage, this.outputFile), Resources.OpenCipherTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

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
        /// The base 64 chk checked changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Base64ChkCheckedChanged(object sender, EventArgs e)
        {
            this.UpdatePicture();
        }

        /// <summary>
        /// The update picture.
        /// </summary>
        private void UpdatePicture()
        {
            this.LockPct.Image = this.action == CipherAction.Encrypt ? (this.Base64Chk.Checked ? Resources.FileTxtLock : Resources.FileLock) : (this.Base64Chk.Checked ? Resources.FileTxtUnlock : Resources.FileUnlock);
        }
    }
}
