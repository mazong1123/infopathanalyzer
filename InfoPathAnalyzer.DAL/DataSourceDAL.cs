using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GeekBangCN.InfoPathAnalyzer.IDAL;
using GeekBangCN.InfoPathAnalyzer.Model;
using GeekBangCN.InfoPathAnalyzer.Utility;

namespace GeekBangCN.InfoPathAnalyzer.DAL
{
    public class DataSourceDAL : DALBase, IDataSourceDAL
    {
        #region Constructors

        public DataSourceDAL(Stream xsnFileContents)
        {
            this.XsnFileStream = xsnFileContents;
        }

        public DataSourceDAL(string xsnFilePath)
        {
            using (StreamReader sr = new StreamReader(xsnFilePath))
            {
                this.XsnFileStream = this.CloneStream(sr.BaseStream);
            }
        }

        #endregion

        #region Public Methods

        public IList<DataSource> GetDataSources(bool isGetContent)
        {
            Stream manifestFileStream = this.GetManifestFileStream();
            ManifestManager manifestManager = new ManifestManager(manifestFileStream);

            List<DataSource> dataSources = new List<DataSource>();

            // Add main data sources (only has display name and internal name value).
            dataSources.AddRange(manifestManager.GetNameOfMainDataSources());

            // Add secondary data sources (only has display name and internal name value).
            dataSources.AddRange(manifestManager.GetNameOfSecondaryDataSources());

            // If need to get data source content.
            if (isGetContent)
            {
                this.GetDataSourceContent(dataSources);
            }

            return dataSources;
        }

        public IList<DataSource> GetMainDataSources(bool isGetContent)
        {
            Stream manifestFileStream = this.GetManifestFileStream();
            ManifestManager manifestManager = new ManifestManager(manifestFileStream);

            List<DataSource> dataSources = new List<DataSource>();

            // Add main data sources (only has display name and internal name value).
            dataSources.AddRange(manifestManager.GetNameOfMainDataSources());

            // If need to get data source content.
            if (isGetContent)
            {
                this.GetDataSourceContent(dataSources);
            }

            return dataSources;
        }

        #endregion

        #region Private Methods

        private void GetDataSourceContent(IList<DataSource> dataSources)
        {
            using (XsnManager xsnManager = new XsnManager(this.XsnFileStream))
            {
                foreach (DataSource dataSource in dataSources)
                {
                    Stream content = xsnManager.GetInnerFileStream(dataSource.InternalFileName);
                    MemoryStream ms = new MemoryStream();
                    content.CopyTo(ms);
                    ms.Position = 0;
                    dataSource.Content = ms;
                }
            }
        }

        #endregion
    }
}
