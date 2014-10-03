//-----------------------------------------------------------------------
// <copyright file="InfoPath2007HtmlDesignerService.cs" company="GeekBangCN">
//     GeekBangCN. All rights reserved.
// </copyright>
// <author>Jim Ma</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeekBangCN.InfoPathAnalyzer.Utility;
using mshtml;

namespace GeekBangCN.InfoPathAnalyzer.InfoPathUnderlying.InfoPathSurface
{
    /// <summary>
    /// Read/wirte html to InfoPath desginer or filler of InfoPath 2007.
    /// </summary>
    public class InfoPath2007HtmlDesignerService : IInfoPathHtmlDesignerService
    {
        #region Private Fields

        /// <summary>
        /// InfoPath handler.
        /// </summary>
        private IntPtr infoPathWnd = IntPtr.Zero;

        /// <summary>
        /// Caption text of InfoPath desginer or filler.
        /// </summary>
        private String windowCaption;

        #endregion

        #region Public Properties

        public virtual string InfoPathApplicationName
        {
            get
            {
                return "Microsoft Office InfoPath";
            }
        }

        public virtual string InfoPathDesignModeFlag
        {
            get
            {
                return "(Design)";
            }
        }

        public virtual string InfoPathPreviewModeFlag
        {
            get
            {
                return "(Preview)";
            }
        }

        #endregion

        #region Public Methods

        public IntPtr SearchWindowByCaption(string windowCaption)
        {
            this.windowCaption = windowCaption;
            Win32Functions.EnumWindows(new NativeMethods.EnumWindowsProc(EvalWindow), IntPtr.Zero);

            return this.infoPathWnd;
        }

        public string ExtractHtmlFromInfoPath(IntPtr hwnd)
        {
            String htmlText = String.Empty;

            IHTMLDocument2 htmlDoc = extractHTMLDocObj(hwnd);
            if (htmlDoc != null)
            {
                // Get the html text.
                if (htmlDoc.body != null)
                {
                    htmlText = htmlDoc.body.parentElement.outerHTML;
                }
            }

            return htmlText;
        }

        public string ExtractBodyHtmlFromInfoPath(IntPtr hwnd)
        {
            String htmlText = String.Empty;

            IHTMLDocument2 htmlDoc = extractHTMLDocObj(hwnd);
            if (htmlDoc != null)
            {
                // Get the html text.
                if (htmlDoc.body != null)
                {
                    htmlText = htmlDoc.body.innerHTML;
                }
            }

            return htmlText;
        }

        public void SetBodyHtmlToInfoPath(IntPtr hwnd, string htmlText)
        {
            // Extract html document object.
            IHTMLDocument2 htmlDoc = extractHTMLDocObj(hwnd);

            if (htmlDoc != null)
            {
                // Assign the new html text to the body.
                htmlDoc.body.innerHTML = htmlText;
            }
        }

        #endregion

        #region Protected Methods

        protected virtual IHTMLDocument2 extractHTMLDocObj(IntPtr hwnd)
        {
            // Find the "XDocs HTMLSurface" window inside the InfoPath main window.
            IntPtr hwndXDocs = Win32Functions.FindWindowEx(hwnd, IntPtr.Zero, "XDocs HTMLSurface", default(string));

            // Try to find "Internet Explorer_Server" winodw inside "XDocs HTMLSurface" window.
            IntPtr hwndIPIE = Win32Functions.FindIEWindowHandler(hwndXDocs);

            // If found the IE window inside the InfoPath window, extract html text and write to the specified location.
            if (hwndIPIE != IntPtr.Zero)
            {
                IHTMLDocument2 htmlDoc = null;
                try
                {
                    htmlDoc = Win32Functions.IEDOMFromhWnd(hwndIPIE);
                    return htmlDoc;
                }
                catch
                {
                    return null;
                }
            }

            return null;
        }

        #endregion

        #region Private Methods

        private bool EvalWindow(IntPtr hwnd, IntPtr lParam)
        {
            StringBuilder sb = new StringBuilder(256);

            // Get the caption text of the window.
            Win32Functions.GetWindowText(hwnd, sb, 256);

            // If find the specific window, store the window handler.
            if (sb.ToString().Equals(this.windowCaption))
            {
                this.infoPathWnd = hwnd;
            }

            return true;
        }

        #endregion
    }
}
