using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using GeekBangCN.InfoPathAnalyzer.DAL;
using GeekBangCN.InfoPathAnalyzer.IBLL;
using GeekBangCN.InfoPathAnalyzer.IDAL;
using GeekBangCN.InfoPathAnalyzer.Model;
using GeekBangCN.InfoPathAnalyzer.Utility;
using GeekBangCN.InfoPathAnalyzer.Utility.View;

namespace GeekBangCN.InfoPathAnalyzer.BLL
{
    public class ControlBLL : BLLBaseWithDAL<IViewDAL>, IControlBLL
    {
        private IDataSourceBLL dataSourceBLL = null;
        private IElementBLL elementBLL = null;
        private IList<DataSource> cachedDataSources;

        #region Constructors

        public ControlBLL()
        {
        }

        public ControlBLL(Stream xsnFileContents)
        {
            this.DataAccessLayer = new ViewDAL(xsnFileContents);
            this.dataSourceBLL = new DataSourceBLL(xsnFileContents);
            this.elementBLL = new ElementBLL(xsnFileContents);
        }

        public ControlBLL(string xsnFilePath)
        {
            this.DataAccessLayer = new ViewDAL(xsnFilePath);
            this.dataSourceBLL = new DataSourceBLL(xsnFilePath);
            this.elementBLL = new ElementBLL(xsnFilePath);
        }

        #endregion

        #region Public Methods

        public void GetAssociatedElement(Control control)
        {
            this.ValidateDataAccessLayer();

            View view = control.ParentView;
            if (view == null)
            {
                throw new ArgumentException("The control's parent view is null.");
            }

            if (view.Content == null)
            {
                throw new ArgumentException("The control's parent view contents are empty.");
            }

            control.AssociatedElement = null;
            control.IsAlreadyGotAssociatedElement = false;

            // Find control element in view.
            XDocument viewDoc = XDocument.Load(view.Content);
            XNamespace xdNamespace = LinqHelper.GetNamespace(viewDoc, "xd");
            XElement controlElement = LinqHelper.FindSpecificElementInAllElements(viewDoc.Root,
                delegate(XElement element)
                {
                    XAttribute ctrlIDAttribute = element.Attribute(xdNamespace + "CtrlId");
                    if (ctrlIDAttribute == null)
                    {
                        return false;
                    }

                    if (ctrlIDAttribute.Value.Equals(control.ID, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return true;
                    }

                    return false;
                });

            if (controlElement != null)
            {
                Element associatedElement = this.GetAssociatedElementByControlNode(controlElement, xdNamespace);
                control.AssociatedElement = associatedElement;
            }

            control.IsAlreadyGotAssociatedElement = true;
        }

        #endregion

        #region Private Methods

        private Element GetAssociatedElementByControlNode(XElement controlNode, XNamespace xdNamespace)
        {
            string bindingElementName = string.Empty;
            using (ViewManager viewManager = new ViewManager())
            {
                bindingElementName = viewManager.GetBindingValueFromElement(controlNode, xdNamespace);
            }

            if (string.IsNullOrWhiteSpace(bindingElementName) || bindingElementName.Equals("\"\"", StringComparison.InvariantCultureIgnoreCase))
            {
                return null;
            }

            bindingElementName = bindingElementName.Substring(bindingElementName.LastIndexOf("/") + 1);

            Element foundElement = null;

            // Get the data sources.
            if (this.cachedDataSources == null)
            {
                this.cachedDataSources = this.dataSourceBLL.GetDataSources(true);
            }

            // Find the specific element in all elements.
            foreach (DataSource ds in this.cachedDataSources)
            {
                if (!ds.IsAlreadyGotElements)
                {
                    this.dataSourceBLL.GetElements(ds);
                }
                
                foreach (Element element in ds.Elements)
                {
                    foundElement = this.FindElementByNameWithPrefix(element, bindingElementName);
                    if (foundElement != null)
                    {
                        return foundElement;
                    }
                }
            }

            return foundElement;
        }

        private Element FindElementByNameWithPrefix(Element parent, string nameWithPrefix)
        {
            if (parent.NameWithNamespacePrefix.Equals(nameWithPrefix, StringComparison.InvariantCultureIgnoreCase))
            {
                return parent;
            }

            if (!parent.IsAlreadyGotChildElements)
            {
                this.elementBLL.GetChildElements(parent);
            }

            Element foundElement = null;
            foreach (Element child in parent.Children)
            {
                foundElement = this.FindElementByNameWithPrefix(child, nameWithPrefix);
                if (foundElement != null)
                {
                    return foundElement;
                }
            }

            return null;
        }

        #endregion
    }
}
