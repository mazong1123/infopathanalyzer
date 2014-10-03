using System;
using System.Collections;   
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GeekBangCN.InfoPathAnalyzer.IBLL;
using GeekBangCN.InfoPathAnalyzer.Model;
using GeekBangCN.InfoPathAnalyzer.UC.Helper;

namespace GeekBangCN.InfoPathAnalyzer.UC.DataManager
{
    public class FieldTreeViewControlDataManager : TreeViewControlDataManager
    {
        private Dictionary<string, TreeNode> cachedElementNameWithPrefixToTreeNode = new Dictionary<string,TreeNode>();

        public FieldTreeViewControlDataManager()
            : base("Data Sources")
        {
        }

        public IDataSourceBLL DataSourceBLL
        {
            get;
            set;
        }

        public IElementBLL ElementBLL
        {
            get;
            set;
        }

        public override void PopulateTreeViewData()
        {
            base.PopulateTreeViewData();

            this.ValidateDataSourceBLL();
            this.ValidateElementBLL();

            TreeNode root = this.GetTreeViewRootNode();

            // Get all data sources and construct the data source tree.
            IList<DataSource> dataSourceList = this.DataSourceBLL.GetDataSources(true);
            foreach (DataSource dataSource in dataSourceList)
            {
                TreeNode dataSourceTreeNode = this.CreateDataSourceNode(dataSource);
                root.Nodes.Add(dataSourceTreeNode);
            }
        }

        public void FindTreeViewNode(Model.Element element)
        {
            this.ValidateTreeView();

            this.treeViewSearcher.ClearBackColor();

            // Find from the cache first.
            if (this.cachedElementNameWithPrefixToTreeNode.Keys.Contains(element.NameWithNamespacePrefix))
            {
                TreeNode foundNode = this.cachedElementNameWithPrefixToTreeNode[element.NameWithNamespacePrefix];
                this.treeViewControl.SelectedNode = foundNode;

                return;
            }

            this.treeViewSearcher.FindAndSelectTreeViewNode(delegate(TreeNode currentTreeNode)
            {
                Model.Element innerElement = TransferObjectBuilder.GetTagObjectFromTreeNode<Model.Element>(currentTreeNode);
                if (innerElement != null)
                {
                    if (innerElement.NameWithNamespacePrefix.Equals(element.NameWithNamespacePrefix, StringComparison.InvariantCultureIgnoreCase))
                    {
                        // Cache current pair.
                        if (!this.cachedElementNameWithPrefixToTreeNode.ContainsKey(innerElement.NameWithNamespacePrefix))
                        {
                            this.cachedElementNameWithPrefixToTreeNode.Add(innerElement.NameWithNamespacePrefix, currentTreeNode);
                        }

                        return true;
                    }
                }

                return false;
            });
        }

        public void GetInnerElementBoundControl(TreeNode treeNode)
        {
            this.ValidateElementBLL();

            Element innerElement = Helper.TransferObjectBuilder.GetTagObjectFromTreeNode<Element>(treeNode);
            if (innerElement != null)
            {
                if (!innerElement.IsAreadyGotBoundControls)
                {
                    this.ElementBLL.GetBoundControls(innerElement);
                }
            }
        }

        private TreeNode CreateDataSourceNode(DataSource dataSource)
        {
            // Get all elements of the data source.
            this.DataSourceBLL.GetElements(dataSource);

            // Create a data source tree node.
            TreeNode dataSourceTreeNode = new TreeNode();
            dataSourceTreeNode.Name = dataSource.InternalFileName;
            dataSourceTreeNode.Text = string.Format("{0} ({1})", dataSource.DisplayName, dataSource.InternalFileName);
            dataSourceTreeNode.Tag = dataSource;
            if (!dataSource.IsMainDataSource)
            {
                dataSourceTreeNode.ImageIndex = 1;
                dataSourceTreeNode.SelectedImageIndex = 1;
            }
            else
            {
                dataSourceTreeNode.ImageIndex = 2;
                dataSourceTreeNode.SelectedImageIndex = 2;
            }

            this.ConstructFieldTree(dataSourceTreeNode, dataSource.Elements);

            return dataSourceTreeNode;
        }

        private void ConstructFieldTree(TreeNode parentNode, IList<Element> childElements)
        {
            foreach (Element childElement in childElements)
            {
                // Get the children of current element.
                this.ElementBLL.GetChildElements(childElement);

                TreeNode subNode = new TreeNode();
                subNode.Name = childElement.Name;
                subNode.Text = childElement.Name;
                subNode.Tag = childElement;
                if (childElement.Type.Equals("Field", StringComparison.InvariantCultureIgnoreCase))
                {
                    subNode.ImageIndex = 4;
                    subNode.SelectedImageIndex = 4;
                }
                else
                {
                    subNode.ImageIndex = 3;
                    subNode.SelectedImageIndex = 3;
                }

                parentNode.Nodes.Add(subNode);

                // Construst the sub tree.
                this.ConstructFieldTree(subNode, childElement.Children);
            }
        }

        private void ValidateDataSourceBLL()
        {
            if (this.DataSourceBLL == null)
            {
                throw new MemberAccessException("DataSourceBLL cannot be null.");
            }
        }

        private void ValidateElementBLL()
        {
            if (this.ElementBLL == null)
            {
                throw new MemberAccessException("ElementBLL cannot be null.");
            }
        }
    }
}
