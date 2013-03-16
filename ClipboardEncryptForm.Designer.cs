namespace OpenCipher
{
    sealed partial class ClipboardEncryptForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClipboardEncryptForm));
            this.LockPct = new System.Windows.Forms.PictureBox();
            this.InfoLbl = new System.Windows.Forms.Label();
            this.OkBtn = new System.Windows.Forms.Button();
            this.KeyTxt = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.LockPct)).BeginInit();
            this.SuspendLayout();
            // 
            // LockPct
            // 
            resources.ApplyResources(this.LockPct, "LockPct");
            this.LockPct.Image = global::OpenCipher.Properties.Resources.ClipboardLock;
            this.LockPct.Name = "LockPct";
            this.LockPct.TabStop = false;
            // 
            // InfoLbl
            // 
            resources.ApplyResources(this.InfoLbl, "InfoLbl");
            this.InfoLbl.Name = "InfoLbl";
            // 
            // OkBtn
            // 
            resources.ApplyResources(this.OkBtn, "OkBtn");
            this.OkBtn.Name = "OkBtn";
            this.OkBtn.UseVisualStyleBackColor = true;
            this.OkBtn.Click += new System.EventHandler(this.OkBtnClick);
            // 
            // KeyTxt
            // 
            resources.ApplyResources(this.KeyTxt, "KeyTxt");
            this.KeyTxt.Name = "KeyTxt";
            this.KeyTxt.UseSystemPasswordChar = true;
            this.KeyTxt.TextChanged += new System.EventHandler(this.KeyTxtTextChanged);
            this.KeyTxt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyTxtKeyDown);
            // 
            // ClipboardEncryptForm
            // 
            this.AcceptButton = this.OkBtn;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LockPct);
            this.Controls.Add(this.InfoLbl);
            this.Controls.Add(this.OkBtn);
            this.Controls.Add(this.KeyTxt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ClipboardEncryptForm";
            this.Shown += new System.EventHandler(this.ClipboardEncryptFormShown);
            ((System.ComponentModel.ISupportInitialize)(this.LockPct)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox LockPct;
        private System.Windows.Forms.Label InfoLbl;
        private System.Windows.Forms.Button OkBtn;
        private System.Windows.Forms.TextBox KeyTxt;

    }
}