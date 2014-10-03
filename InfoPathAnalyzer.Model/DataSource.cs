using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GeekBangCN.InfoPathAnalyzer.Model
{
    public class DataSource : ObjectModelBase
    {
        private List<Element> elementList = new List<Element>();
        private bool isAlreadyGotElements = false;
        private bool isMainDataSource = false;
        private Stream content;

        public string InternalFileName
        {
            get;
            set;
        }

        public string DisplayName
        {
            get;
            set;
        }

        public Stream Content
        {
            get
            {
                return this.CloneStream(this.content);
            }

            set
            {
                this.content = value;
            }
        }

        public IList<Element> Elements
        {
            get
            {
                return this.elementList;
            }
        }

        public bool IsAlreadyGotElements
        {
            get
            {
                return this.isAlreadyGotElements;
            }

            set
            {
                this.isAlreadyGotElements = value;
            }
        }

        public bool IsMainDataSource
        {
            get
            {
                return this.isMainDataSource;
            }

            set
            {
                this.isMainDataSource = value;
            }
        }
    }
}
