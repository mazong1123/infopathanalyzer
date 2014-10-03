namespace GeekBangCN.InfoPathAnalyzer.WinForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tbcFieldControlTreeView = new System.Windows.Forms.TabControl();
            this.tbpFieldTreeView = new System.Windows.Forms.TabPage();
            this.fieldTreeViewControl = new GeekBangCN.InfoPathAnalyzer.UC.Control.FieldTreeViewControl();
            this.tbpControlTreeView = new System.Windows.Forms.TabPage();
            this.controlTreeViewControl = new GeekBangCN.InfoPathAnalyzer.UC.Control.ControlTreeViewControl();
            this.gbPreview = new System.Windows.Forms.GroupBox();
            this.previewControl = new GeekBangCN.InfoPathAnalyzer.UC.Control.PreviewControl();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileMainMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openMainMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitMainMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpDocumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buyBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbcViewer = new System.Windows.Forms.TabControl();
            this.tbpViewer = new System.Windows.Forms.TabPage();
            this.elementControlMapControl = new GeekBangCN.InfoPathAnalyzer.UC.Control.ElementControlMapControl();
            this.tbpEditor = new System.Windows.Forms.TabPage();
            this.htmlEditorControl = new GeekBangCN.InfoPathAnalyzer.UC.Control.HtmlEditor.HtmlEditorControl();
            this.tbcFieldControlTreeView.SuspendLayout();
            this.tbpFieldTreeView.SuspendLayout();
            this.tbpControlTreeView.SuspendLayout();
            this.gbPreview.SuspendLayout();
            this.mainMenu.SuspendLayout();
            this.tbcViewer.SuspendLayout();
            this.tbpViewer.SuspendLayout();
            this.tbpEditor.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbcFieldControlTreeView
            // 
            this.tbcFieldControlTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.tbcFieldControlTreeView.Controls.Add(this.tbpFieldTreeView);
            this.tbcFieldControlTreeView.Controls.Add(this.tbpControlTreeView);
            this.tbcFieldControlTreeView.Location = new System.Drawing.Point(6, 6);
            this.tbcFieldControlTreeView.Name = "tbcFieldControlTreeView";
            this.tbcFieldControlTreeView.SelectedIndex = 0;
            this.tbcFieldControlTreeView.Size = new System.Drawing.Size(330, 274);
            this.tbcFieldControlTreeView.TabIndex = 7;
            // 
            // tbpFieldTreeView
            // 
            this.tbpFieldTreeView.Controls.Add(this.fieldTreeViewControl);
            this.tbpFieldTreeView.Location = new System.Drawing.Point(4, 22);
            this.tbpFieldTreeView.Name = "tbpFieldTreeView";
            this.tbpFieldTreeView.Padding = new System.Windows.Forms.Padding(3);
            this.tbpFieldTreeView.Size = new System.Drawing.Size(322, 248);
            this.tbpFieldTreeView.TabIndex = 0;
            this.tbpFieldTreeView.Text = "Field";
            this.tbpFieldTreeView.UseVisualStyleBackColor = true;
            // 
            // fieldTreeViewControl
            // 
            this.fieldTreeViewControl.AcceptButton = null;
            this.fieldTreeViewControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.fieldTreeViewControl.DataManager = null;
            this.fieldTreeViewControl.Location = new System.Drawing.Point(6, 7);
            this.fieldTreeViewControl.Name = "fieldTreeViewControl";
            this.fieldTreeViewControl.Size = new System.Drawing.Size(307, 237);
            this.fieldTreeViewControl.TabIndex = 6;
            // 
            // tbpControlTreeView
            // 
            this.tbpControlTreeView.Controls.Add(this.controlTreeViewControl);
            this.tbpControlTreeView.Location = new System.Drawing.Point(4, 22);
            this.tbpControlTreeView.Name = "tbpControlTreeView";
            this.tbpControlTreeView.Padding = new System.Windows.Forms.Padding(3);
            this.tbpControlTreeView.Size = new System.Drawing.Size(322, 248);
            this.tbpControlTreeView.TabIndex = 1;
            this.tbpControlTreeView.Text = "Control";
            this.tbpControlTreeView.UseVisualStyleBackColor = true;
            // 
            // controlTreeViewControl
            // 
            this.controlTreeViewControl.AcceptButton = null;
            this.controlTreeViewControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.controlTreeViewControl.DataManager = null;
            this.controlTreeViewControl.Location = new System.Drawing.Point(6, 7);
            this.controlTreeViewControl.Name = "controlTreeViewControl";
            this.controlTreeViewControl.Size = new System.Drawing.Size(307, 237);
            this.controlTreeViewControl.TabIndex = 0;
            // 
            // gbPreview
            // 
            this.gbPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbPreview.Controls.Add(this.previewControl);
            this.gbPreview.Location = new System.Drawing.Point(342, 6);
            this.gbPreview.Name = "gbPreview";
            this.gbPreview.Size = new System.Drawing.Size(393, 274);
            this.gbPreview.TabIndex = 8;
            this.gbPreview.TabStop = false;
            this.gbPreview.Text = "Preview";
            // 
            // previewControl
            // 
            this.previewControl.AcceptButton = null;
            this.previewControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.previewControl.DataManager = null;
            this.previewControl.Location = new System.Drawing.Point(6, 20);
            this.previewControl.Name = "previewControl";
            this.previewControl.Size = new System.Drawing.Size(381, 248);
            this.previewControl.TabIndex = 1;
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMainMenuToolStripMenuItem,
            this.helpHToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(773, 25);
            this.mainMenu.TabIndex = 9;
            this.mainMenu.Text = "Main Menu";
            // 
            // fileMainMenuToolStripMenuItem
            // 
            this.fileMainMenuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openMainMenuToolStripMenuItem,
            this.exitMainMenuToolStripMenuItem});
            this.fileMainMenuToolStripMenuItem.Name = "fileMainMenuToolStripMenuItem";
            this.fileMainMenuToolStripMenuItem.Size = new System.Drawing.Size(57, 21);
            this.fileMainMenuToolStripMenuItem.Text = "File (&F)";
            // 
            // openMainMenuToolStripMenuItem
            // 
            this.openMainMenuToolStripMenuItem.Image = global::GeekBangCN.InfoPathAnalyzer.WinForm.Properties.Resources.Open;
            this.openMainMenuToolStripMenuItem.Name = "openMainMenuToolStripMenuItem";
            this.openMainMenuToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.openMainMenuToolStripMenuItem.Text = "Open (&O)...";
            this.openMainMenuToolStripMenuItem.Click += new System.EventHandler(this.OnOpenMainMenuToolStripMenuItemClick);
            // 
            // exitMainMenuToolStripMenuItem
            // 
            this.exitMainMenuToolStripMenuItem.Name = "exitMainMenuToolStripMenuItem";
            this.exitMainMenuToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.exitMainMenuToolStripMenuItem.Text = "Exit (&E)";
            this.exitMainMenuToolStripMenuItem.Click += new System.EventHandler(this.OnExitMainMenuToolStripMenuItemClick);
            // 
            // helpHToolStripMenuItem
            // 
            this.helpHToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpDocumentToolStripMenuItem,
            this.buyBToolStripMenuItem,
            this.aboutAToolStripMenuItem});
            this.helpHToolStripMenuItem.Name = "helpHToolStripMenuItem";
            this.helpHToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.helpHToolStripMenuItem.Text = "Help (&H)";
            // 
            // helpDocumentToolStripMenuItem
            // 
            this.helpDocumentToolStripMenuItem.Image = global::GeekBangCN.InfoPathAnalyzer.WinForm.Properties.Resources.Help;
            this.helpDocumentToolStripMenuItem.Name = "helpDocumentToolStripMenuItem";
            this.helpDocumentToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.helpDocumentToolStripMenuItem.Text = "Help Document (&H)";
            this.helpDocumentToolStripMenuItem.Click += new System.EventHandler(this.OnHelpDocumentToolStripMenuItemClick);
            // 
            // buyBToolStripMenuItem
            // 
            this.buyBToolStripMenuItem.Name = "buyBToolStripMenuItem";
            this.buyBToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.buyBToolStripMenuItem.Text = "Buy (&B)";
            this.buyBToolStripMenuItem.Click += new System.EventHandler(this.OnBuyToolStripMenuItemClick);
            // 
            // aboutAToolStripMenuItem
            // 
            this.aboutAToolStripMenuItem.Image = global::GeekBangCN.InfoPathAnalyzer.WinForm.Properties.Resources.About1;
            this.aboutAToolStripMenuItem.Name = "aboutAToolStripMenuItem";
            this.aboutAToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.aboutAToolStripMenuItem.Text = "About (&A)";
            this.aboutAToolStripMenuItem.Click += new System.EventHandler(this.OnAboutToolStripMenuItemClick);
            // 
            // tbcViewer
            // 
            this.tbcViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbcViewer.Controls.Add(this.tbpViewer);
            this.tbcViewer.Controls.Add(this.tbpEditor);
            this.tbcViewer.Location = new System.Drawing.Point(12, 25);
            this.tbcViewer.Name = "tbcViewer";
            this.tbcViewer.SelectedIndex = 0;
            this.tbcViewer.Size = new System.Drawing.Size(749, 443);
            this.tbcViewer.TabIndex = 10;
            // 
            // tbpViewer
            // 
            this.tbpViewer.Controls.Add(this.gbPreview);
            this.tbpViewer.Controls.Add(this.elementControlMapControl);
            this.tbpViewer.Controls.Add(this.tbcFieldControlTreeView);
            this.tbpViewer.Location = new System.Drawing.Point(4, 22);
            this.tbpViewer.Name = "tbpViewer";
            this.tbpViewer.Padding = new System.Windows.Forms.Padding(3);
            this.tbpViewer.Size = new System.Drawing.Size(741, 417);
            this.tbpViewer.TabIndex = 0;
            this.tbpViewer.Text = "Viewer";
            this.tbpViewer.UseVisualStyleBackColor = true;
            // 
            // elementControlMapControl
            // 
            this.elementControlMapControl.AcceptButton = null;
            this.elementControlMapControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.elementControlMapControl.DataManager = null;
            this.elementControlMapControl.Location = new System.Drawing.Point(6, 291);
            this.elementControlMapControl.Name = "elementControlMapControl";
            this.elementControlMapControl.Size = new System.Drawing.Size(729, 123);
            this.elementControlMapControl.TabIndex = 0;
            // 
            // tbpEditor
            // 
            this.tbpEditor.Controls.Add(this.htmlEditorControl);
            this.tbpEditor.Location = new System.Drawing.Point(4, 22);
            this.tbpEditor.Name = "tbpEditor";
            this.tbpEditor.Padding = new System.Windows.Forms.Padding(3);
            this.tbpEditor.Size = new System.Drawing.Size(741, 417);
            this.tbpEditor.TabIndex = 1;
            this.tbpEditor.Text = "Editor";
            this.tbpEditor.UseVisualStyleBackColor = true;
            // 
            // htmlEditorControl
            // 
            this.htmlEditorControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.htmlEditorControl.Location = new System.Drawing.Point(6, 6);
            this.htmlEditorControl.Name = "htmlEditorControl";
            this.htmlEditorControl.Size = new System.Drawing.Size(729, 405);
            this.htmlEditorControl.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(773, 479);
            this.Controls.Add(this.tbcViewer);
            this.Controls.Add(this.mainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InfoPath Analyzer";
            this.tbcFieldControlTreeView.ResumeLayout(false);
            this.tbpFieldTreeView.ResumeLayout(false);
            this.tbpControlTreeView.ResumeLayout(false);
            this.gbPreview.ResumeLayout(false);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.tbcViewer.ResumeLayout(false);
            this.tbpViewer.ResumeLayout(false);
            this.tbpEditor.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UC.Control.ElementControlMapControl elementControlMapControl;
        private UC.Control.PreviewControl previewControl;
        private UC.Control.FieldTreeViewControl fieldTreeViewControl;
        private System.Windows.Forms.TabControl tbcFieldControlTreeView;
        private System.Windows.Forms.TabPage tbpFieldTreeView;
        private System.Windows.Forms.TabPage tbpControlTreeView;
        private System.Windows.Forms.GroupBox gbPreview;
        private UC.Control.ControlTreeViewControl controlTreeViewControl;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileMainMenuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openMainMenuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitMainMenuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpHToolStripMenuItem;
        private System.Windows.Forms.TabControl tbcViewer;
        private System.Windows.Forms.TabPage tbpViewer;
        private System.Windows.Forms.TabPage tbpEditor;
        private System.Windows.Forms.ToolStripMenuItem helpDocumentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buyBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutAToolStripMenuItem;
        private UC.Control.HtmlEditor.HtmlEditorControl htmlEditorControl;

    }
}