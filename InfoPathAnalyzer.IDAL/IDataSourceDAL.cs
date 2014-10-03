using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeekBangCN.InfoPathAnalyzer.Model;

namespace GeekBangCN.InfoPathAnalyzer.IDAL
{
    public interface IDataSourceDAL
    {
        IList<DataSource> GetDataSources(bool isGetContent);
        IList<DataSource> GetMainDataSources(bool isGetContent);
    }
}
