using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace GeekBangCN.InfoPathAnalyzer.Utility.View
{
    public abstract class BindingValueAndControlFinder
    {
        private XElement controlElement;
        private Dictionary<string, XNamespace> namespaceRepository = new Dictionary<string, XNamespace>();
        private XNamespace xdNamespace;

        public XElement ControlElement
        {
            get
            {
                return this.controlElement;
            }

            set
            {
                this.controlElement = value;
            }
        }

        public Dictionary<string, XNamespace> NamespaceRepository
        {
            get
            {
                return this.namespaceRepository;
            }
        }

        public static string GetBindingElementNameFromValueOfXslMethod(XElement element)
        {
            string bindingElementName = string.Empty;
            IEnumerable<XElement> children = element.Elements();
            if (children.Count() > 0)
            {
                foreach (XElement subElement in children)
                {
                    if (subElement.Name.LocalName.Equals("value-of", StringComparison.InvariantCultureIgnoreCase))
                    {
                        XAttribute selectAttribute = subElement.Attribute("select");
                        if (selectAttribute != null)
                        {
                            bindingElementName = selectAttribute.Value;
                        }
                    }
                    else
                    {
                        bindingElementName = GetBindingElementNameFromValueOfXslMethod(subElement);
                    }

                    if (!string.IsNullOrEmpty(bindingElementName))
                    {
                        break;
                    }
                }
            }

            // Need to find the actual binding value.
            if (bindingElementName.Equals(".", StringComparison.InvariantCultureIgnoreCase))
            {
                bindingElementName = FindRealBindingValueFromRelatedBindingValue(element);
            }

            return bindingElementName;
        }

        public void AddNamespace(string prefix, XNamespace ns)
        {
            if (!this.namespaceRepository.ContainsKey(prefix))
            {
                this.namespaceRepository.Add(prefix, ns);
            }
        }

        public void RemoveNamespace(string prefix)
        {
            if (this.namespaceRepository.ContainsKey(prefix))
            {
                this.namespaceRepository.Remove(prefix);
            }
        }

        public abstract bool IsValidControlElement();
        public abstract string GetBindingValue();
        public abstract bool GetControlIDAndBindingValue(Hashtable controlIDToBindingValue);

        protected static string FindRealBindingValueFromRelatedBindingValue(XElement element)
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

        protected XNamespace GetXdNamespace()
        {
            if (this.xdNamespace == null)
            {
                this.xdNamespace = this.NamespaceRepository["xd"];
            }

            return this.xdNamespace;
        }
    }
}
