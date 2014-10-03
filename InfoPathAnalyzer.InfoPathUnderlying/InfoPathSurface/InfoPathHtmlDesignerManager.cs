//-----------------------------------------------------------------------
// <copyright file="InfoPathHtmlDesignerManager.cs" company="GeekBangCN">
//     GeekBangCN. All rights reserved.
// </copyright>
// <author>Jim Ma</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using GeekBangCN.InfoPathAnalyzer.Utility;

namespace GeekBangCN.InfoPathAnalyzer.InfoPathUnderlying.InfoPathSurface
{
    public class InfoPathHtmlDesignerManager
    {
        #region Private Fields

        /// <summary>
        /// Instance of InfoPath html designer service.
        /// </summary>
        private IInfoPathHtmlDesignerService designerService = null;

        #endregion

        #region Constructor

        /// <summary>
        /// Initialize an instance of InfoPathHtmlDesignerManager class.
        /// </summary>
        public InfoPathHtmlDesignerManager()
        {
        }

        /// <summary>
        /// Initialize an instance of InfoPathHtmlDesignerManager class.
        /// </summary>
        /// <param name="designerService">An InfoPath html desinger service.</param>
        public InfoPathHtmlDesignerManager(IInfoPathHtmlDesignerService designerService)
        {
            this.designerService = designerService;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the html designer service.
        /// <value>
        /// The html designer service is used to access underlying html of InfoPath. It must be specified before calling any 
        /// read/write method.
        /// </value>
        /// </summary>
        public IInfoPathHtmlDesignerService DesignerService
        {
            get
            {
                return this.designerService;
            }

            set
            {
                this.designerService = value;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Finda all opened InfoPath windows.
        /// </summary>
        /// <returns>A collection represents the text of the opened InfoPath window.</returns>
        public ReadOnlyCollection<InfoPathWindow> FindAllInfoPathWindow()
        {
            List<InfoPathWindow> ipWindowList = new List<InfoPathWindow>();
            Win32Functions.EnumWindows(new NativeMethods.EnumWindowsProc(delegate(IntPtr hwnd, IntPtr lParam)
                {
                    StringBuilder sb = new StringBuilder(256);

                    // Get the caption text of the window.
                    Win32Functions.GetWindowText(hwnd, sb, 256);
                    String ipWinCaption = sb.ToString();

                    // If find the specific window, store the window text.
                    if (ipWinCaption.Contains(designerService.InfoPathApplicationName) && (ipWinCaption.Contains(designerService.InfoPathDesignModeFlag) || (ipWinCaption.Contains(designerService.InfoPathPreviewModeFlag))))
                    {
                        String handlerValue = hwnd.ToInt32().ToString();
                        InfoPathWindow ipWin = new InfoPathWindow()
                        {
                            HandlerValue = handlerValue,
                            Caption = sb.ToString()
                        };

                        ipWindowList.Add(ipWin);
                    }

                    return true;
                }), IntPtr.Zero);

            return ipWindowList.AsReadOnly();
        }

        /// <summary>
        /// Extract html text from InfoPath.
        /// </summary>
        /// <param name="windowCaption">Specifies a caption text of the InfoPath desginer/filler you want to extract html text from.</param>
        /// <returns>A string represents html text from InfoPath</returns>
        public String ExtractHtmlFromInfoPath(String windowCaption)
        {
            this.ValidateHtmlService();

            IntPtr hwnd = designerService.SearchWindowByCaption(windowCaption);

            return this.ExtractHtmlFromInfoPath(hwnd);
        }

        /// <summary>
        /// Extract html text from InfoPath.
        /// </summary>
        /// <param name="hwnd">Specifies a window handler of the InfoPath desginer/filler you want to extract html text from.</param>
        /// <returns>A string represents html text from InfoPath</returns>
        public String ExtractHtmlFromInfoPath(IntPtr hwnd)
        {
            this.ValidateHtmlService();

            if (hwnd == IntPtr.Zero)
            {
                throw new NullReferenceException("Failed to find the window by the specified window caption text");
            }

            String htmlText = this.designerService.ExtractHtmlFromInfoPath(hwnd);

            return htmlText; 
        }

        /// <summary>
        /// Extract body text from InfoPath html.
        /// </summary>
        /// <param name="hwnd">Specifies a window handler of the InfoPath desginer/filler you want to extract html text from.</param>
        /// <returns>A string represents html text from InfoPath</returns>
        public String ExtractBodyHtmlFromInfoPath(IntPtr hwnd)
        {
            this.ValidateHtmlService();

            if (hwnd == IntPtr.Zero)
            {
                throw new NullReferenceException("Failed to find the window by the specified window caption text");
            }

            String htmlText = this.designerService.ExtractBodyHtmlFromInfoPath(hwnd);

            return htmlText;
        }

        /// <summary>
        /// Extract body text from InfoPath html.
        /// </summary>
        /// <param name="windowCaption">Specifies a caption text of the InfoPath desginer/filler you want to extract html text from.</param>
        /// <returns>A string represents html text from InfoPath</returns>
        public String ExtractBodyHtmlFromInfoPath(String windowCaption)
        {
            this.ValidateHtmlService();

            IntPtr hwnd = this.designerService.SearchWindowByCaption(windowCaption);

            return this.ExtractBodyHtmlFromInfoPath(hwnd);
        }

        /// <summary>
        /// Set html text to the body of InfoPath html.
        /// </summary>
        /// <param name="windowCaption">Specifies a caption text of the InfoPath desginer/filler you want to extract html text from.</param>
        /// <param name="htmlText">Specifies a string represents the html text you set to InfoPath.</param>
        public void SetBodyHtmlToInfoPath(String windowCaption, String htmlText)
        {
            this.ValidateHtmlService();

            IntPtr hwnd = this.designerService.SearchWindowByCaption(windowCaption);
            this.SetBodyHtmlToInfoPath(hwnd, htmlText);
        }

        /// <summary>
        /// Set html text to the body of InfoPath html.
        /// </summary>
        /// <param name="hwnd">Specifies a window handler of the InfoPath desginer/filler you want to extract html text from.</param>
        /// <param name="htmlText">Specifies a string represents the html text you set to InfoPath.</param>
        public void SetBodyHtmlToInfoPath(IntPtr hwnd, String htmlText)
        {
            this.ValidateHtmlService();

            if (hwnd == IntPtr.Zero)
            {
                throw new NullReferenceException("Failed to find the window by the specified window caption text");
            }

            this.designerService.SetBodyHtmlToInfoPath(hwnd, htmlText);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Validate html designer service to see if it's null.
        /// </summary>
        private void ValidateHtmlService()
        {
            if (this.designerService == null)
            {
                throw new MemberAccessException("Service cannot be null. Please assign an IInfoPathHtmlService before call this method");
            }
        }

        #endregion
    }

    /// <summary>
    /// Represents the information of an InfoPath window.
    /// </summary>
    public class InfoPathWindow
    {
        /// <summary>
        /// Gets or sets an InfoPath window caption.
        /// </summary>
        public String Caption
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets an InfoPath window handler value.
        /// </summary>
        public String HandlerValue
        {
            get;
            set;
        }

        /// <summary>
        /// Return a text combines the window caption and the window handler value.
        /// </summary>
        /// <returns>A string represents the window caption and the window handler value.</returns>
        public override string ToString()
        {
            return String.Format("{0} - {1}", this.Caption, this.HandlerValue);
        }
    }
}
