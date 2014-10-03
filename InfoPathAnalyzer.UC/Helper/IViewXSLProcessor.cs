using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeekBangCN.InfoPathAnalyzer.UC.Helper
{
    public interface IViewXSLProcessor
    {
        /// <summary>
        /// Process view xsl content.
        /// </summary>
        /// <param name="viewXSLContent">Content of view xsl file</param>
        /// <returns>Processed view xsl content.</returns>
        String Process(String viewXSLContent);
    }
}
