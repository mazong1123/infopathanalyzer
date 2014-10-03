using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using GeekBangCN.InfoPathAnalyzer.Utility;

namespace GeekBangCN.InfoPathAnalyzer.DAL
{
    public abstract class DALBase
    {
        private Stream xsnFileStream;

        public Stream XsnFileStream
        {
            get
            {
                // Clone a Stream and return.
                return this.CloneStream(this.xsnFileStream);
            }
            protected set
            {
                this.xsnFileStream = value;
            }
        }

        protected Stream GetManifestFileStream()
        {
            using (XsnManager xsnManager = new XsnManager(this.XsnFileStream))
            {
                Stream manifestFileStream = xsnManager.GetInnerFileStream("manifest.xsf");

                return manifestFileStream;
            }
        }

        protected Stream CloneStream(Stream inputStream)
        {
            if (inputStream == null)
            {
                return inputStream;
            }

            MemoryStream ms = new MemoryStream();
            inputStream.CopyTo(ms);
            inputStream.Position = 0;
            ms.Position = 0;

            return ms;
        }
    }
}
