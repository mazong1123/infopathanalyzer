namespace GeekBangCN.InfoPathAnalyzer.UC.Control
{
    partial class ElementControlMapControl
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
            this.components = new System.ComponentModel.Container();
            this.elementControlMapListView = new System.Windows.Forms.ListView();
            this.ElementControlMapListViewContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyTheWholeRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ElementControlMapListViewContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // elementControlMapListView
            // 
            this.elementControlMapListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.elementControlMapListView.HideSelection = false;
            this.elementControlMapListView.Location = new System.Drawing.Point(0, 0);
            this.elementControlMapListView.MultiSelect = false;
            this.elementControlMapListView.Name = "elementControlMapListView";
            this.elementControlMapListView.Size = new System.Drawing.Size(598, 149);
            this.elementControlMapListView.TabIndex = 0;
            this.elementControlMapListView.UseCompatibleStateImageBehavior = false;
            this.elementControlMapListView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnListViewMouseClick);
            // 
            // ElementControlMapListViewContextMenu
            // 
            this.ElementControlMapListViewContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.copyTheWholeRowToolStripMenuItem});
            this.ElementControlMapListViewContextMenu.Name = "ElementControlMapListViewContextMenu";
            this.ElementControlMapListViewContextMenu.Size = new System.Drawing.Size(193, 48);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.OnListViewCopyMenuItemClick);
            // 
            // copyTheWholeRowToolStripMenuItem
            // 
            this.copyTheWholeRowToolStripMenuItem.Name = "copyTheWholeRowToolStripMenuItem";
            this.copyTheWholeRowToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.copyTheWholeRowToolStripMenuItem.Text = "Copy the whole row";
            this.copyTheWholeRowToolStripMenuItem.Click += new System.EventHandler(this.OnListViewCopyTheWholeRowMenuItemClick);
            // 
            // ElementControlMapControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.elementControlMapListView);
            this.Name = "ElementControlMapControl";
            this.Size = new System.Drawing.Size(601, 152);
            this.ElementControlMapListViewContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView elementControlMapListView;
        private System.Windows.Forms.ContextMenuStrip ElementControlMapListViewContextMenu;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyTheWholeRowToolStripMenuItem;
    }
}
