using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeekBangCN.InfoPathAnalyzer.Model
{
    public class Element : ObjectModelBase
    {
        private List<Control> boundControls = new List<Control>();
        private List<Element> children = new List<Element>();
        private bool isAlreadyGotBoundControls = false;
        private bool isAlreadyGotChildElements = false;

        public string Name
        {
            get;
            set;
        }

        public string NameWithNamespacePrefix
        {
            get;
            set;
        }

        public string NamespaceUri
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }

        public bool IsRepeating
        {
            get;
            set;
        }

        public string DataType
        {
            get;
            set;
        }

        public DataSource DataSource
        {
            get;
            set;
        }

        public IList<Control> BoundControls
        {
            get
            {
                return this.boundControls;
            }
        }

        public IList<Element> Children
        {
            get
            {
                return this.children;
            }
        }

        public bool IsAreadyGotBoundControls
        {
            get
            {
                return this.isAlreadyGotBoundControls;
            }
            set
            {
                this.isAlreadyGotBoundControls = value;
            }
        }

        public bool IsAlreadyGotChildElements
        {
            get
            {
                return this.isAlreadyGotChildElements;
            }
            set
            {
                this.isAlreadyGotChildElements = value;
            }
        }
    }
}
