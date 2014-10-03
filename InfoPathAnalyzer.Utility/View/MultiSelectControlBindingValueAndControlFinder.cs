using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace GeekBangCN.InfoPathAnalyzer.Utility.View
{
    public class MultiSelectControlBindingValueAndControlFinder : BindingValueAndControlFinder
    {
        public MultiSelectControlBindingValueAndControlFinder()
        {
        }

        public MultiSelectControlBindingValueAndControlFinder(XElement element, XNamespace xdNamespace)
        {
            this.ControlElement = element;
            this.NamespaceRepository.Add("xd", xdNamespace);
        }

        public override bool IsValidControlElement()
        {
            if (this.ControlElement.Name.LocalName.Equals("span", StringComparison.InvariantCultureIgnoreCase)
                && this.ControlElement.Attribute("class") != null
                && this.ControlElement.Attribute("class").Value.Equals("xdMultiSelectList", StringComparison.InvariantCultureIgnoreCase)
                && this.ControlElement.Attribute(this.GetXdNamespace() + "CtrlId") != null
                && this.ControlElement.Attribute(this.GetXdNamespace() + "ref") != null)
            {
                return true;
            }

            return false;
        }

        public override string GetBindingValue()
        {
            string bindingValue = this.ControlElement.Attribute(this.GetXdNamespace() + "ref").Value;

            // Need to find the actual binding value.
            if (bindingValue.Equals(".", StringComparison.InvariantCultureIgnoreCase))
            {
                bindingValue = FindRealBindingValueFromRelatedBindingValue(this.ControlElement);
            }

            return bindingValue;
        }

        public override bool GetControlIDAndBindingValue(System.Collections.Hashtable controlIDToBindingValue)
        {
            string bindingValue = this.GetBindingValue();
            string controlID = this.ControlElement.Attribute(this.GetXdNamespace() + "CtrlId").Value;

            controlIDToBindingValue.Add(controlID, bindingValue);

            return true;
        }
    }
}
