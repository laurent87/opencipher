namespace OpenCipher
{
    sealed partial class FileEncryptForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileEncryptForm));
            this.KeyTxt = new System.Windows.Forms.TextBox();
            this.OkBtn = new System.Windows.Forms.Button();
            this.InfoLbl = new System.Windows.Forms.Label();
            this.Base64Chk = new System.Windows.Forms.CheckBox();
            this.LockPct = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.LockPct)).BeginInit();
            this.SuspendLayout();
            // 
            // KeyTxt
            // 
            resources.ApplyResources(this.KeyTxt, "KeyTxt");
            this.KeyTxt.Name = "KeyTxt";
            this.KeyTxt.UseSystemPasswordChar = true;
            this.KeyTxt.TextChanged += new System.EventHandler(this.KeyTxtTextChanged);
            this.KeyTxt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyTxtKeyDown);
            // 
            // OkBtn
            // 
            resources.ApplyResources(this.OkBtn, "OkBtn");
            this.OkBtn.Name = "OkBtn";
            this.OkBtn.UseVisualStyleBackColor = true;
            this.OkBtn.Click += new System.EventHandler(this.OkBtnClick);
            // 
            // InfoLbl
            // 
            resources.ApplyResources(this.InfoLbl, "InfoLbl");
            this.InfoLbl.Name = "InfoLbl";
            // 
            // Base64Chk
            // 
            resources.ApplyResources(this.Base64Chk, "Base64Chk");
            this.Base64Chk.Name = "Base64Chk";
            this.Base64Chk.UseVisualStyleBackColor = true;
            this.Base64Chk.CheckedChanged += new System.EventHandler(this.Base64ChkCheckedChanged);
            // 
            // LockPct
            // 
            resources.ApplyResources(this.LockPct, "LockPct");
            this.LockPct.Image = global::OpenCipher.Properties.Resources.FileLock;
            this.LockPct.Name = "LockPct";
            this.LockPct.TabStop = false;
            // 
            // FileEncryptForm
            // 
            this.AcceptButton = this.OkBtn;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LockPct);
            this.Controls.Add(this.Base64Chk);
            this.Controls.Add(this.InfoLbl);
            this.Controls.Add(this.OkBtn);
            this.Controls.Add(this.KeyTxt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FileEncryptForm";
            this.Shown += new System.EventHandler(this.FileEncryptFormShown);
            ((System.ComponentModel.ISupportInitialize)(this.LockPct)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox KeyTxt;
        private System.Windows.Forms.Button OkBtn;
        private System.Windows.Forms.Label InfoLbl;
        private System.Windows.Forms.CheckBox Base64Chk;
        private System.Windows.Forms.PictureBox LockPct;
    }
}