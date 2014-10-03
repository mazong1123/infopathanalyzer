using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GeekBangCN.InfoPathAnalyzer.Model
{
    public class ObjectModelBase
    {
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
