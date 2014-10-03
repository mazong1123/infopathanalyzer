namespace GeekBangCN.InfoPathAnalyzer.UC.Control
{
    partial class PreviewControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.previewWebBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // previewWebBrowser
            // 
            this.previewWebBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.previewWebBrowser.Location = new System.Drawing.Point(0, 0);
            this.previewWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.previewWebBrowser.Name = "previewWebBrowser";
            this.previewWebBrowser.Size = new System.Drawing.Size(435, 364);
            this.previewWebBrowser.TabIndex = 0;
            // 
            // PreivewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.previewWebBrowser);
            this.Name = "PreivewControl";
            this.Size = new System.Drawing.Size(438, 367);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser previewWebBrowser;
    }
}
