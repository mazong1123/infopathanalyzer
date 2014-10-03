namespace GeekBangCN.InfoPathAnalyzer.UC.Control.HtmlEditor
{
    partial class BodyEditor
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
            this.tbBodyEditor = new BodyTextBox();
            this.SuspendLayout();
            // 
            // tbBodyEditor
            // 
            this.tbBodyEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBodyEditor.Location = new System.Drawing.Point(0, 0);
            this.tbBodyEditor.MaxLength = 2147483647;
            this.tbBodyEditor.Multiline = true;
            this.tbBodyEditor.Name = "tbBodyEditor";
            this.tbBodyEditor.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbBodyEditor.Size = new System.Drawing.Size(440, 123);
            this.tbBodyEditor.TabIndex = 0;
            // 
            // BodyEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbBodyEditor);
            this.Name = "BodyEditor";
            this.Size = new System.Drawing.Size(443, 138);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BodyTextBox tbBodyEditor;
    }
}
