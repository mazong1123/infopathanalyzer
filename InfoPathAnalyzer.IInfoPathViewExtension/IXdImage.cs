using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeekBangCN.InfoPathAnalyzer.IInfoPathViewExtension
{
    public interface IXdImage
    {
        System.IO.Stream XsnContent
        {
            get;
            set;
        }

        string getImageUrl(string imageBase64Binary);
    }
}
