using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GeekBangCN.InfoPathAnalyzer.IBLL;
using GeekBangCN.InfoPathAnalyzer.Model;
using GeekBangCN.InfoPathAnalyzer.UC.Helper;

namespace GeekBangCN.InfoPathAnalyzer.UC.DataManager
{
    public class ControlTreeViewControlDataManager : TreeViewControlDataManager
    {
        private Dictionary<Model.Control, TreeNode> cachedControlToTreeNode = new Dictionary<Model.Control, TreeNode>();

        public ControlTreeViewControlDataManager()
            :base("Views")
        {
        }

        public IViewBLL ViewBLL
        {
            get;
            set;
        }

        public IControlBLL ControlBLL
        {
            get;
            set;
        }

        public override void PopulateTreeViewData()
        {
            base.PopulateTreeViewData();

            this.ValidateViewBLL();
            this.ValidateControlBLL();

            TreeNode root = this.GetTreeViewRootNode();

            // Get all views and construct the view tree.
            IList<Model.View> viewList = this.ViewBLL.GetViews(true);
            foreach (Model.View view in viewList)
            {
                TreeNode viewTreeNode = this.CreateViewNode(view);
                root.Nodes.Add(viewTreeNode);
            }
        }

        public void GetInnerControlAssociatedElement(TreeNode treeNode)
        {
            this.ValidateControlBLL();

            Model.Control innerControl = Helper.TransferObjectBuilder.GetTagObjectFromTreeNode<Model.Control>(treeNode);
            if (innerControl != null)
            {
                if (!innerControl.IsAlreadyGotAssociatedElement)
                {
                    this.ControlBLL.GetAssociatedElement(innerControl);
                }
            }
        }

        public void FindTreeViewNode(Model.Control control)
        {
            this.ValidateTreeView();

            this.treeViewSearcher.ClearBackColor();

            // Find from the cache first.
            foreach (Model.Control key in this.cachedControlToTreeNode.Keys)
            {
                if (key.ID.Equals(control.ID, StringComparison.InvariantCultureIgnoreCase)
                    && key.ParentView.InternalFileName.Equals(control.ParentView.InternalFileName, StringComparison.InvariantCultureIgnoreCase))
                {
                    TreeNode foundNode = this.cachedControlToTreeNode[key];
                    this.treeViewControl.SelectedNode = foundNode;

                    return;
                }
            }

            this.treeViewSearcher.FindAndSelectTreeViewNode(delegate(TreeNode currentTreeNode)
            {
                Model.Control innerControl = TransferObjectBuilder.GetTagObjectFromTreeNode<Model.Control>(currentTreeNode);
                if (innerControl != null)
                {
                    if (innerControl.ParentView.InternalFileName.Equals(control.ParentView.InternalFileName, StringComparison.InvariantCultureIgnoreCase)
                        && innerControl.ID.Equals(control.ID, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return true;
                    }
                }

                return false;
            });
        }

        private TreeNode CreateViewNode(Model.View view)
        {
            // Get all controls in the view.
            this.ViewBLL.GetControls(view);

            TreeNode viewTreeNode = new TreeNode();
            viewTreeNode.Name = view.DisplayName;
            viewTreeNode.Text = string.Format("{0} ({1})", view.DisplayName, view.InternalFileName);
            viewTreeNode.Tag = view;
            viewTreeNode.ImageIndex = 1;
            viewTreeNode.SelectedImageIndex = 1;

            this.ConstructControlTree(viewTreeNode, view.Controls);

            return viewTreeNode;
        }

        private void ConstructControlTree(TreeNode parentNode, IList<Model.Control> childControls)
        {
            foreach (Model.Control childControl in childControls)
            {
                TreeNode subNode = new TreeNode();
                subNode.Name = childControl.ID;
                subNode.Text = childControl.ID;
                subNode.Tag = childControl;
                subNode.ImageIndex = 2;
                subNode.SelectedImageIndex = 2;

                parentNode.Nodes.Add(subNode);
            }
        }

        private void ValidateControlBLL()
        {
            if (this.ControlBLL == null)
            {
                throw new MemberAccessException("ControlBLL cannot be null.");
            }
        }

        private void ValidateViewBLL()
        {
            if (this.ViewBLL == null)
            {
                throw new MemberAccessException("ViewBLL cannot be null.");
            }
        }
    }
}
