// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainForm.cs" company="Open Cipher">
//   Open Cipher is free software distributed under GPL version 3 license
//   http://www.gnu.org/licenses/gpl-3.0.html
// </copyright>
// <summary>
//   Defines the MainForm type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OpenCipher
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Windows.Forms;

    using OpenCipher.Properties;

    /// <summary>
    /// The main form.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// The lock status, because I click too much.
        /// </summary>
        private Boolean lockStatus;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            this.InitializeComponent();

            this.lockStatus = false;

            // register the hot keys
            HotKeyManager.RegisterHotKey(Keys.E, KeyModifiers.Control | KeyModifiers.Alt);
            HotKeyManager.RegisterHotKey(Keys.D, KeyModifiers.Control | KeyModifiers.Alt);
            HotKeyManager.RegisterHotKey(Keys.S, KeyModifiers.Control | KeyModifiers.Alt);

            // add the event handler
            HotKeyManager.HotKeyPressed += this.HotKeyPressed;
        }

        /// <summary>
        /// The hot key pressed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void HotKeyPressed(object sender, HotKeyEventArgs e)
        {
            // dispatch
            switch (e.Key)
            {
                case Keys.E:
                    this.EncryptClipboard(sender, e);
                    break;
                case Keys.D:
                    this.DecryptClipboard(sender, e);
                    break;
                case Keys.S:
                    this.EncryptSelection(sender, e);
                    break;
            }
        }

        /// <summary>
        /// The lock picture click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void LockPctClick(object sender, EventArgs e)
        {
            this.lockStatus = !this.lockStatus;
            this.LockPct.Image = this.lockStatus ? Resources.LockOpen : Resources.LockClosed;
        }

        /// <summary>
        /// Settings menu click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void SettingsMenuClick(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.Hide();
            }
            else
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
            }
        }

        /// <summary>
        /// Main form resize.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void MainFormResize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
        }

        /// <summary>
        /// Notify icon mouse up.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void NotifyIconMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                MethodInfo methodInfo = typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
                methodInfo.Invoke(this.NotifyIcon, null);
            }
        }

        /// <summary>
        /// Encrypt clipboard.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void EncryptClipboard(object sender, EventArgs e)
        {
            ClipboardEncryptForm form = new ClipboardEncryptForm(CipherAction.Encrypt);
            form.ShowDialog();
        }

        /// <summary>
        /// Decrypt clipboard
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void DecryptClipboard(object sender, EventArgs e)
        {
            ClipboardEncryptForm form = new ClipboardEncryptForm(CipherAction.Decrypt);
            form.ShowDialog();
        }

        /// <summary>
        /// Encrypt selected text.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void EncryptSelection(object sender, EventArgs e)
        {
            SelectionEncryptForm form = new SelectionEncryptForm();
            form.ShowDialog();
        }

        /// <summary>
        /// The exit menu click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void ExitMenuClick(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// The help menu click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void HelpMenuClick(object sender, EventArgs e)
        {
            String target = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), Resources.HelpFile);

            try
            {
                System.Diagnostics.Process.Start(target);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            this.Hide();
        }

        /// <summary>
        /// The encrypt file menu click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void EncryptFileMenuClick(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
                                            {
                                                InitialDirectory =
                                                    Environment.SpecialFolder.Personal.ToString(),
                                                Filter = Resources.DialogFilterAllFiles,
                                                RestoreDirectory = true
                                            };

            if (fileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            FileEncryptForm form = new FileEncryptForm(fileDialog.FileName);
            form.ShowDialog();
        }

        /// <summary>
        /// The main form shown, automatically hide it to tray.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void MainFormShown(object sender, EventArgs e)
        {
            this.Hide();
        }

        /// <summary>
        /// The main form load, checks file associations
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void MainFormLoad(object sender, EventArgs e)
        {
            if (FileAssociationManager.CheckAssociations())
            {
                return;
            }

            DialogResult result = MessageBox.Show(
                "It seems that file associations are broken, do you want Open Cipher to fix it ?",
                Resources.OpenCipherTitle,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
            {
                return;
            }

            try
            {
                FileAssociationManager.FixAssociations();
            }
            catch
            {
                DialogResult retryResult = MessageBox.Show(
                    "Open Cipher can't update the registry, do you want to retry ?",
                    Resources.OpenCipherTitle,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
                if (result != DialogResult.Yes)
                {
                    return;
                }

                try
                {
                    FileAssociationManager.FixAssociations();
                }
                catch
                {
                    MessageBox.Show(
                        "File association can't be restored, skipping.",
                        Resources.OpenCipherTitle,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }
            }

            ////MessageBox.Show(
            ////    "File association has been restored.",
            ////    "Open Cipher",
            ////    MessageBoxButtons.OK,
            ////    MessageBoxIcon.Information);
        }

        /// <summary>
        /// The notify icon double click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void NotifyIconDoubleClick(object sender, EventArgs e)
        {
            this.SettingsMenuClick(sender, e);
        }
    }
}
