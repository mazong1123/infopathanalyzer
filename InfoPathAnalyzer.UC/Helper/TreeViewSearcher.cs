using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GeekBangCN.InfoPathAnalyzer.UC.Helper
{
    public class TreeViewSearcher
    {
        #region Private Fields

        private TreeView treeViewControl;
        private IList<TreeNode> currentFoundNodes = new List<TreeNode>();
        private int currentFoundNodeIndex = -1;

        #endregion

        #region Constructors

        public TreeViewSearcher(TreeView treeViewControl)
        {
            this.treeViewControl = treeViewControl;
        }

        #endregion

        #region Public Methods

        public void FindAndSelectTreeViewNode(Func<TreeNode, bool> fileterMethod)
        {
            this.ClearBackColor();

            TreeNode root = this.treeViewControl.Nodes[0];
            IList<TreeNode> foundNodes = this.FindNodeByText(root, fileterMethod);

            // Highlight the found nodes.
            this.HighlightNodes(foundNodes);
        }

        public void FindTreeViewNode(string searchKeyWord, bool ignoreCase, bool matchExact)
        {
            this.ClearBackColor();

            if (string.IsNullOrWhiteSpace(searchKeyWord))
            {
                this.currentFoundNodes = new List<TreeNode>();
                this.currentFoundNodeIndex = -1;

                return;
            }

            TreeNode root = this.treeViewControl.Nodes[0];
            IList<TreeNode> foundNodes = this.FindNodeByText(root, delegate(TreeNode currentTreeNode)
            {
                string nodeText = currentTreeNode.Text;
                if (ignoreCase)
                {
                    nodeText = nodeText.ToLowerInvariant();
                    searchKeyWord = searchKeyWord.ToLowerInvariant();
                }

                if (matchExact)
                {
                    if (nodeText.Equals(searchKeyWord))
                    {
                        return true;
                    }
                }
                else
                {
                    if (nodeText.Contains(searchKeyWord))
                    {
                        return true;
                    }
                }

                return false;
            });

            // Highlight the found nodes.
            this.HighlightNodes(foundNodes);
        }

        public void SelectNextFoundNode()
        {
            if (this.currentFoundNodes.Count <= 0)
            {
                return;
            }

            if (this.currentFoundNodeIndex + 1 >= this.currentFoundNodes.Count)
            {
                return;
            }

            TreeNode currentFoundNode = this.currentFoundNodes[++this.currentFoundNodeIndex];
            this.treeViewControl.SelectedNode = currentFoundNode;
        }

        public void SelectPrevFoundNode()
        {
            if (this.currentFoundNodes.Count <= 0)
            {
                return;
            }

            if (this.currentFoundNodeIndex - 1 < 0)
            {
                return;
            }

            TreeNode currentFoundNode = this.currentFoundNodes[--this.currentFoundNodeIndex];
            this.treeViewControl.SelectedNode = currentFoundNode;
        }

        public bool IsCurrentLastFoundNode()
        {
            return this.currentFoundNodeIndex >= this.currentFoundNodes.Count - 1;
        }

        public bool IsCurrentFirstFoundNode()
        {
            return this.currentFoundNodeIndex <= 0;
        }

        public int GetFoundNodeCount()
        {
            return this.currentFoundNodes.Count;
        }

        /// <summary>
        /// Recursively move through the treeview nodes and reset backcolors to white 
        /// </summary>
        public void ClearBackColor()
        {
            this.ClearRecursive(this.treeViewControl.Nodes[0]);
        }

        #endregion

        #region Private Methods

        private void ExpandParentNodes(TreeNode node)
        {
            while (node.Parent != null && !node.Parent.IsExpanded)
            {
                node = node.Parent;
                node.Expand();
            }
        }

        private void HighlightNodes(IList<TreeNode> treeNodeList)
        {
            for (int i = treeNodeList.Count - 1; i >= 0; i--)
            {
                TreeNode foundNode = treeNodeList[i];
                foundNode.BackColor = Color.Yellow;
                this.ExpandParentNodes(foundNode);
            }

            this.currentFoundNodes = treeNodeList;
            if (treeNodeList.Count > 0)
            {
                // Select the first found node.
                treeViewControl.SelectedNode = treeNodeList[0];
                this.currentFoundNodeIndex = 0;
            }
            else
            {
                this.currentFoundNodeIndex = -1;
            }
        }

        private IList<TreeNode> FindNodeByText(TreeNode parentNode, Func<TreeNode, bool> filterMethod)
        {
            List<TreeNode> foundNodes = new List<TreeNode>();

            if (filterMethod(parentNode))
            {
                foundNodes.Add(parentNode);
            }

            foreach (TreeNode child in parentNode.Nodes)
            {
                foundNodes.AddRange(this.FindNodeByText(child, filterMethod));
            }

            return foundNodes;
        }

        #region Remove BackColor

        /// <summary>
        /// Called by ClearBackColor function.
        /// </summary>
        /// <param name="treeNode"></param>
        private void ClearRecursive(TreeNode treeNode)
        {
            treeNode.BackColor = Color.White;
            foreach (TreeNode tn in treeNode.Nodes)
            {
                this.ClearRecursive(tn);
            }
        }

        #endregion

        #endregion
    }
}
