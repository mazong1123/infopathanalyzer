using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GeekBangCN.InfoPathAnalyzer.DAL;
using GeekBangCN.InfoPathAnalyzer.IBLL;
using GeekBangCN.InfoPathAnalyzer.IDAL;
using GeekBangCN.InfoPathAnalyzer.Model;
using GeekBangCN.InfoPathAnalyzer.Utility;
using GeekBangCN.InfoPathAnalyzer.Utility.View;

namespace GeekBangCN.InfoPathAnalyzer.BLL
{
    public class ViewBLL : BLLBaseWithDAL<IViewDAL>, IViewBLL
    {
        #region Constructors

        public ViewBLL()
        {
        }

        public ViewBLL(Stream xsnFileContents)
        {
            this.DataAccessLayer = new ViewDAL(xsnFileContents);
        }

        public ViewBLL(string xsnFilePath)
        {
            this.DataAccessLayer = new ViewDAL(xsnFilePath);
        }

        #endregion

        #region Public Methods

        public IList<View> GetViews(bool isGetContent)
        {
            this.ValidateDataAccessLayer();

            return this.DataAccessLayer.GetViews(isGetContent);
        }

        public View GetViewByInternalFileName(string internalFileName, bool isGetContent)
        {
            this.ValidateDataAccessLayer();

            return this.DataAccessLayer.GetViewByInternalFileName(internalFileName, isGetContent);
        }

        public View GetDefaultView(bool isGetContent)
        {
            this.ValidateDataAccessLayer();

            return this.DataAccessLayer.GetDefaultView(isGetContent);
        }

        public Stream GetTemplateFileStream()
        {
            this.ValidateDataAccessLayer();

            return this.DataAccessLayer.GetTemplateFileStream();
        }

        public void GetControls(View view)
        {
            if (view.Content == null)
            {
                throw new ArgumentException("The view does not have any content.");
            }

            view.Controls.Clear();
            view.IsAlreadyGotControls = false;

            // Create a ViewManager object to parse view data.
            using (ViewManager viewManager = new ViewManager(view.Content))
            {
                // Find all control IDs.
                IList<string> controlIDList = viewManager.FindAllControlIDs();
                foreach (string controlID in controlIDList)
                {
                    Control control = this.CreateControl(controlID, view);
                    view.Controls.Add(control);
                }

                view.IsAlreadyGotControls = true;
            }
        }

        #endregion

        #region Private Methods

        private Control CreateControl(string controlID, View parentView)
        {
            Control control = new Control();
            control.ID = controlID;
            control.ParentView = parentView;
            control.Label = string.Empty; // TODO: TBD

            return control;
        }

        #endregion
    }
}
