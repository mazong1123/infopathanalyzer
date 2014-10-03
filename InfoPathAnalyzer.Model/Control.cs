using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeekBangCN.InfoPathAnalyzer.Model
{
    public class Control : ObjectModelBase
    {
        private bool isAlreadyGotAssociatedElement= false;

        public string Label
        {
            get;
            set;
        }

        public string ID
        {
            get;
            set;
        }

        public View ParentView
        {
            get;
            set;
        }

        public Element AssociatedElement
        {
            get;
            set;
        }

        public bool IsAlreadyGotAssociatedElement
        {
            get
            {
                return this.isAlreadyGotAssociatedElement;
            }
            set
            {
                this.isAlreadyGotAssociatedElement = value;
            }
        }
    }
}
