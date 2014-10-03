using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GeekBangCN.InfoPathAnalyzer.Utility
{
    public static class XsdParserFactory
    {
        private static MemoryStream infoPathXsnFileStream;

        public static XsdParser CreateInfoPathXsdParser(Stream xsdContent, Stream xsnFileStream)
        {
            if (infoPathXsnFileStream != null)
            {
                infoPathXsnFileStream.Close();
            }
            
            infoPathXsnFileStream = new MemoryStream();
            xsnFileStream.CopyTo(infoPathXsnFileStream);
            infoPathXsnFileStream.Position = 0;
            XsdParser xsdParser = new XsdParser(xsdContent);
            xsdParser.ReadExternalSchema(GetInnerFileContentFromXsn);

            return xsdParser;
        }

        private static Stream GetInnerFileContentFromXsn(string fileName)
        {
            using (XsnManager xsnManger = new XsnManager(infoPathXsnFileStream))
            {
                Stream innerFileStream = xsnManger.GetInnerFileStream(fileName);

                MemoryStream ms = new MemoryStream();
                innerFileStream.CopyTo(ms);
                ms.Position = 0;

                return ms;
            }
        }
    }
}
