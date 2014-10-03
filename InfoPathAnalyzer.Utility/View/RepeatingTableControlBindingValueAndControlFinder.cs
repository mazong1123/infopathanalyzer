using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace GeekBangCN.InfoPathAnalyzer.Utility.View
{
    public class RepeatingTableControlBindingValueAndControlFinder : BindingValueAndControlFinder
    {
        public RepeatingTableControlBindingValueAndControlFinder()
        {
        }

        public RepeatingTableControlBindingValueAndControlFinder(XElement element, XNamespace xdNamespace)
        {
            this.ControlElement = element;
            this.NamespaceRepository.Add("xd", xdNamespace);
        }

        public override bool IsValidControlElement()
        {
            if (this.ControlElement.Name.LocalName.Equals("table", StringComparison.InvariantCultureIgnoreCase)
                && this.ControlElement.Attribute("class") != null
                && this.ControlElement.Attribute("class").Value.Contains("xdRepeatingTable"))
            {
                return true;
            }

            return false;
        }

        public override string GetBindingValue()
        {
            string selectFieldXPath = string.Empty;

            XElement forEachElement = LinqHelper.FindSpecificElementInAllElements(this.ControlElement, delegate(XElement currentElement)
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
                selectFieldXPath = FindRealBindingValueFromRelatedBindingValue(this.ControlElement);
            }

            return selectFieldXPath;
        }

        public override bool GetControlIDAndBindingValue(System.Collections.Hashtable controlIDToBindingValue)
        {
            string selectFieldXPath = this.GetBindingValue();
            if (string.IsNullOrWhiteSpace(selectFieldXPath))
            {
                return false;
            }

            // Find "tbody".
            XElement tbodyElement = LinqHelper.FindSpecificElementInAllElements(this.ControlElement, delegate(XElement currentElement)
            {
                if (currentElement.Name.LocalName.Equals("tbody", StringComparison.InvariantCultureIgnoreCase)
                    && currentElement.Attribute(this.GetXdNamespace() + "xctname") != null
                    && currentElement.Attribute(this.GetXdNamespace() + "xctname").Value.Equals("RepeatingTable", StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }

                return false;
            });

            // Add the table element bindings.
            XAttribute ctrlIDAttribute = this.ControlElement.Attribute(this.GetXdNamespace() + "CtrlId");
            if (ctrlIDAttribute != null)
            {
                controlIDToBindingValue.Add(ctrlIDAttribute.Value, selectFieldXPath);
            }

            // Find binding elements.
            IList<XElement> bindingElements = LinqHelper.FindSepcificElementsInAllElements(this.ControlElement, delegate(XElement currentElement)
            {
                if (currentElement.Attribute(this.GetXdNamespace() + "binding") != null
                    && currentElement.Attribute(this.GetXdNamespace() + "CtrlId") != null)
                {
                    return true;
                }

                return false;
            });

            foreach (XElement bindingElement in bindingElements)
            {
                string bindingValue = bindingElement.Attribute(this.GetXdNamespace() + "binding").Value;
                string controlID = bindingElement.Attribute(this.GetXdNamespace() + "CtrlId").Value;
                if (bindingValue.Equals("."))
                {
                    bindingValue = selectFieldXPath;
                }

                controlIDToBindingValue.Add(controlID, bindingValue);
            }

            return true;
        }
    }
}
