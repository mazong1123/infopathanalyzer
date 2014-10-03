//-----------------------------------------------------------------------
// <copyright file="IInfoPathHtmlDesignerService.cs" company="GeekBangCN">
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
    /// <summary>
    /// Defines methods to read/write html from/to InfoPath.
    /// </summary>
    public interface IInfoPathHtmlDesignerService
    {
        string InfoPathApplicationName
        {
            get;
        }

        string InfoPathDesignModeFlag
        {
            get;
        }

        string InfoPathPreviewModeFlag
        {
            get;
        }

        IntPtr SearchWindowByCaption(String windowCaption);
        String ExtractHtmlFromInfoPath(IntPtr hwnd);
        String ExtractBodyHtmlFromInfoPath(IntPtr hwnd);
        void SetBodyHtmlToInfoPath(IntPtr hwnd, String htmlText);
    }
}
