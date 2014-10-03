using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace GeekBangCN.InfoPathAnalyzer.IInfoPathViewExtension
{
    public interface IXdXDocument
    {
        string Role
        {
            get;
            set;
        }

        XPathNodeIterator GetDOM(string dataSourceName);

        string GetNamedNodeProperty(XPathNavigator mainDOMNode, string propertyName, string defaultValue);
    }
}
