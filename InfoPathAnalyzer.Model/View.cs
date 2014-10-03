using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GeekBangCN.InfoPathAnalyzer.Model
{
    public class View : ObjectModelBase
    {
        private List<Control> controls = new List<Control>();
        private bool isAlreadyGotControls = false;
        private Stream content;

        public string DisplayName
        {
            get;
            set;
        }

        public string InternalFileName
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

        public IList<Control> Controls
        {
            get
            {
                return this.controls;
            }
        }

        public bool IsDefaultView
        {
            get;
            set;
        }

        public bool IsAlreadyGotControls
        {
            get
            {
                return this.isAlreadyGotControls;
            }
            set
            {
                this.isAlreadyGotControls = value;
            }
        }
    }
}
