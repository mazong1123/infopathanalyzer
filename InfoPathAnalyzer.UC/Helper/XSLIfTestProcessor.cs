using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeekBangCN.InfoPathAnalyzer.UC.Helper
{
    /// <summary>
    /// Process 'xsl:if test=' element.
    /// </summary>
    public class XSLIfTestProcessor : IViewXSLProcessor
    {
        #region IViewXSLProcessor Members

        public string Process(string viewXSLContent)
        {
            // Find "<xsl:if test=" element.
            Int32 xslIfStartIndex = viewXSLContent.IndexOf("<xsl:if test=", StringComparison.OrdinalIgnoreCase);
            while (xslIfStartIndex != -1)
            {
                // Find the value of 'test='.
                Int32 quoteStartIndex = xslIfStartIndex + 13;
                Int32 quoteEndIndex = viewXSLContent.IndexOf("\"", quoteStartIndex + 1);
                String subStringDel = viewXSLContent.Substring(quoteStartIndex, quoteEndIndex - quoteStartIndex + 1);
                
                if (canDelete(subStringDel))
                {
                    // Delete the value of 'test='
                    viewXSLContent = viewXSLContent.Remove(quoteStartIndex + 1, (quoteEndIndex - 1) - (quoteStartIndex + 1) + 1);
                    
                    // Because we wanna the inner contents of <xsl:if test=.../> element always display, 
                    // we must put a condition expression to make the 'test=' always equals TRUE>
                    viewXSLContent = viewXSLContent.Insert(quoteStartIndex + 1, "1=1");

                    // Because we deleted some characters, the quote end index is changed.
                    quoteEndIndex = quoteStartIndex + 4;
                }

                // Find next '<xsl:if '
                xslIfStartIndex = viewXSLContent.IndexOf("<xsl:if test=", quoteEndIndex, StringComparison.OrdinalIgnoreCase);
            }

            return viewXSLContent;
        }

        private Boolean canDelete(String subStringDel)
        {
            // If string doesn't contain "function-available", it can be deleted.
            if (!subStringDel.Contains("function-available"))
            {
                return true;
            }
            else if (subStringDel.Contains("xdXDocument:GetDOM"))
            {
                // If string contains "function-available" and it contains "xdXDocument:GetDOM", it can be deleted.
                return true;
            }
            else
            {
                // If string contains "function-available" but it doesn't contain "xdXDocument:GetDOM", it cannot be deleted.
                return false;
            }
            
        }

        #endregion
    }
}
