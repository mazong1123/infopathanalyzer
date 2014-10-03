using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Schema;
using GeekBangCN.InfoPathAnalyzer.DAL;
using GeekBangCN.InfoPathAnalyzer.IBLL;
using GeekBangCN.InfoPathAnalyzer.IDAL;
using GeekBangCN.InfoPathAnalyzer.Model;
using GeekBangCN.InfoPathAnalyzer.Utility;
using GeekBangCN.InfoPathAnalyzer.Utility.View;

namespace GeekBangCN.InfoPathAnalyzer.BLL
{
    public class ElementBLL : BLLBaseWithDAL<IViewDAL>, IElementBLL
    {
        private Dictionary<string, IDictionary<string, string>> cachedViewControlToElementDictionary = new Dictionary<string, IDictionary<string,string>>();

        public ElementBLL()
        {
        }

        public ElementBLL(Stream xsnFileContents)
        {
            this.DataAccessLayer = new ViewDAL(xsnFileContents);
        }

        public ElementBLL(string xsnFilePath)
        {
            this.DataAccessLayer = new ViewDAL(xsnFilePath);
        }

        public void GetChildElements(Element element)
        {
            if (element.DataSource.Content == null)
            {
                throw new ArgumentException("The element data source content cannot be null.");
            }

            element.Children.Clear();
            element.IsAlreadyGotChildElements = false;

            // Create a xsd parser to help us resolve the data source content.
            XsdParser xsdParser = XsdParserFactory.CreateInfoPathXsdParser(element.DataSource.Content, ((DALBase)this.DataAccessLayer).XsnFileStream);

            // Get the real element in the xsd file.
            XmlSchemaObject parentElementObject = xsdParser.GetSingleItemByName(element.Name, element.NamespaceUri);

            // If the parent object is an element, find all the children.
            // Otherwise, the parent object is an attribute, it won't have children.
            if (XsdParser.IsXmlObjectElement(parentElementObject))
            {
                // Find the real element if it is ref element.
                XmlSchemaObject realParentElementObject = xsdParser.FindRealItem(parentElementObject);

                // Get all children of the element.
                List<XmlSchemaObject> xmlChildObjects = xsdParser.GetChildElementsAndAttributes((XmlSchemaElement)realParentElementObject);
                if ((xmlChildObjects == null) || (xmlChildObjects.Count == 0))
                {
                    element.IsAlreadyGotChildElements = true;

                    return;
                }

                foreach (XmlSchemaObject xmlSchemaObject in xmlChildObjects)
                {
                    Element childElement = ModelObjectBuilder.CreateElementFromXmlSchemaObjectAndDataSource(xmlSchemaObject, element.DataSource, ((DALBase)this.DataAccessLayer).XsnFileStream);
                    if (childElement != null)
                    {
                        element.Children.Add(childElement);
                        if (element.Type.Equals("Field", StringComparison.InvariantCultureIgnoreCase))
                        {
                            element.Type = "Group"; // The element has child, it is a group.
                        }
                    }
                }

                element.IsAlreadyGotChildElements = true;
            }
        }

        public void GetBoundControls(Element element)
        {
            this.ValidateDataAccessLayer();

            element.BoundControls.Clear();
            element.IsAreadyGotBoundControls = false;

            IList<View> views = this.DataAccessLayer.GetViews(true);

            using (ViewManager viewManager = new ViewManager())
            {
                foreach (View view in views)
                {
                    viewManager.ViewFileContent = view.Content;
                    IDictionary<string, string> controlToElementDict = this.GetControlToElementDictionary(view.InternalFileName, viewManager);

                    // Get the element bound control ID in current view.
                    IList<string> boundControlIDs = ViewManager.GetElementBoundControlIDs(element.NameWithNamespacePrefix, controlToElementDict);

                    // Create Control objects and add them to the Element bound controls.
                    foreach (string boundControlID in boundControlIDs)
                    {
                        Control control = ModelObjectBuilder.CreateControl(boundControlID, view);
                        element.BoundControls.Add(control);
                    }
                }
            }

            element.IsAreadyGotBoundControls = true;
        }

        private IDictionary<string, string> GetControlToElementDictionary(string viewInternalFileName, ViewManager viewManager)
        {
            if (this.cachedViewControlToElementDictionary.ContainsKey(viewInternalFileName))
            {
                return this.cachedViewControlToElementDictionary[viewInternalFileName];
            }

            IDictionary<string, string> controlToElementDict = viewManager.GenerateControlToElementDictionary();
            this.cachedViewControlToElementDictionary.Add(viewInternalFileName, controlToElementDict);

            return controlToElementDict;
        }
    }
}
