using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using GeekBangCN.InfoPathAnalyzer.Model;

namespace GeekBangCN.InfoPathAnalyzer.Utility
{
    public class ManifestManager
    {
        #region Private Fields

        private XDocument manifestDoc;

        #endregion

        #region Constructors

        public ManifestManager(Stream manifestFileStream)
        {
            this.manifestDoc = XDocument.Load(manifestFileStream);
        }

        public ManifestManager(string manifestFilePath)
        {
            this.manifestDoc = XDocument.Load(manifestFilePath);
        }

        #endregion

        #region Public Methods

        #region Data Source Operations

        /// <summary>
        /// Get main data source names from manifest file.
        /// </summary>
        /// <returns>A list represents the secondary data sources. Note only DataSource.DisplayName and DataSource.InternalName has specified value.</returns>
        public IList<DataSource> GetNameOfMainDataSources()
        {
            // The visited xml nodes in this method are like below:
            // ==============================================================================================
            // <xsf:documentSchemas>
            //    <xsf:documentSchema rootSchema="yes" 
            //         location="http://schemas.microsoft.com/office/infopath/2003/myXSD/2004-04-08T15:45:56 myschema.xsd">
            //    </xsf:documentSchema>
            // </xsf:documentSchemas>
            // ==============================================================================================

            List<DataSource> mainDataSources = new List<DataSource>();

            XNamespace ns = LinqHelper.GetNamespace(this.manifestDoc, "xsf");
            IEnumerable<XElement> allDSElements = this.manifestDoc.Elements().First().Element(ns + "documentSchemas").Elements();
            foreach (XElement dsElement in allDSElements)
            {
                XAttribute mainDSIntenalNameAttr = dsElement.Attribute("location");
                if (mainDSIntenalNameAttr == null)
                {
                    continue;
                }

                String mainDSInternalName = mainDSIntenalNameAttr.Value;

                // Remove the namespace of this value.
                mainDSInternalName = mainDSInternalName.Substring(mainDSInternalName.LastIndexOf(" ") + 1);

                // Create a DataSource object to represent the display name and internal name.
                DataSource dataSource = new DataSource();
                dataSource.DisplayName = "Main";
                dataSource.InternalFileName = mainDSInternalName;
                dataSource.IsMainDataSource = true;

                mainDataSources.Add(dataSource);
            }

            return mainDataSources;
        }

        /// <summary>
        /// Get secondary data source names from manifest file.
        /// </summary>
        /// <returns>A list represents the secondary data sources. Note only DataSource.DisplayName and DataSource.InternalName has specified value.</returns>
        public IList<DataSource> GetNameOfSecondaryDataSources()
        {
            // The visited xml nodes in this method are like below:
            // =======================================================
            // <xsf:dataObjects>
            //    <xsf:dataObject name="Subcontractor MSSAs" 
            //                    schema="Subcontractor MSSAs.xsd" 
            //                    initOnLoad="no">
            //    </xsf:dataObject>
            //        .
            //        .
            //        .
            // </xsf:dataObjects>
            // =======================================================

            List<DataSource> secondaryDataSourceList = new List<DataSource>();

            XNamespace ns = LinqHelper.GetNamespace(this.manifestDoc, "xsf");
            XElement dataObjectsElement = this.manifestDoc.Elements().First().Element(ns + "dataObjects");
            if (dataObjectsElement == null)
            {
                return secondaryDataSourceList;
            }

            IEnumerable<XElement> allDSElements = dataObjectsElement.Elements();
            foreach (XElement dsElement in allDSElements)
            {
                String displayName = String.Empty;
                String internalName = String.Empty;

                // Retrieve the display name
                XAttribute displayNameAttr = dsElement.Attribute("name");
                if (displayNameAttr == null)
                {
                    continue;
                }

                displayName = displayNameAttr.Value;

                // Retrieve the internal name
                XAttribute internalNameAttr = dsElement.Attribute("schema");
                if (internalNameAttr == null)
                {
                    continue;
                }

                internalName = internalNameAttr.Value;

                // Create a DataSource object to represent the display name and internal name.
                DataSource secondaryDataSource = new DataSource();
                secondaryDataSource.DisplayName = displayName;
                secondaryDataSource.InternalFileName = internalName;

                secondaryDataSourceList.Add(secondaryDataSource);
            }

            return secondaryDataSourceList;
        }

        #endregion

        #region View Operations

        /// <summary>
        /// Get the default name from manifest file.
        /// </summary>
        /// <returns>A View object represents the view display name and internal name. Note only View.DisplaName and View.InternalName has specified value.</returns>
        public Model.View GetDefaultViewName()
        {
            // The visited xml nodes in this method are like below:
            // ==============================================================================================
            // <xsf:views default="0-Overview">
            //    <xsf:view name="Contract-EN-Amendment Agreement" caption="Contract-EN-Amendment Agreement">
            //       <xsf:mainpane transform="AmendmentAgreement.xsl"></xsf:mainpane>
            //    </xsf:view>
            // <xsf:views>
            // ==============================================================================================

            XNamespace ns = LinqHelper.GetNamespace(this.manifestDoc, "xsf");
            XElement viewsElement = this.manifestDoc.Elements().First().Element(ns + "views");
            if (viewsElement == null)
            {
                return null;
            }

            XAttribute defaultAttr = viewsElement.Attribute("default");
            if (defaultAttr == null)
            {
                return null;
            }

            string defalutViewDisplayName = defaultAttr.Value;
            if (string.IsNullOrWhiteSpace(defalutViewDisplayName))
            {
                return null;
            }

            // Find the default view node.
            IEnumerable<XElement> allViewElements = viewsElement.Elements();
            var query = from c in allViewElements
                        where c.Attribute("name") != null && c.Attribute("name").Value.Equals(defalutViewDisplayName)
                        select c;

            if (query == null || query.Count() <= 0)
            {
                return null;
            }

            XElement viewElement = query.First();

            XElement mainpaneElement = viewElement.Element(ns + "mainpane");
            if (mainpaneElement == null)
            {
                return null;
            }

            XAttribute transform = mainpaneElement.Attribute("transform");
            if (transform == null)
            {
                return null;
            }

            string defaultViewInternalName = transform.Value;
            if (string.IsNullOrWhiteSpace(defaultViewInternalName))
            {
                return null;
            }

            // Create a View object to represent the view display name nad internal name.
            Model.View view = new Model.View();
            view.DisplayName = defalutViewDisplayName;
            view.InternalFileName = defaultViewInternalName;
            view.IsDefaultView = true;

            return view;
        }

        /// <summary>
        /// Get the view names from manifest file.
        /// </summary>
        /// <returns>A View object represents the view display name and internal name. Note only View.DisplaName and View.InternalName has specified value.</returns>
        public IList<Model.View> GetViewNames()
        {
            // The visited xml nodes in this method are like below:
            // ==============================================================================================
            // <xsf:views default="0-Overview">
            //    <xsf:view name="Contract-EN-Amendment Agreement" caption="Contract-EN-Amendment Agreement">
            //       <xsf:mainpane transform="AmendmentAgreement.xsl"></xsf:mainpane>
            //    </xsf:view>
            // <xsf:views>
            // ==============================================================================================

            List<Model.View> viewList = new List<Model.View>();
            XNamespace ns = LinqHelper.GetNamespace(this.manifestDoc, "xsf");
            XElement viewsElement = this.manifestDoc.Elements().First().Element(ns + "views");
            if (viewsElement == null)
            {
                return viewList;
            }
            
            XAttribute defaultAttr = viewsElement.Attribute("default");
            if (defaultAttr == null)
            {
                return viewList;
            }
                
            string defalutViewDisplayName = defaultAttr.Value;
            if (string.IsNullOrWhiteSpace(defalutViewDisplayName))
            {
                return viewList;
            }

            IEnumerable<XElement> allViewElements = viewsElement.Elements();
            foreach (XElement viewElement in allViewElements)
            {
                // Retrieve the display name
                XAttribute displayNameAttr = viewElement.Attribute("name");
                if (displayNameAttr == null)
                {
                    continue;
                }

                string viewDisplayName = displayNameAttr.Value;

                XElement mainpaneElement = viewElement.Element(ns + "mainpane");
                if (mainpaneElement == null)
                {
                    continue;
                }

                XAttribute transform = mainpaneElement.Attribute("transform");
                if (transform == null)
                {
                    continue;
                }

                string viewInternalName = transform.Value;
                if (string.IsNullOrWhiteSpace(viewInternalName))
                {
                    continue;
                }

                // Create a View object to represent the view display name nad internal name.
                Model.View view = new Model.View();
                view.DisplayName = viewDisplayName;
                view.InternalFileName = viewInternalName;
                if (viewDisplayName.Equals(defalutViewDisplayName))
                {
                    view.IsDefaultView = true;
                }
                else
                {
                    view.IsDefaultView = false;
                }

                viewList.Add(view);
            }

            return viewList;
        }

        #endregion

        #endregion
    }
}
