using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeekBangCN.InfoPathAnalyzer.Model;

namespace GeekBangCN.InfoPathAnalyzer.IBLL
{
    public interface IViewBLL
    {
        IList<View> GetViews(bool isGetContent);
        View GetViewByInternalFileName(string internalFileName, bool isGetContent);
        View GetDefaultView(bool isGetContent);
        System.IO.Stream GetTemplateFileStream();
        void GetControls(View view);
    }
}
