using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

namespace GeekBangCN.InfoPathAnalyzer.Utility
{
    public sealed class LinqHelper
    {
        private LinqHelper()
        {
        }

        /// <summary>
        /// Gets a specified namespace in a xml document.
        /// </summary>
        /// <param name="xDoc">A XDocument object contains the xml text.</param>
        /// <param name="name">The name of namespace.</param>
        /// <returns>A XNamespace object.</returns>
        public static XNamespace GetNamespace(XDocument xDoc, String name)
        {
            XPathNavigator nav = xDoc.CreateNavigator();
            nav.MoveToFollowing(XPathNodeType.Element);
            IDictionary<String, String> nsDict = nav.GetNamespacesInScope(System.Xml.XmlNamespaceScope.All);
            return XNamespace.Get(nsDict[name]);
        }

        public static XElement FindSpecificElementInAllElements(XElement root, Func<XElement, bool> filterMethod)
        {
            return RecursivelyFindSpecificElementInAllElements(root, filterMethod);
        }

        public static IList<XElement> FindSepcificElementsInAllElements(XElement root, Func<XElement, bool> filterMethod)
        {
            return RecursivelyFindSpecificElementsInAllElements(root, filterMethod);
        }

        private static IList<XElement> RecursivelyFindSpecificElementsInAllElements(XElement parent, Func<XElement, bool> filterMethod)
        {
            List<XElement> foundElementList = new List<XElement>();
            if (filterMethod(parent))
            {
                foundElementList.Add(parent);
            }

            var children = parent.Elements();

            foreach (XElement child in children)
            {
                IList<XElement> foundElements = RecursivelyFindSpecificElementsInAllElements(child, filterMethod);
                foundElementList.AddRange(foundElements);
            }

            return foundElementList;
        }

        private static XElement RecursivelyFindSpecificElementInAllElements(XElement parent, Func<XElement, bool> filterMethod)
        {
            if (filterMethod(parent))
            {
                return parent;
            }

            var children = parent.Elements();
            var query = from c in children
                        where filterMethod(c)
                        select c;

            if (query != null && query.Count() > 0)
            {
                return query.First();
            }

            foreach (XElement child in children)
            {
                XElement foundElement = RecursivelyFindSpecificElementInAllElements(child, filterMethod);
                if (foundElement != null)
                {
                    return foundElement;
                }
            }

            return null;
        }
    }
}
