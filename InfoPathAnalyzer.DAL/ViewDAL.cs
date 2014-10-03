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
    public class ViewDAL : DALBase, IViewDAL
    {
        #region Constructors

        public ViewDAL(Stream xsnFileContents)
        {
            this.XsnFileStream = xsnFileContents;
        }

        public ViewDAL(string xsnFilePath)
        {
            using (StreamReader sr = new StreamReader(xsnFilePath))
            {
                this.XsnFileStream = this.CloneStream(sr.BaseStream);
            }
        }

        #endregion

        #region Public Methods

        public IList<View> GetViews(bool isGetContent)
        {
            IList<View> views = this.GetViewsWithoutContent();

            if (isGetContent)
            {
                this.GetViewContent(views);
            }

            return views;
        }

        public View GetDefaultView(bool isGetContent)
        {
            Stream manifestFileStream = this.GetManifestFileStream();
            ManifestManager manifestManager = new ManifestManager(manifestFileStream);

            View defaultView = manifestManager.GetDefaultViewName();

            if (isGetContent)
            {
                this.GetViewContent(defaultView);
            }

            return defaultView;
        }

        public View GetViewByInternalFileName(string internalFileName, bool isGetContent)
        {
            IList<View> views = this.GetViewsWithoutContent();

            var query = from c in views
                        where c.InternalFileName.Equals(internalFileName, StringComparison.InvariantCultureIgnoreCase)
                        select c;

            if (query == null || query.Count() <= 0)
            {
                return null;
            }

            View view = query.First();

            if (isGetContent)
            {
                this.GetViewContent(view);
            }

            return view;
        }

        public Stream GetTemplateFileStream()
        {
            using (XsnManager xsnManager = new XsnManager(this.XsnFileStream))
            {
                Stream templateFileStream = xsnManager.GetInnerFileStream("template.xml");

                return templateFileStream;
            }
        }

        #endregion

        #region Private Methods

        private void GetViewContent(IList<View> views)
        {
            using (XsnManager xsnManager = new XsnManager(this.XsnFileStream))
            {
                foreach (View view in views)
                {
                    Stream content = xsnManager.GetInnerFileStream(view.InternalFileName);
                    MemoryStream ms = new MemoryStream();
                    content.CopyTo(ms);
                    ms.Position = 0;
                    view.Content = ms;
                }
            }
        }

        private void GetViewContent(View view)
        {
            using (XsnManager xsnManager = new XsnManager(this.XsnFileStream))
            {
                Stream content = xsnManager.GetInnerFileStream(view.InternalFileName);
                MemoryStream ms = new MemoryStream();
                content.CopyTo(ms);
                ms.Position = 0;
                view.Content = ms;
            }
        }

        private IList<View> GetViewsWithoutContent()
        {
            Stream manifestFileStream = this.GetManifestFileStream();
            ManifestManager manifestManager = new ManifestManager(manifestFileStream);

            List<View> views = new List<View>();

            views.AddRange(manifestManager.GetViewNames());

            return views;
        }

        #endregion
    }
}
