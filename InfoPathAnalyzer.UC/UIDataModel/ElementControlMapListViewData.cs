using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GeekBangCN.InfoPathAnalyzer.UC.UIDataModel
{
    public class ElementControlMapListViewData : EventArgs
    {
        public string DataSourceName
        {
            get;
            set;
        }

        public string ElementXPath
        {
            get;
            set;
        }

        public string ElementNameWithPrefix
        {
            get;
            set;
        }

        public string ElementNamespace
        {
            get;
            set;
        }

        public string ElementType
        {
            get;
            set;
        }

        public string ElementDataType
        {
            get;
            set;
        }

        public string ViewName
        {
            get;
            set;
        }

        public string ViewFileName
        {
            get;
            set;
        }

        public string ControlID
        {
            get;
            set;
        }
    }
}
