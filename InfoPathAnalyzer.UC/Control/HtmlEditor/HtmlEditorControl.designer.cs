namespace GeekBangCN.InfoPathAnalyzer.UC.Control.HtmlEditor
{
    partial class HtmlEditorControl
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
            this.ddlInfoPathWindow = new System.Windows.Forms.ComboBox();
            this.compatibilityMenu = new System.Windows.Forms.MenuStrip();
            this.compatibilityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoPath2010ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoPath2007ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblCurrentCompatibility = new System.Windows.Forms.Label();
            this.tbBodyHtml = new GeekBangCN.InfoPathAnalyzer.UC.Control.HtmlEditor.BodyEditor();
            this.btnRefreshIPWindowDropdownList = new System.Windows.Forms.Button();
            this.btnLoadIPWindow = new System.Windows.Forms.Button();
            this.btnSaveBodyHtml = new System.Windows.Forms.Button();
            this.compatibilityMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // ddlInfoPathWindow
            // 
            this.ddlInfoPathWindow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ddlInfoPathWindow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlInfoPathWindow.FormattingEnabled = true;
            this.ddlInfoPathWindow.Location = new System.Drawing.Point(6, 54);
            this.ddlInfoPathWindow.Name = "ddlInfoPathWindow";
            this.ddlInfoPathWindow.Size = new System.Drawing.Size(324, 21);
            this.ddlInfoPathWindow.TabIndex = 2;
            // 
            // compatibilityMenu
            // 
            this.compatibilityMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.compatibilityToolStripMenuItem});
            this.compatibilityMenu.Location = new System.Drawing.Point(0, 0);
            this.compatibilityMenu.Name = "compatibilityMenu";
            this.compatibilityMenu.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.compatibilityMenu.Size = new System.Drawing.Size(504, 24);
            this.compatibilityMenu.TabIndex = 6;
            this.compatibilityMenu.Text = "menuStrip1";
            // 
            // compatibilityToolStripMenuItem
            // 
            this.compatibilityToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.infoPath2010ToolStripMenuItem,
            this.infoPath2007ToolStripMenuItem});
            this.compatibilityToolStripMenuItem.Name = "compatibilityToolStripMenuItem";
            this.compatibilityToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.compatibilityToolStripMenuItem.Text = "Compatibility";
            // 
            // infoPath2010ToolStripMenuItem
            // 
            this.infoPath2010ToolStripMenuItem.Checked = true;
            this.infoPath2010ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.infoPath2010ToolStripMenuItem.Name = "infoPath2010ToolStripMenuItem";
            this.infoPath2010ToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.infoPath2010ToolStripMenuItem.Text = "InfoPath 2010";
            this.infoPath2010ToolStripMenuItem.Click += new System.EventHandler(this.OnInfoPath2010ToolStripMenuItemClick);
            // 
            // infoPath2007ToolStripMenuItem
            // 
            this.infoPath2007ToolStripMenuItem.Name = "infoPath2007ToolStripMenuItem";
            this.infoPath2007ToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.infoPath2007ToolStripMenuItem.Text = "InfoPath 2007";
            this.infoPath2007ToolStripMenuItem.Click += new System.EventHandler(this.OnInfoPath2007ToolStripMenuItemClick);
            // 
            // lblCurrentCompatibility
            // 
            this.lblCurrentCompatibility.AutoSize = true;
            this.lblCurrentCompatibility.Location = new System.Drawing.Point(3, 32);
            this.lblCurrentCompatibility.Name = "lblCurrentCompatibility";
            this.lblCurrentCompatibility.Size = new System.Drawing.Size(175, 13);
            this.lblCurrentCompatibility.TabIndex = 7;
            this.lblCurrentCompatibility.Text = "Current Compatibility: InfoPath 2010";
            // 
            // tbBodyHtml
            // 
            this.tbBodyHtml.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBodyHtml.EditorText = "";
            this.tbBodyHtml.Location = new System.Drawing.Point(5, 84);
            this.tbBodyHtml.Name = "tbBodyHtml";
            this.tbBodyHtml.Size = new System.Drawing.Size(487, 231);
            this.tbBodyHtml.TabIndex = 5;
            // 
            // btnRefreshIPWindowDropdownList
            // 
            this.btnRefreshIPWindowDropdownList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshIPWindowDropdownList.Image = global::GeekBangCN.InfoPathAnalyzer.UC.Properties.Resources.Refresh;
            this.btnRefreshIPWindowDropdownList.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRefreshIPWindowDropdownList.Location = new System.Drawing.Point(336, 55);
            this.btnRefreshIPWindowDropdownList.Name = "btnRefreshIPWindowDropdownList";
            this.btnRefreshIPWindowDropdownList.Size = new System.Drawing.Size(71, 23);
            this.btnRefreshIPWindowDropdownList.TabIndex = 4;
            this.btnRefreshIPWindowDropdownList.Text = "Refresh";
            this.btnRefreshIPWindowDropdownList.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRefreshIPWindowDropdownList.UseVisualStyleBackColor = true;
            this.btnRefreshIPWindowDropdownList.Click += new System.EventHandler(this.OnRefreshIPWindowDropdownListButtonClick);
            // 
            // btnLoadIPWindow
            // 
            this.btnLoadIPWindow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadIPWindow.Image = global::GeekBangCN.InfoPathAnalyzer.UC.Properties.Resources.Load;
            this.btnLoadIPWindow.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLoadIPWindow.Location = new System.Drawing.Point(423, 55);
            this.btnLoadIPWindow.Name = "btnLoadIPWindow";
            this.btnLoadIPWindow.Size = new System.Drawing.Size(69, 23);
            this.btnLoadIPWindow.TabIndex = 3;
            this.btnLoadIPWindow.Text = "Load";
            this.btnLoadIPWindow.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLoadIPWindow.UseVisualStyleBackColor = true;
            this.btnLoadIPWindow.Click += new System.EventHandler(this.OnLoadIPWindowButtonClick);
            // 
            // btnSaveBodyHtml
            // 
            this.btnSaveBodyHtml.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveBodyHtml.Image = global::GeekBangCN.InfoPathAnalyzer.UC.Properties.Resources.Save;
            this.btnSaveBodyHtml.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveBodyHtml.Location = new System.Drawing.Point(5, 321);
            this.btnSaveBodyHtml.Name = "btnSaveBodyHtml";
            this.btnSaveBodyHtml.Size = new System.Drawing.Size(75, 23);
            this.btnSaveBodyHtml.TabIndex = 1;
            this.btnSaveBodyHtml.Text = "Save";
            this.btnSaveBodyHtml.UseVisualStyleBackColor = true;
            this.btnSaveBodyHtml.Click += new System.EventHandler(this.OnSaveBodyHtmlButtonClick);
            // 
            // HtmlEditorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblCurrentCompatibility);
            this.Controls.Add(this.tbBodyHtml);
            this.Controls.Add(this.btnRefreshIPWindowDropdownList);
            this.Controls.Add(this.btnLoadIPWindow);
            this.Controls.Add(this.ddlInfoPathWindow);
            this.Controls.Add(this.btnSaveBodyHtml);
            this.Controls.Add(this.compatibilityMenu);
            this.Name = "HtmlEditorControl";
            this.Size = new System.Drawing.Size(504, 353);
            this.compatibilityMenu.ResumeLayout(false);
            this.compatibilityMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSaveBodyHtml;
        private System.Windows.Forms.ComboBox ddlInfoPathWindow;
        private System.Windows.Forms.Button btnLoadIPWindow;
        private System.Windows.Forms.Button btnRefreshIPWindowDropdownList;
        private BodyEditor tbBodyHtml;
        private System.Windows.Forms.MenuStrip compatibilityMenu;
        private System.Windows.Forms.ToolStripMenuItem compatibilityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infoPath2007ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infoPath2010ToolStripMenuItem;
        private System.Windows.Forms.Label lblCurrentCompatibility;
    }
}

