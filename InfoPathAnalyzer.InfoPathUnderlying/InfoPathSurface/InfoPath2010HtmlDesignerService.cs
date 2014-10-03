//-----------------------------------------------------------------------
// <copyright file="InfoPath2010HtmlDesignerService.cs" company="GeekBangCN">
//     GeekBangCN. All rights reserved.
// </copyright>
// <author>Jim Ma</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeekBangCN.InfoPathAnalyzer.InfoPathUnderlying.InfoPathSurface
{
    public class InfoPath2010HtmlDesignerService : InfoPath2007HtmlDesignerService
    {
        public override string InfoPathApplicationName
        {
            get
            {
                return "Microsoft InfoPath";
            }
        }
    }
}
