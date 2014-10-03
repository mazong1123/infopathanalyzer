using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GeekBangCN.InfoPathAnalyzer.UC.DataManager;

namespace GeekBangCN.InfoPathAnalyzer.UC.Control
{
    public partial class PreviewControl : PreviewControlDataSeparatedBase
    {
        public PreviewControl()
        {
            InitializeComponent();
        }

        public void Initialize(Stream xsnFileStream)
        {
            this.DataManager = new PreviewControlDataManager();
            this.DataManager.PreviewWebBrowser = this.previewWebBrowser;
            this.DataManager.ViewBLL = new GeekBangCN.InfoPathAnalyzer.BLL.ViewBLL(xsnFileStream);
        }

        public void SetView(string viewInternalFileName, string controlID)
        {
            this.DataManager.SetPreviewContent(viewInternalFileName, controlID);
        }

        public void ClearView()
        {
            this.DataManager.ClearPreviewContent();
        }
    }
}
