using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace GeekBangCN.InfoPathAnalyzer.Utility.View
{
    public class ExpressionBoxWithoutBindingAttributeControlBindingValueAndControlFinder : BindingValueAndControlFinder
    {
        public ExpressionBoxWithoutBindingAttributeControlBindingValueAndControlFinder()
        {
        }

        public ExpressionBoxWithoutBindingAttributeControlBindingValueAndControlFinder(XElement element, XNamespace xdNamespace)
        {
            this.ControlElement = element;
            this.NamespaceRepository.Add("xd", xdNamespace);
        }

        public override bool IsValidControlElement()
        {
            if (this.ControlElement.Name.LocalName.Equals("span", StringComparison.InvariantCultureIgnoreCase)
                && this.ControlElement.Attribute("class") != null && this.ControlElement.Attribute("class").Value.Equals("xdExpressionBox xdDataBindingUI", StringComparison.InvariantCultureIgnoreCase)
                && this.ControlElement.Attribute(this.GetXdNamespace() + "binding") == null)
            {
                return true;
            }

            return false;
        }

        public override string GetBindingValue()
        {
            return GetBindingElementNameFromValueOfXslMethod(this.ControlElement);
        }

        public override bool GetControlIDAndBindingValue(System.Collections.Hashtable controlIDToBindingValue)
        {
            if (this.ControlElement.Attribute(this.GetXdNamespace() + "CtrlId") == null)
            {
                return false;
            }

            string bindingValue = this.GetBindingValue();
            string controlID = this.ControlElement.Attribute(this.GetXdNamespace() + "CtrlId").Value;

            controlIDToBindingValue.Add(controlID, bindingValue);

            return true;
        }
    }
}
