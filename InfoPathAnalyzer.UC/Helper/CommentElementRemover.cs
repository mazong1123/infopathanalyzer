using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeekBangCN.InfoPathAnalyzer.UC.Helper
{
    public class CommentElementRemover : IViewXSLProcessor
    {
        #region IViewXSLProcessor Members

        public string Process(string viewXSLContent)
        {
            // Find '<comment/>' element
            Int32 commentNodeIndex = viewXSLContent.IndexOf("<comment/>", 0, StringComparison.OrdinalIgnoreCase);

            // Remove all '<comment/>'
            while (commentNodeIndex != -1)
            {
                viewXSLContent = viewXSLContent.Remove(commentNodeIndex, 10);
                commentNodeIndex = viewXSLContent.IndexOf("<comment/>", StringComparison.OrdinalIgnoreCase);
            }

            return viewXSLContent;
        }

        #endregion
    }
}
