using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeekBangCN.InfoPathAnalyzer.Model;

namespace GeekBangCN.InfoPathAnalyzer.IDAL
{
    public interface IViewDAL
    {
        IList<View> GetViews(bool isGetContent);
        View GetViewByInternalFileName(string internalFileName, bool isGetContent);
        View GetDefaultView(bool isGetContent);
        System.IO.Stream GetTemplateFileStream();
    }
}
