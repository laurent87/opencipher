namespace OpenCipher
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.NotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.IconContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.EncryptFileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.SeparatorMenu = new System.Windows.Forms.ToolStripSeparator();
            this.EncryptClipboardMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.DecryptClipboardMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.Separator2Menu = new System.Windows.Forms.ToolStripSeparator();
            this.HelpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.OkBtn = new System.Windows.Forms.Button();
            this.InfoTxt = new System.Windows.Forms.TextBox();
            this.LockPct = new System.Windows.Forms.PictureBox();
            this.BackPct = new System.Windows.Forms.PictureBox();
            this.IconContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LockPct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BackPct)).BeginInit();
            this.SuspendLayout();
            // 
            // NotifyIcon
            // 
            resources.ApplyResources(this.NotifyIcon, "NotifyIcon");
            this.NotifyIcon.ContextMenuStrip = this.IconContextMenu;
            this.NotifyIcon.DoubleClick += new System.EventHandler(this.NotifyIconDoubleClick);
            this.NotifyIcon.MouseUp += new System.Windows.Forms.MouseEventHandler(this.NotifyIconMouseUp);
            // 
            // IconContextMenu
            // 
            resources.ApplyResources(this.IconContextMenu, "IconContextMenu");
            this.IconContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EncryptFileMenu,
            this.SeparatorMenu,
            this.EncryptClipboardMenu,
            this.DecryptClipboardMenu,
            this.Separator2Menu,
            this.HelpMenu,
            this.AboutMenu,
            this.ExitMenu});
            this.IconContextMenu.Name = "contextMenuStrip1";
            // 
            // EncryptFileMenu
            // 
            resources.ApplyResources(this.EncryptFileMenu, "EncryptFileMenu");
            this.EncryptFileMenu.Image = global::OpenCipher.Properties.Resources.FileLock;
            this.EncryptFileMenu.Name = "EncryptFileMenu";
            this.EncryptFileMenu.Click += new System.EventHandler(this.EncryptFileMenuClick);
            // 
            // SeparatorMenu
            // 
            resources.ApplyResources(this.SeparatorMenu, "SeparatorMenu");
            this.SeparatorMenu.Name = "SeparatorMenu";
            // 
            // EncryptClipboardMenu
            // 
            resources.ApplyResources(this.EncryptClipboardMenu, "EncryptClipboardMenu");
            this.EncryptClipboardMenu.Image = global::OpenCipher.Properties.Resources.ClipboardLock;
            this.EncryptClipboardMenu.Name = "EncryptClipboardMenu";
            this.EncryptClipboardMenu.Click += new System.EventHandler(this.EncryptClipboard);
            // 
            // DecryptClipboardMenu
            // 
            resources.ApplyResources(this.DecryptClipboardMenu, "DecryptClipboardMenu");
            this.DecryptClipboardMenu.Image = global::OpenCipher.Properties.Resources.Clipboard;
            this.DecryptClipboardMenu.Name = "DecryptClipboardMenu";
            this.DecryptClipboardMenu.Click += new System.EventHandler(this.DecryptClipboard);
            // 
            // Separator2Menu
            // 
            resources.ApplyResources(this.Separator2Menu, "Separator2Menu");
            this.Separator2Menu.Name = "Separator2Menu";
            // 
            // HelpMenu
            // 
            resources.ApplyResources(this.HelpMenu, "HelpMenu");
            this.HelpMenu.Name = "HelpMenu";
            this.HelpMenu.Click += new System.EventHandler(this.HelpMenuClick);
            // 
            // AboutMenu
            // 
            resources.ApplyResources(this.AboutMenu, "AboutMenu");
            this.AboutMenu.Name = "AboutMenu";
            this.AboutMenu.Click += new System.EventHandler(this.SettingsMenuClick);
            // 
            // ExitMenu
            // 
            resources.ApplyResources(this.ExitMenu, "ExitMenu");
            this.ExitMenu.Name = "ExitMenu";
            this.ExitMenu.Click += new System.EventHandler(this.ExitMenuClick);
            // 
            // OkBtn
            // 
            resources.ApplyResources(this.OkBtn, "OkBtn");
            this.OkBtn.Name = "OkBtn";
            this.OkBtn.UseVisualStyleBackColor = true;
            this.OkBtn.Click += new System.EventHandler(this.OkBtnClick);
            // 
            // InfoTxt
            // 
            resources.ApplyResources(this.InfoTxt, "InfoTxt");
            this.InfoTxt.BackColor = System.Drawing.SystemColors.Window;
            this.InfoTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.InfoTxt.Name = "InfoTxt";
            this.InfoTxt.ReadOnly = true;
            this.InfoTxt.TabStop = false;
            // 
            // LockPct
            // 
            resources.ApplyResources(this.LockPct, "LockPct");
            this.LockPct.BackColor = System.Drawing.SystemColors.Window;
            this.LockPct.Image = global::OpenCipher.Properties.Resources.LockClosed;
            this.LockPct.Name = "LockPct";
            this.LockPct.TabStop = false;
            this.LockPct.Click += new System.EventHandler(this.LockPctClick);
            // 
            // BackPct
            // 
            resources.ApplyResources(this.BackPct, "BackPct");
            this.BackPct.BackColor = System.Drawing.SystemColors.Window;
            this.BackPct.Name = "BackPct";
            this.BackPct.TabStop = false;
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LockPct);
            this.Controls.Add(this.OkBtn);
            this.Controls.Add(this.InfoTxt);
            this.Controls.Add(this.BackPct);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.Shown += new System.EventHandler(this.MainFormShown);
            this.Resize += new System.EventHandler(this.MainFormResize);
            this.IconContextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LockPct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BackPct)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon NotifyIcon;
        private System.Windows.Forms.ContextMenuStrip IconContextMenu;
        private System.Windows.Forms.ToolStripMenuItem EncryptClipboardMenu;
        private System.Windows.Forms.ToolStripMenuItem DecryptClipboardMenu;
        private System.Windows.Forms.ToolStripMenuItem AboutMenu;
        private System.Windows.Forms.ToolStripSeparator Separator2Menu;
        private System.Windows.Forms.ToolStripMenuItem ExitMenu;
        private System.Windows.Forms.Button OkBtn;
        private System.Windows.Forms.TextBox InfoTxt;
        private System.Windows.Forms.PictureBox LockPct;
        private System.Windows.Forms.PictureBox BackPct;
        private System.Windows.Forms.ToolStripMenuItem HelpMenu;
        private System.Windows.Forms.ToolStripMenuItem EncryptFileMenu;
        private System.Windows.Forms.ToolStripSeparator SeparatorMenu;
    }
}

