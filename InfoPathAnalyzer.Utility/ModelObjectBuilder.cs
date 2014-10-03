using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml.Schema;
using GeekBangCN.InfoPathAnalyzer.Model;
using System.IO;

namespace GeekBangCN.InfoPathAnalyzer.Utility
{
    public class ModelObjectBuilder
    {
        private ModelObjectBuilder()
        {
        }

        public static Control CreateControl(string controlID, Model.View parentView)
        {
            Control control = new Control();
            control.ID = controlID;
            control.ParentView = parentView;

            return control;
        }

        public static Element CreateElementFromXmlSchemaObjectAndDataSource(XmlSchemaObject schemaObject, DataSource dataSource, Stream xsnFileStream)
        {
            Element element = new Element();
            element.DataSource = dataSource;
            element.Type = XsdParser.GetElementAndAttributeType(schemaObject);

            XsdParser xsdParser = XsdParserFactory.CreateInfoPathXsdParser(dataSource.Content, xsnFileStream);
            XmlSchemaObject realSchemaObject = xsdParser.FindRealItem(schemaObject);
            if (XsdParser.IsXmlObjectElement(realSchemaObject))
            {
                XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)realSchemaObject;

                element.Name = xmlSchemaElement.QualifiedName.Name; // Note: xmlSchemaElement.Name may be null. We must use QualifiedName.
                element.NameWithNamespacePrefix = XsdParser.GetElementNameWithNamespacePrefix(xmlSchemaElement);
                element.NamespaceUri = xmlSchemaElement.QualifiedName.Namespace;
                element.DataType = xmlSchemaElement.SchemaTypeName.Name;
                element.IsRepeating = false; // TODO: TBD
            }
            else if (XsdParser.IsXmlObjectAttribute(realSchemaObject))
            {
                XmlSchemaAttribute xmlSchemaAttribute = (XmlSchemaAttribute)realSchemaObject;

                element.Name = xmlSchemaAttribute.QualifiedName.Name; // Note: xmlSchemaElement.Name may be null. We must use QualifiedName.
                element.NameWithNamespacePrefix = XsdParser.GetElementNameWithNamespacePrefix(xmlSchemaAttribute);
                element.NamespaceUri = xmlSchemaAttribute.QualifiedName.Namespace;
                element.DataType = xmlSchemaAttribute.SchemaTypeName.Name;
                element.IsRepeating = false; // TODO: TBD
            }
            else
            {
                // Not support for other type currently.
                // Debug.Assert(false, "The schemaObject is neither Element nor Attribute");

                return null;
            }

            return element;
        }
    }
}
