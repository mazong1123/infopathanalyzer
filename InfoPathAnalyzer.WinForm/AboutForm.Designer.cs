namespace GeekBangCN.InfoPathAnalyzer.WinForm
{
    partial class AboutForm
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
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblHomePage = new System.Windows.Forms.Label();
            this.lnklblHomePage = new System.Windows.Forms.LinkLabel();
            this.pbGeekBangCN = new System.Windows.Forms.PictureBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblSupport = new System.Windows.Forms.Label();
            this.lnklblSupport = new System.Windows.Forms.LinkLabel();
            this.lblSplit = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbGeekBangCN)).BeginInit();
            this.SuspendLayout();
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(108, 8);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(77, 12);
            this.lblVersion.TabIndex = 0;
            this.lblVersion.Text = "Version: 1.0";
            // 
            // lblHomePage
            // 
            this.lblHomePage.AutoSize = true;
            this.lblHomePage.Location = new System.Drawing.Point(108, 36);
            this.lblHomePage.Name = "lblHomePage";
            this.lblHomePage.Size = new System.Drawing.Size(65, 12);
            this.lblHomePage.TabIndex = 1;
            this.lblHomePage.Text = "Home Page:";
            // 
            // lnklblHomePage
            // 
            this.lnklblHomePage.AutoSize = true;
            this.lnklblHomePage.Location = new System.Drawing.Point(171, 36);
            this.lnklblHomePage.Name = "lnklblHomePage";
            this.lnklblHomePage.Size = new System.Drawing.Size(107, 12);
            this.lnklblHomePage.TabIndex = 0;
            this.lnklblHomePage.TabStop = true;
            this.lnklblHomePage.Text = "InfoPath Analyzer";
            this.lnklblHomePage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnHomePageLinkClicked);
            // 
            // pbGeekBangCN
            // 
            this.pbGeekBangCN.Image = global::GeekBangCN.InfoPathAnalyzer.WinForm.Properties.Resources.About;
            this.pbGeekBangCN.Location = new System.Drawing.Point(12, 3);
            this.pbGeekBangCN.Name = "pbGeekBangCN";
            this.pbGeekBangCN.Size = new System.Drawing.Size(90, 90);
            this.pbGeekBangCN.TabIndex = 3;
            this.pbGeekBangCN.TabStop = false;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(124, 110);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 21);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.OnOKButtonClick);
            // 
            // lblSupport
            // 
            this.lblSupport.AutoSize = true;
            this.lblSupport.Location = new System.Drawing.Point(108, 65);
            this.lblSupport.Name = "lblSupport";
            this.lblSupport.Size = new System.Drawing.Size(53, 12);
            this.lblSupport.TabIndex = 5;
            this.lblSupport.Text = "Support:";
            // 
            // lnklblSupport
            // 
            this.lnklblSupport.AutoSize = true;
            this.lnklblSupport.Location = new System.Drawing.Point(154, 65);
            this.lnklblSupport.Name = "lnklblSupport";
            this.lnklblSupport.Size = new System.Drawing.Size(137, 12);
            this.lnklblSupport.TabIndex = 1;
            this.lnklblSupport.TabStop = true;
            this.lnklblSupport.Text = "support@geekbangcn.com";
            this.lnklblSupport.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnSupportLinkClicked);
            // 
            // lblSplit
            // 
            this.lblSplit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSplit.Location = new System.Drawing.Point(12, 98);
            this.lblSplit.Name = "lblSplit";
            this.lblSplit.Size = new System.Drawing.Size(307, 2);
            this.lblSplit.TabIndex = 7;
            // 
            // AboutForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 142);
            this.Controls.Add(this.lblSplit);
            this.Controls.Add(this.lnklblSupport);
            this.Controls.Add(this.lblSupport);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.pbGeekBangCN);
            this.Controls.Add(this.lnklblHomePage);
            this.Controls.Add(this.lblHomePage);
            this.Controls.Add(this.lblVersion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About InfoPath Analyzer";
            ((System.ComponentModel.ISupportInitialize)(this.pbGeekBangCN)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblHomePage;
        private System.Windows.Forms.LinkLabel lnklblHomePage;
        private System.Windows.Forms.PictureBox pbGeekBangCN;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblSupport;
        private System.Windows.Forms.LinkLabel lnklblSupport;
        private System.Windows.Forms.Label lblSplit;
    }
}