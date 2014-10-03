using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeekBangCN.InfoPathAnalyzer.Model;

namespace GeekBangCN.InfoPathAnalyzer.IBLL
{
    public interface IElementBLL
    {
        void GetChildElements(Element element);
        void GetBoundControls(Element element);
    }
}
