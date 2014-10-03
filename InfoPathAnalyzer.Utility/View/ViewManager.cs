using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Collections;

namespace GeekBangCN.InfoPathAnalyzer.Utility.View
{
    public class ViewManager : IDisposable
    {
        #region Private Fields

        private Stream viewFileContent;
        private IList<BindingValueAndControlFinder> bindingValueAndControlFinders;

        #endregion

        #region Constructors

        public ViewManager()
        {
            this.GenerateDefaultFinders();
        }

        public ViewManager(Stream viewFileContent)
        {
            this.viewFileContent = viewFileContent;
            this.GenerateDefaultFinders();
        }

        public ViewManager(IList<BindingValueAndControlFinder> finders)
        {
            this.bindingValueAndControlFinders = finders;
        }

        public ViewManager(Stream viewFileContent, IList<BindingValueAndControlFinder> finders)
        {
            this.viewFileContent = viewFileContent;
            this.bindingValueAndControlFinders = finders;
        }

        #endregion

        #region Public Properties

        public Stream ViewFileContent
        {
            get
            {
                if (this.viewFileContent == null)
                {
                    return null;
                }

                MemoryStream ms = new MemoryStream();
                viewFileContent.CopyTo(ms);
                viewFileContent.Position = 0;
                ms.Position = 0;

                return ms;
            }
            set
            {
                this.viewFileContent = value;
            }
        }

        public IList<BindingValueAndControlFinder> BindingValueAndControlFinders
        {
            get
            {
                return this.bindingValueAndControlFinders;
            }
        }

        #endregion

        #region Public Methods

        public static IList<string> GetElementBoundControlIDs(string elementNameWithPrefix, IDictionary<string, string> controlToElementDictionary)
        {
            List<string> boundControlIDs = new List<string>();

            // Some elements bind to control like this: 'xd:binding="../my:Customer/my:CustomerNameOther"'.
            // So we need to extract the string after the last '/' to compare to the element name with prefix.
            var query = from c in controlToElementDictionary
                        where (c.Value == elementNameWithPrefix) || (c.Value.Substring(c.Value.LastIndexOf("/") + 1) == elementNameWithPrefix)
                        select c;

            if ((query == null) || (query.Count() == 0))
            {
                return boundControlIDs;
            }

            foreach (KeyValuePair<string, string> dictionary in query)
            {
                boundControlIDs.Add(dictionary.Key);
            }

            return boundControlIDs;
        }

        public string GetBindingValueFromElement(XElement element, XNamespace xdNamespace)
        {
            string bindingValue = string.Empty;

            if (this.bindingValueAndControlFinders == null)
            {
                this.GenerateDefaultFinders();
            }

            foreach (BindingValueAndControlFinder finder in this.bindingValueAndControlFinders)
            {
                finder.ControlElement = element;
                finder.AddNamespace("xd", xdNamespace);
                if (finder.IsValidControlElement())
                {
                    bindingValue = finder.GetBindingValue();

                    break;
                }
            }

            return bindingValue;
        }

        public static bool IsElementExpressionBoxWithoutBindingAttribute(XElement element, XNamespace xdNamespace)
        {
            BindingValueAndControlFinder finder = new ExpressionBoxWithoutBindingAttributeControlBindingValueAndControlFinder(element, xdNamespace);

            return finder.IsValidControlElement();
        }

        public static string GetExpressionBoxWithoutBindingAttributeBindingValue(XElement element, XNamespace xdNamespace)
        {
            BindingValueAndControlFinder finder = new ExpressionBoxWithoutBindingAttributeControlBindingValueAndControlFinder();
            finder.ControlElement = element;
            finder.AddNamespace("xd", xdNamespace);

            string bindingValue = string.Empty;
            if (finder.IsValidControlElement())
            {
                bindingValue = finder.GetBindingValue();
            }

            return bindingValue;
        }

        public static string GetSimpleControlBindingValue(XElement element, XNamespace xdNamespace)
        {
            string bindingValue = element.Attribute(xdNamespace + "binding").Value;

            // Need to find the actual binding value.
            if (bindingValue.Equals(".", StringComparison.InvariantCultureIgnoreCase))
            {
                bindingValue = FindRealBindingValueFromRelatedBindingValue(element);
            }

            return bindingValue;
        }

        public static string GetRepeatingTableBindingValue(XElement element)
        {
            string selectFieldXPath = string.Empty;

            XElement forEachElement = LinqHelper.FindSpecificElementInAllElements(element, delegate(XElement currentElement)
            {
                if (currentElement.Name.LocalName.Equals("for-each", StringComparison.InvariantCultureIgnoreCase)
                    && currentElement.Attribute("select") != null)
                {
                    return true;
                }

                return false;
            });

            if (forEachElement == null)
            {
                return selectFieldXPath;
            }

            selectFieldXPath = forEachElement.Attribute("select").Value;

            // Need to find the actual binding value.
            if (selectFieldXPath.Equals(".", StringComparison.InvariantCultureIgnoreCase))
            {
                selectFieldXPath = FindRealBindingValueFromRelatedBindingValue(element);
            }

            return selectFieldXPath;
        }

        public static string GetSectionBindingValue(XElement element)
        {
            string bindingValue = element.Attribute("match").Value;
            if (bindingValue.Equals(".", StringComparison.InvariantCultureIgnoreCase))
            {
                bindingValue = FindRealBindingValueFromRelatedBindingValue(element);
            }

            return bindingValue;
        }

        public static string FindRealBindingValueFromRelatedBindingValue(XElement element)
        {
            string bindingValue = ".";

            XElement tempElement = element;
            while (tempElement.Parent != null)
            {
                tempElement = tempElement.Parent;
                if (tempElement.Name.LocalName.Equals("template", StringComparison.InvariantCultureIgnoreCase)
                    && tempElement.Attribute("match") != null)
                {
                    bindingValue = tempElement.Attribute("match").Value;

                    break;
                }
                else if (tempElement.Name.LocalName.Equals("for-each", StringComparison.InvariantCultureIgnoreCase)
                    && tempElement.Attribute("select") != null)
                {
                    bindingValue = tempElement.Attribute("select").Value;

                    break;
                }
            }

            return bindingValue;
        }

        public IList<string> FindAllControlIDs()
        {
            List<String> allControlIDs = new List<string>();

            XDocument viewDoc = XDocument.Load(this.ViewFileContent);
            XNamespace xdNamespace = LinqHelper.GetNamespace(viewDoc, "xd");
            var elementsWithCtrlId = LinqHelper.FindSepcificElementsInAllElements(viewDoc.Root, delegate(XElement element)
            {
                if (element.Attribute(xdNamespace + "CtrlId") != null)
                {
                    return true;
                }

                return false;
            });

            foreach (XElement elementWithCtrlId in elementsWithCtrlId)
            {
                string controlIdValue = elementWithCtrlId.Attribute(xdNamespace + "CtrlId").Value;
                if (!allControlIDs.Exists(delegate(string ctrlId)
                {
                    if (ctrlId.Equals(controlIdValue))
                    {
                        return true;
                    }

                    return false;
                }))
                {
                    allControlIDs.Add(controlIdValue);
                }
            }

            return allControlIDs;
        }

        public Dictionary<String, String> GenerateControlToElementDictionary()
        {
            Dictionary<String, String> controlToElementDict = new Dictionary<string, string>();

            XDocument viewDoc = XDocument.Load(this.ViewFileContent);
            XNamespace xdNamespace = LinqHelper.GetNamespace(viewDoc, "xd");

            // Find controls are bound to field.
            var elementsWithBinding = LinqHelper.FindSepcificElementsInAllElements(viewDoc.Root, delegate(XElement element)
            {
                Hashtable controlIDToBindingValue = new Hashtable();
                foreach (BindingValueAndControlFinder finder in this.bindingValueAndControlFinders)
                {
                    finder.ControlElement = element;
                    finder.AddNamespace("xd", xdNamespace);

                    if (finder.IsValidControlElement())
                    {
                        finder.GetControlIDAndBindingValue(controlIDToBindingValue);

                        foreach (string ctrlIDValue in controlIDToBindingValue.Keys)
                        {
                            if (!controlToElementDict.Keys.Contains(ctrlIDValue))
                            {
                                controlToElementDict.Add(ctrlIDValue, (string)controlIDToBindingValue[ctrlIDValue]);
                            }
                        }

                        return true;
                    }
                }

                return false;
            });

            return controlToElementDict;
        }

        public void Dispose()
        {
            if (this.viewFileContent != null)
            {
                this.viewFileContent.Close();
            }
        }

        #endregion

        #region Private Methods

        private void GenerateDefaultFinders()
        {
            this.bindingValueAndControlFinders = new List<BindingValueAndControlFinder>();
            this.bindingValueAndControlFinders.Add(new DateTimePickerControlBindingValueAndControlFinder());
            this.bindingValueAndControlFinders.Add(new ExpressionBoxWithoutBindingAttributeControlBindingValueAndControlFinder());
            this.bindingValueAndControlFinders.Add(new MultiSelectControlBindingValueAndControlFinder());
            this.bindingValueAndControlFinders.Add(new RepeatingTableControlBindingValueAndControlFinder());
            this.bindingValueAndControlFinders.Add(new SectionControlBindingValueAndControlFinder());
            this.bindingValueAndControlFinders.Add(new SimpleControlBindingValueAndControlFinder());
        }

        #endregion
    }
}
