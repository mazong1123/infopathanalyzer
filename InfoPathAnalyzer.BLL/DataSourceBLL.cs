using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Schema;
using GeekBangCN.InfoPathAnalyzer.DAL;
using GeekBangCN.InfoPathAnalyzer.IBLL;
using GeekBangCN.InfoPathAnalyzer.IDAL;
using GeekBangCN.InfoPathAnalyzer.Model;
using GeekBangCN.InfoPathAnalyzer.Utility;

namespace GeekBangCN.InfoPathAnalyzer.BLL
{
    public class DataSourceBLL : BLLBaseWithDAL<IDataSourceDAL>, IDataSourceBLL
    {
        #region Constructors

        public DataSourceBLL()
        {
        }

        public DataSourceBLL(Stream xsnFileContents)
        {
            this.DataAccessLayer = new DataSourceDAL(xsnFileContents);
        }

        public DataSourceBLL(string xsnFilePath)
        {
            this.DataAccessLayer = new DataSourceDAL(xsnFilePath);
        }

        #endregion

        #region Public Methods

        public IList<DataSource> GetDataSources(bool isGetContent)
        {
            this.ValidateDataAccessLayer();

            return this.DataAccessLayer.GetDataSources(isGetContent);
        }

        public IList<DataSource> GetMainDataSources(bool isGetContent)
        {
            this.ValidateDataAccessLayer();

            return this.DataAccessLayer.GetMainDataSources(isGetContent);
        }

        public void GetElements(DataSource dataSource)
        {
            if (dataSource.Content == null)
            {
                throw new ArgumentException("The data source does not have any content.");
            }

            dataSource.Elements.Clear();
            dataSource.IsAlreadyGotElements = false;

            // Create xsd parser.
            XsdParser xsdParser = XsdParserFactory.CreateInfoPathXsdParser(dataSource.Content, ((DALBase)this.DataAccessLayer).XsnFileStream);

            // Get all elements.
            IList<XmlSchemaObject> schemaObjectList = xsdParser.GetAllElementsAndAttributes(true);
            foreach (XmlSchemaObject schemaObject in schemaObjectList)
            {
                Element element = ModelObjectBuilder.CreateElementFromXmlSchemaObjectAndDataSource(schemaObject, dataSource, ((DALBase)this.DataAccessLayer).XsnFileStream);
                dataSource.Elements.Add(element);
            }

            dataSource.IsAlreadyGotElements = true;
        }

        #endregion

        #region Private Methods



        #endregion
    }
}
