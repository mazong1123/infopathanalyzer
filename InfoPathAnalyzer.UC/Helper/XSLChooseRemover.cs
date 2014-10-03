using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeekBangCN.InfoPathAnalyzer.UC.Helper
{
    /// <summary>
    /// Remove all 'xsl:choose' elements.
    /// </summary>
    public class XSLChooseRemover : IViewXSLProcessor
    {
        #region IViewXSLProcessor Members

        public string Process(string viewXSLContent)
        {
            // Find <xsl:choose> element.
            Int32 xslWhenXDocumentIndex = viewXSLContent.IndexOf("<xsl:choose", StringComparison.OrdinalIgnoreCase);
            
            // Remove all <xsl:choose> elements.
            while (xslWhenXDocumentIndex != -1)
            {
                Int32 xslEndWhenXDocumentIndex = viewXSLContent.IndexOf("</xsl:choose>", xslWhenXDocumentIndex, StringComparison.OrdinalIgnoreCase);

                string chooseContent = viewXSLContent.Substring(xslWhenXDocumentIndex, xslEndWhenXDocumentIndex + 12 - xslWhenXDocumentIndex + 1);
                if (chooseContent.Contains("xd:CtrlId")) // Don't remove the choose content if there're controls inside it.
                {
                    xslWhenXDocumentIndex = viewXSLContent.IndexOf("<xsl:choose", xslWhenXDocumentIndex + 11, StringComparison.OrdinalIgnoreCase);
                    continue;
                }

                viewXSLContent = viewXSLContent.Remove(xslWhenXDocumentIndex, xslEndWhenXDocumentIndex + 12 - xslWhenXDocumentIndex + 1);

                xslWhenXDocumentIndex = viewXSLContent.IndexOf("<xsl:choose", xslWhenXDocumentIndex, StringComparison.OrdinalIgnoreCase);
            }

            return viewXSLContent;
        }

        #endregion
    }
}
