using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace GeekBangCN.InfoPathAnalyzer.Utility.View
{
    public class SectionControlBindingValueAndControlFinder : BindingValueAndControlFinder
    {
        public SectionControlBindingValueAndControlFinder()
        {
        }

        public SectionControlBindingValueAndControlFinder(XElement element, XNamespace xdNamespace)
        {
            this.ControlElement = element;
            this.NamespaceRepository.Add("xd", xdNamespace);
        }

        public override bool IsValidControlElement()
        {
            if (this.ControlElement.Attribute("class") != null
                && (this.ControlElement.Attribute("class").Value.Equals("xdSection xdRepeating", StringComparison.InvariantCultureIgnoreCase)
                || this.ControlElement.Attribute("class").Value.Equals("xdRepeatingSection xdRepeating", StringComparison.InvariantCultureIgnoreCase))
                && this.ControlElement.Attribute(this.GetXdNamespace() + "CtrlId") != null)
            {
                return true;
            }

            return false;
        }

        public override string GetBindingValue()
        {
            string bindingValue = FindRealBindingValueFromRelatedBindingValue(this.ControlElement);

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
