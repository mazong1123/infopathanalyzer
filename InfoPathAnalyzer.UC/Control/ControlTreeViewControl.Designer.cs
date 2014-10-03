namespace GeekBangCN.InfoPathAnalyzer.UC.Control
{
    partial class ControlTreeViewControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlTreeViewControl));
            this.cbMatchExact = new System.Windows.Forms.CheckBox();
            this.cbIgnoreCase = new System.Windows.Forms.CheckBox();
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.internalControlTreeView = new GeekBangCN.InfoPathAnalyzer.UC.Control.NodeFocusableTreeView();
            this.treeViewImageList = new System.Windows.Forms.ImageList(this.components);
            this.lblSearchResult = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbMatchExact
            // 
            this.cbMatchExact.AutoSize = true;
            this.cbMatchExact.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbMatchExact.Location = new System.Drawing.Point(99, 50);
            this.cbMatchExact.Name = "cbMatchExact";
            this.cbMatchExact.Size = new System.Drawing.Size(86, 17);
            this.cbMatchExact.TabIndex = 11;
            this.cbMatchExact.Text = "Exact Match";
            this.cbMatchExact.UseVisualStyleBackColor = true;
            // 
            // cbIgnoreCase
            // 
            this.cbIgnoreCase.AutoSize = true;
            this.cbIgnoreCase.Checked = true;
            this.cbIgnoreCase.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIgnoreCase.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbIgnoreCase.Location = new System.Drawing.Point(4, 50);
            this.cbIgnoreCase.Name = "cbIgnoreCase";
            this.cbIgnoreCase.Size = new System.Drawing.Size(83, 17);
            this.cbIgnoreCase.TabIndex = 10;
            this.cbIgnoreCase.Text = "Ignore Case";
            this.cbIgnoreCase.UseVisualStyleBackColor = true;
            // 
            // tbSearch
            // 
            this.tbSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSearch.Location = new System.Drawing.Point(4, 21);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(251, 20);
            this.tbSearch.TabIndex = 8;
            // 
            // internalControlTreeView
            // 
            this.internalControlTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.internalControlTreeView.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.internalControlTreeView.HideSelection = false;
            this.internalControlTreeView.ImageIndex = 0;
            this.internalControlTreeView.ImageList = this.treeViewImageList;
            this.internalControlTreeView.Location = new System.Drawing.Point(3, 112);
            this.internalControlTreeView.Name = "internalControlTreeView";
            this.internalControlTreeView.SelectedImageIndex = 0;
            this.internalControlTreeView.Size = new System.Drawing.Size(352, 334);
            this.internalControlTreeView.TabIndex = 7;
            this.internalControlTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.OnInternalControlTreeViewAfterSelect);
            // 
            // treeViewImageList
            // 
            this.treeViewImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("treeViewImageList.ImageStream")));
            this.treeViewImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.treeViewImageList.Images.SetKeyName(0, "ControlTreeViewRoot.png");
            this.treeViewImageList.Images.SetKeyName(1, "View.ico");
            this.treeViewImageList.Images.SetKeyName(2, "Control.png");
            // 
            // lblSearchResult
            // 
            this.lblSearchResult.AutoSize = true;
            this.lblSearchResult.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSearchResult.Location = new System.Drawing.Point(189, 87);
            this.lblSearchResult.Name = "lblSearchResult";
            this.lblSearchResult.Size = new System.Drawing.Size(0, 13);
            this.lblSearchResult.TabIndex = 14;
            // 
            // btnNext
            // 
            this.btnNext.Enabled = false;
            this.btnNext.Image = global::GeekBangCN.InfoPathAnalyzer.UC.Properties.Resources.Next;
            this.btnNext.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNext.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnNext.Location = new System.Drawing.Point(99, 75);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 25);
            this.btnNext.TabIndex = 13;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.OnNextButtonClick);
            // 
            // btnPrev
            // 
            this.btnPrev.Enabled = false;
            this.btnPrev.Image = global::GeekBangCN.InfoPathAnalyzer.UC.Properties.Resources.Prev;
            this.btnPrev.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrev.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPrev.Location = new System.Drawing.Point(4, 75);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(75, 25);
            this.btnPrev.TabIndex = 12;
            this.btnPrev.Text = "Prev";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.OnPrevButtonClick);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Enabled = false;
            this.btnSearch.Image = global::GeekBangCN.InfoPathAnalyzer.UC.Properties.Resources.Search;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSearch.Location = new System.Drawing.Point(277, 21);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 25);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Text = "Search";
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.OnSearchButtonClick);
            // 
            // ControlTreeViewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblSearchResult);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.cbMatchExact);
            this.Controls.Add(this.cbIgnoreCase);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.tbSearch);
            this.Controls.Add(this.internalControlTreeView);
            this.Name = "ControlTreeViewControl";
            this.Size = new System.Drawing.Size(355, 450);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.CheckBox cbMatchExact;
        private System.Windows.Forms.CheckBox cbIgnoreCase;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox tbSearch;
        private NodeFocusableTreeView internalControlTreeView;
        private System.Windows.Forms.Label lblSearchResult;
        private System.Windows.Forms.ImageList treeViewImageList;
    }
}
