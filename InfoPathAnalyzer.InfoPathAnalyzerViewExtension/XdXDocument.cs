using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeekBangCN.InfoPathAnalyzer.IInfoPathViewExtension;
using System.Xml;
using System.Xml.XPath;

namespace GeekBangCN.InfoPathAnalyzer.InfoPathAnalyzerViewExtension
{
    public class XdXDocument : IXdXDocument
    {
        private string userRole = string.Empty;
        private string dummyXml = @"<Root><Child></Child></Root>";

        public string Role
        {
            get
            {
                return this.userRole;
            }
            set
            {
                this.userRole = value;
            }
        }

        public XPathNodeIterator GetDOM(string dataSourceName)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(this.dummyXml);
            return xmlDocument.CreateNavigator().Select("/");
        }


        public string GetNamedNodeProperty(XPathNavigator mainDOMNode, string propertyName, string defaultValue)
        {
            return defaultValue;
        }
    }
}
