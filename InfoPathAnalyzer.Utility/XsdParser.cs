using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Schema;
using System.IO;
using System.Xml;
using System.Collections;
using System.Xml.Linq;

namespace GeekBangCN.InfoPathAnalyzer.Utility
{
    public class XsdParser
    {
        private string validationError;
        //private XmlSchema schema;
        private List<XmlSchema> schemaList = new List<XmlSchema>();
        private ValidationEventHandler validateEventHandler;

        public XsdParser(Stream xsdConent)
        {
            this.Initialize(xsdConent);
        }

        public string ValidationError
        {
            get
            {
                return this.validationError;
            }
        }

        #region Public Methods

        public static XmlQualifiedName[] GetItemNamespaces(XmlSchemaObject item)
        {
            XmlSchemaObject root = item;
            while (root.Parent != null)
            {
                root = root.Parent;
            }

            return root.Namespaces.ToArray();
        }

        public static string GetElementNameWithNamespacePrefix(XmlSchemaElement element)
        {
            XmlQualifiedName[] allNamespaces = GetItemNamespaces(element);
            foreach (XmlQualifiedName ns in allNamespaces)
            {
                if (string.Compare(element.QualifiedName.Namespace, ns.Namespace, true) == 0)
                {
                    return String.Format("{0}:{1}", ns.Name, element.QualifiedName.Name);
                }
            }

            return element.QualifiedName.ToString();
        }

        public static string GetElementNameWithNamespacePrefix(XmlSchemaAttribute attribute)
        {
            XmlQualifiedName[] allNamespaces = GetItemNamespaces(attribute);
            foreach (XmlQualifiedName ns in allNamespaces)
            {
                if (String.Compare(attribute.QualifiedName.Namespace, ns.Namespace, true) == 0)
                {
                    return String.Format("{0}:{1}", ns.Name, attribute.QualifiedName.Name);
                }
            }

            return attribute.QualifiedName.ToString();
        }

        public static string GetElementAndAttributeType(XmlSchemaObject schemaObject)
        {
            if (IsXmlObjectElement(schemaObject))
            {
                XmlSchemaElement xmlElement = (XmlSchemaElement)schemaObject;
                if ((xmlElement.SchemaType != null) && (xmlElement.SchemaType.GetType() == typeof(XmlSchemaComplexType)))
                {
                    return "Group";
                }
                else
                {
                    return "Field";
                }
            }
            else
            {
                // The attriubte cannot be grouped.
                return "Field";
            }
        }

        public static bool IsXmlObjectElement(XmlSchemaObject schemaObject)
        {
            return schemaObject.GetType() == typeof(XmlSchemaElement);
        }

        public static bool IsXmlObjectAttribute(XmlSchemaObject schemaObject)
        {
            return schemaObject.GetType() == typeof(XmlSchemaAttribute);
        }

        /// <summary>
        /// Gets data type of a field.
        /// </summary>
        /// <param name="xEl"></param>
        /// <returns>If success, return a non-namespace data type name, otherwise return an empty string.</returns>
        public static string GetElementAndAttributeDataType(XmlSchemaObject schemaObject)
        {
            if (IsXmlObjectElement(schemaObject))
            {
                XmlSchemaElement xmlElement = (XmlSchemaElement)schemaObject;
                return xmlElement.SchemaTypeName.Name;
            }
            else
            {
                XmlSchemaAttribute xmlAttribute = (XmlSchemaAttribute)schemaObject;
                return xmlAttribute.SchemaTypeName.Name;
            }
        }

        public XmlSchemaObject FindRealItem(XmlSchemaObject referenceItem)
        {
            XmlSchemaObject realItem = referenceItem;

            if (XsdParser.IsXmlObjectElement(referenceItem))
            {
                XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)referenceItem;
                if (!xmlSchemaElement.RefName.IsEmpty)
                {
                    realItem = this.GetSingleItemByName(xmlSchemaElement.RefName.Name, xmlSchemaElement.RefName.Namespace);
                }
            }
            else if (XsdParser.IsXmlObjectAttribute(referenceItem))
            {
                XmlSchemaAttribute xmlSchemaAttribute = (XmlSchemaAttribute)referenceItem;
                if (!xmlSchemaAttribute.RefName.IsEmpty)
                {
                    realItem = this.GetSingleItemByName(xmlSchemaAttribute.RefName.Name, xmlSchemaAttribute.RefName.Namespace);
                }
            }

            return realItem;
        }

        public IList<XmlSchemaObject> GetAllElementsAndAttributes(bool filterReferencedItems)
        {
            Hashtable referencedItemsQualifiedName = new Hashtable();

            List<XmlSchemaObject> xmlSchemaItemList = new List<XmlSchemaObject>();
            XmlSchema schema = this.schemaList.First(); // Only get the main schema.
            foreach (XmlSchemaObject schemaObj in schema.Items)
            {
                bool isXmlObjectElement = IsXmlObjectElement(schemaObj);
                bool isXmlObjectAttribute = IsXmlObjectAttribute(schemaObj);

                if (isXmlObjectElement || isXmlObjectAttribute)
                {
                    xmlSchemaItemList.Add(schemaObj);

                    if (!filterReferencedItems || !isXmlObjectElement)
                    {
                        continue;
                    }

                    // Find the children of current element, add reference items to the hash table for filtering.
                    List<XmlSchemaObject> children = this.GetChildElementsAndAttributes((XmlSchemaElement)schemaObj);
                    foreach (XmlSchemaObject child in children)
                    {
                        if (IsXmlObjectElement(child))
                        {
                            XmlSchemaElement childElement = (XmlSchemaElement)child;
                            if (!string.IsNullOrWhiteSpace(childElement.RefName.ToString())) // If this is a reference child.
                            {
                                string refName = childElement.RefName.ToString();
                                if (!referencedItemsQualifiedName.ContainsKey(refName))
                                {
                                    referencedItemsQualifiedName.Add(refName, string.Empty);
                                }
                            }
                        }
                        else if (IsXmlObjectAttribute(child))
                        {
                            XmlSchemaAttribute childAttribute = (XmlSchemaAttribute)child;
                            if (!string.IsNullOrWhiteSpace(childAttribute.RefName.ToString())) // If this is a reference child.
                            {
                                referencedItemsQualifiedName.Add((childAttribute).RefName.ToString(), string.Empty);
                            }
                        }
                    } // End of foreach (XmlSchemaObject child in children)
                } // End of if (isXmlObjectElement || isXmlObjectAttribute)
            }

            // Whether need to filter the referenced items.
            if (!filterReferencedItems || referencedItemsQualifiedName.Keys.Count <= 0)
            {
                return xmlSchemaItemList;
            }

            // Filter referenced items.
            List<XmlSchemaObject> filteredXmlSchemaItemList = new List<XmlSchemaObject>();
            foreach (XmlSchemaObject item in xmlSchemaItemList)
            {
                string qualifiedName = string.Empty;
                if (IsXmlObjectElement(item))
                {
                    XmlSchemaElement element = (XmlSchemaElement)item;
                    qualifiedName = element.QualifiedName.ToString();
                }
                else if (IsXmlObjectAttribute(item))
                {
                    XmlSchemaAttribute attribute = (XmlSchemaAttribute)item;
                    qualifiedName = attribute.QualifiedName.ToString();
                }

                if (!referencedItemsQualifiedName.ContainsKey(qualifiedName))
                {
                    filteredXmlSchemaItemList.Add(item);
                }
            }

            return filteredXmlSchemaItemList;
        }

        //private XmlSchemaAttribute FindTypeReferencedItem(XmlSchemaAttribute typeAttribute)
        //{
        //    foreach (XmlSchema schema in this.schemaList)
        //    {
        //        foreach (XmlSchemaObject schemaObject in schema.Items)
        //        {
        //            if (!schemaObject.GetType().Equals(typeof(XmlSchemaElement)))
        //            {
        //                continue;
        //            }

        //            XmlSchemaAttribute schemaAttributeObject = (XmlSchemaAttribute)schemaObject;

        //            if (typeAttribute.SchemaTypeName.Equals(schemaAttributeObject.SchemaTypeName))
        //            {
        //                return schemaAttributeObject;
        //            }
        //        }
        //    }

        //    return typeAttribute;
        //}

        //private XmlSchemaElement FindTypeReferencedItem(XmlSchemaElement typeElement)
        //{
        //    foreach (XmlSchema schema in this.schemaList)
        //    {
        //        foreach (XmlSchemaObject schemaObject in schema.Items)
        //        {
        //            if (!schemaObject.GetType().Equals(typeof(XmlSchemaElement)))
        //            {
        //                continue;
        //            }

        //            XmlSchemaElement schemaElementObject = (XmlSchemaElement)schemaObject;

        //            if (typeElement.SchemaTypeName.Equals(schemaElementObject.SchemaTypeName))
        //            {
        //                return schemaElementObject;
        //            }
        //        }
        //    }

        //    return typeElement;
        //}

        /// <summary>
        /// Get single item by name.
        /// Don't pass the Name With Prefix!
        /// </summary>
        /// <param name="itemName">An element name. Don't pass the Name With Prefix!</param>
        /// <returns></returns>
        public XmlSchemaObject GetSingleItemByName(string itemName, string itemNamespace)
        {
            XmlQualifiedName qualifiedName = new XmlQualifiedName(itemName, itemNamespace);

            XmlSchemaObject foundObject = null;

            foreach (XmlSchema schema in this.schemaList)
            {
                foreach (XmlSchemaObject schemaObject in schema.Items)
                {
                    // If it is an element, we should travel all child elements.
                    if (IsXmlObjectElement(schemaObject))
                    {
                        XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)schemaObject;
                        if (xmlSchemaElement.QualifiedName.Equals(qualifiedName))
                        {
                            return xmlSchemaElement;
                        }

                        foundObject = FindElementByQualifiedName(qualifiedName, xmlSchemaElement);
                        if (foundObject != null)
                        {
                            if (IsXmlObjectElement(foundObject))
                            {
                                XmlSchemaElement xmlElement = (XmlSchemaElement)foundObject;

                                // If the xml object's RefName is empty, it means this element is the actual
                                // element in the schema. Otherwise, we must continue to find the actual element.
                                if (xmlElement.RefName.IsEmpty)
                                {
                                    return foundObject;
                                }
                            }
                            else if (IsXmlObjectAttribute(foundObject))
                            {
                                XmlSchemaAttribute xmlAttribute = (XmlSchemaAttribute)foundObject;

                                // If the xml object's RefName is empty, it means this element is the actual
                                // element in the schema. Otherwise, we must continue to find the actual element.
                                if (xmlAttribute.RefName.IsEmpty)
                                {
                                    return foundObject;
                                }
                            }
                        }
                    }
                    else if (IsXmlObjectAttribute(schemaObject))
                    {
                        XmlSchemaAttribute xmlSchemaAttribute = (XmlSchemaAttribute)schemaObject;
                        if (xmlSchemaAttribute.QualifiedName.Equals(qualifiedName))
                        {
                            return xmlSchemaAttribute;
                        }
                    }
                }
            }

            return foundObject;
        }

        /// <summary>
        /// Get child elements.
        /// NOTE: The names of returned elements are null. Use QualifiedName or RefName.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public List<XmlSchemaObject> GetChildElementsAndAttributes(XmlSchemaElement element)
        {
            List<XmlSchemaObject> childElementList = new List<XmlSchemaObject>();
            if (element.ElementSchemaType == null || !(element.ElementSchemaType is XmlSchemaComplexType))
            {
                return childElementList;
            }

            XmlSchemaComplexType elementComplextType = (XmlSchemaComplexType)element.ElementSchemaType;

            // Find if there's any child element
            XmlSchemaParticle particle = ((XmlSchemaComplexType)element.ElementSchemaType).Particle;
            if (particle != null)
            {
                XmlSchemaObjectCollection childElements = ((XmlSchemaGroupBase)particle).Items;
                foreach (XmlSchemaObject childElement in childElements)
                {
                    childElementList.Add(childElement);
                }
            }

            // Find child attributes
            foreach (XmlSchemaObject attribute in elementComplextType.AttributeUses.Values)
            {
                childElementList.Add(attribute);
            }

            return childElementList;
        }

        public void ReadExternalSchema(Func<string, Stream> readExternalSchemaContent)
        {
            List<XmlSchema> xmlSchemaList = new List<XmlSchema>();
            foreach (XmlSchema s in this.schemaList)
            {
                xmlSchemaList.Add(s);
            }

            Hashtable addedExternalSchema = new Hashtable();

            for (int i = 0; i < xmlSchemaList.Count; i++)
            {
                XmlSchema s = (XmlSchema)xmlSchemaList[i];

                // Find external schemas.
                foreach (XmlSchemaObject externalSchemaObject in s.Includes)
                {
                    if (externalSchemaObject is XmlSchemaImport)
                    {
                        string externalSchemaFileName = ((XmlSchemaExternal)externalSchemaObject).SchemaLocation;
                        if (addedExternalSchema.ContainsKey(externalSchemaFileName))
                        {
                            continue;
                        }

                        Stream externalSchemaFileStream = readExternalSchemaContent(externalSchemaFileName);
                        XmlSchema externalSchema = XmlSchema.Read(externalSchemaFileStream, this.validateEventHandler);

                        xmlSchemaList.Add(externalSchema);
                        addedExternalSchema.Add(externalSchemaFileName, string.Empty);
                    }
                }
            }

            // Add xml schemas to xml schema set.
            XmlSchemaSet xmlSchemaSet = new XmlSchemaSet();
            foreach (XmlSchema xmlSchema in xmlSchemaList)
            {
                xmlSchemaSet.Add(xmlSchema);
            }

            try
            {
                xmlSchemaSet.Compile();
            }
            catch (XmlSchemaException)
            {
            }

            this.schemaList.Clear();
            ArrayList xmlSchemaArray = new ArrayList(xmlSchemaSet.Schemas());
            foreach (XmlSchema xmlSchema in xmlSchemaArray)
            {
                this.schemaList.Add(xmlSchema);
            }
        }

        #endregion

        #region Private Methods

        private void Initialize(Stream xsdContent)
        {
            this.validateEventHandler = new ValidationEventHandler(LogValidationgError);
            XmlSchemaSet schemaSet = new XmlSchemaSet();

            xsdContent.Position = 0;
            XmlSchema schema = XmlSchema.Read(xsdContent, this.validateEventHandler);
            schemaSet.Add(schema);
            try
            {
                schemaSet.Compile();
            }
            catch (XmlSchemaException)
            {
            }

            ArrayList xmlSchemaList = new ArrayList(schemaSet.Schemas());
            foreach (XmlSchema xmlSchema in xmlSchemaList)
            {
                this.schemaList.Add(xmlSchema);
            }
        }

        private XmlSchemaObject FindElementByQualifiedName(XmlQualifiedName qualifiedName, XmlSchemaElement parentElement)
        {
            if ((parentElement.ElementSchemaType == null) || (parentElement.ElementSchemaType.GetType() != typeof(XmlSchemaComplexType)))
            {
                return null;
            }

            List<XmlSchemaObject> allChildObjects = GetChildElementsAndAttributes(parentElement);
            foreach (XmlSchemaObject childObject in allChildObjects)
            {
                XmlQualifiedName childObjectQualifiedName = null;
                Boolean isXmlObjectElement = IsXmlObjectElement(childObject);
                if (isXmlObjectElement)
                {
                    XmlSchemaElement childElement = (XmlSchemaElement)childObject;
                    childObjectQualifiedName = childElement.QualifiedName;
                    if (childObjectQualifiedName.Equals(qualifiedName))
                    {
                        return childObject;
                    }
                }
                else if (IsXmlObjectAttribute(childObject))
                {
                    XmlSchemaAttribute childAttribute = (XmlSchemaAttribute)childObject;
                    childObjectQualifiedName = childAttribute.QualifiedName;

                    // The attribute qualified name doesn't have namespace, so we compare the names.
                    if (String.Compare(childObjectQualifiedName.Name, childAttribute.QualifiedName.Name) == 0)
                    {
                        return childObject;
                    }
                }

                if (isXmlObjectElement)
                {
                    XmlSchemaObject foundObject = FindElementByQualifiedName(qualifiedName, (XmlSchemaElement)childObject);
                    if (foundObject != null)
                    {
                        return foundObject;
                    }
                }
            }

            return null;
        }

        private void LogValidationgError(object sender, ValidationEventArgs e)
        {
            this.validationError += String.Format("Severity: {0}, Message: {1}" + Environment.NewLine, e.Severity.ToString(), e.Message);
        }

        #endregion
    }
}
