using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GeekBangCN.InfoPathAnalyzer.UC.UIDataModel;
using GeekBangCN.InfoPathAnalyzer.Model;

namespace GeekBangCN.InfoPathAnalyzer.UC.Helper
{
    public class TransferObjectBuilder
    {
        private TransferObjectBuilder()
        {
        }

        public static IList<ElementControlMapListViewData> BuildElementControlMapListViewDataFromUnboundControlTreeNode(TreeNode controlTreeNode)
        {
            List<ElementControlMapListViewData> viewDataList = new List<ElementControlMapListViewData>();
            Model.Control control = GetTagObjectFromTreeNode<Model.Control>(controlTreeNode);
            if (control == null)
            {
                return viewDataList;
            }

            ElementControlMapListViewData viewData = new ElementControlMapListViewData();
            viewData.DataSourceName = string.Empty;
            viewData.ElementXPath = string.Empty;
            viewData.ElementNameWithPrefix = string.Empty;
            viewData.ElementNamespace = string.Empty;
            viewData.ElementType = string.Empty;
            viewData.ElementDataType = string.Empty;
            viewData.ViewName = control.ParentView.DisplayName;
            viewData.ViewFileName = control.ParentView.InternalFileName;
            viewData.ControlID = control.ID;

            viewDataList.Add(viewData);

            return viewDataList;
        }

        public static IList<ElementControlMapListViewData> BuildElementControlMapListViewDataFromElementTreeNode(TreeNode elementTreeNode)
        {
            List<ElementControlMapListViewData> viewDataList = new List<ElementControlMapListViewData>();
            Element element = GetTagObjectFromTreeNode<Element>(elementTreeNode);
            if (element == null)
            {
                return viewDataList;
            }

            string dataSourceName = element.DataSource.DisplayName;
            string elementXPath = GenerateXPath(elementTreeNode);
            string elementName = element.NameWithNamespacePrefix;
            string elementNamespace = element.NamespaceUri;
            string elementType = element.Type;
            string elementDataType = element.DataType;

            // If there's no bound control, store element data and return.
            if (element.BoundControls.Count <= 0)
            {
                ElementControlMapListViewData elementControlMapListViewData = new ElementControlMapListViewData();
                elementControlMapListViewData.DataSourceName = dataSourceName;
                elementControlMapListViewData.ElementXPath = elementXPath;
                elementControlMapListViewData.ElementNameWithPrefix = elementName;
                elementControlMapListViewData.ElementNamespace = elementNamespace;
                elementControlMapListViewData.ElementType = elementType;
                elementControlMapListViewData.ElementDataType = elementDataType;

                viewDataList.Add(elementControlMapListViewData);

                return viewDataList;
            }

            foreach (Model.Control boundControl in element.BoundControls)
            {
                ElementControlMapListViewData elementControlMapListViewData = new ElementControlMapListViewData();
                elementControlMapListViewData.DataSourceName = dataSourceName;
                elementControlMapListViewData.ElementXPath = elementXPath;
                elementControlMapListViewData.ElementNameWithPrefix = elementName;
                elementControlMapListViewData.ElementNamespace = elementNamespace;
                elementControlMapListViewData.ElementType = elementType;
                elementControlMapListViewData.ElementDataType = elementDataType;

                elementControlMapListViewData.ViewName = boundControl.ParentView.DisplayName;
                elementControlMapListViewData.ViewFileName = boundControl.ParentView.InternalFileName;
                elementControlMapListViewData.ControlID = boundControl.ID;

                viewDataList.Add(elementControlMapListViewData);
            }

            return viewDataList;
        }

        public static T GetTagObjectFromTreeNode<T>(TreeNode treeNode) where T : class
        {
            if (treeNode.Tag == null)
            {
                return null;
            }

            T tagObject = treeNode.Tag as T;

            return tagObject;
        }

        private static string GenerateXPath(TreeNode treeNode)
        {
            TreeNode tempTreeNode = treeNode;
            StringBuilder sb = new StringBuilder();

            while (tempTreeNode != null)
            {
                Element element = GetTagObjectFromTreeNode<Element>(tempTreeNode);
                if (element == null)
                {
                    break;
                }

                sb = sb.Insert(0, element.NameWithNamespacePrefix).Insert(0, "/");

                tempTreeNode = tempTreeNode.Parent;
            }

            return sb.ToString();
        }
    }
}
