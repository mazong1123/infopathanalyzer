using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeekBangCN.InfoPathAnalyzer.Model;

namespace GeekBangCN.InfoPathAnalyzer.IBLL
{
    public interface IDataSourceBLL
    {
        IList<DataSource> GetDataSources(bool isGetContent);
        IList<DataSource> GetMainDataSources(bool isGetContent);
        void GetElements(DataSource dataSource);
    }
}
