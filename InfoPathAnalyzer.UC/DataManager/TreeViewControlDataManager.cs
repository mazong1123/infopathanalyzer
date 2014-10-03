using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GeekBangCN.Common.Extensions;

namespace GeekBangCN.InfoPathAnalyzer.UC.DataManager
{
    public abstract class TreeViewControlDataManager : DataManagerBase
    {
        #region Private Fields

        protected Helper.TreeViewSearcher treeViewSearcher;
        protected TreeView treeViewControl;
        private readonly string rootNodeName = "Data Sources";
        private readonly string findResultTextTemplate = "{0} record(s) found.";        

        #endregion

        #region Constructors

        public TreeViewControlDataManager(string rootNodeName)
        {
            this.rootNodeName = rootNodeName;
        }

        #endregion

        #region Public Porperties

        public TreeView TreeViewControl
        {
            get
            {
                return this.treeViewControl;
            }
            set
            {
                this.treeViewControl = value;
                this.treeViewSearcher = new Helper.TreeViewSearcher(this.treeViewControl);
            }
        }

        #endregion

        #region Public Methods

        #region Virtual Methods

        public virtual void PopulateTreeViewData()
        {
            this.ValidateTreeView();
            this.AddRootToTreeView();
        }

        #endregion

        public void FindTreeViewNode(string searchKeyWord, bool ignoreCase, bool matchExact)
        {
            this.ValidateTreeView();

            this.treeViewSearcher.FindTreeViewNode(searchKeyWord, ignoreCase, matchExact);
        }

        public void SelectNextFoundNode()
        {
            this.ValidateTreeView();

            this.treeViewSearcher.SelectNextFoundNode();
        }

        public void SelectPrevFoundNode()
        {
            this.ValidateTreeView();

            this.treeViewSearcher.SelectPrevFoundNode();
        }

        public bool IsCurrentLastFoundNode()
        {
            this.ValidateTreeView();

            return this.treeViewSearcher.IsCurrentLastFoundNode();
        }

        public bool IsCurrentFirstFoundNode()
        {
            this.ValidateTreeView();

            return this.treeViewSearcher.IsCurrentFirstFoundNode();
        }

        public int GetFoundNodeCount()
        {
            this.ValidateTreeView();

            return this.treeViewSearcher.GetFoundNodeCount();
        }

        public string GenerateFoundResultText()
        {
            int resultCount = this.GetFoundNodeCount();

            return string.Format(this.findResultTextTemplate, resultCount);
        }

        public void ClearNodeSelection()
        {
            this.treeViewControl.SelectedNode = null;
        }

        public void Clear()
        {
            this.treeViewControl.Nodes.Clear();
        }

        #endregion

        #region Protected Methods

        protected void ValidateTreeView()
        {
            if (this.treeViewControl == null)
            {
                throw new MemberAccessException("FieldTreeView cannont be null.");
            }
        }

        protected TreeNode GetTreeViewRootNode()
        {
            if (this.treeViewControl.Nodes.Count <= 0)
            {
                throw new MemberAccessException("The tree node does not contain root.");
            }

            return this.treeViewControl.Nodes[0];
        }

        #endregion

        #region Private Fields

        private TreeNode AddRootToTreeView()
        {
            TreeNode root = new TreeNode(this.rootNodeName);
            if (this.treeViewControl.ImageList != null)
            {
                root.ImageIndex = 0;
                root.SelectedImageIndex = 0;
            }
            
            this.treeViewControl.Nodes.Add(root);

            return root;
        }

        #endregion
    }
}
