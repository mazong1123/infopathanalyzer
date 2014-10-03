using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeekBangCN.InfoPathAnalyzer.IInfoPathViewExtension;

namespace GeekBangCN.InfoPathAnalyzer.InfoPathAnalyzerViewExtension
{
    public class XdImage : IInfoPathViewExtension.IXdImage
    {
        // It will be implemented in future.
        public System.IO.Stream XsnContent
        {
            get;
            set;
        }

        public string getImageUrl(string imageBase64Binary)
        {
            // It will be implemented in future.
            return imageBase64Binary;
        }
    }
}
