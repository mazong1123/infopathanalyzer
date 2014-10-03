//-----------------------------------------------------------------------
// <copyright file="NativeMethods.cs" company="GeekBangCN">
//     GeekBangCN. All rights reserved.
// </copyright>
// <author>Jim Ma</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using mshtml;

namespace GeekBangCN.InfoPathAnalyzer.Utility
{
    public static class NativeMethods
    {
        #region Public Fields

        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
        public delegate bool EnumChildProc(IntPtr hWnd, ref IntPtr lParam);
        public const Int32 SMTO_ABORTIFHUNG = 2;

        #endregion

        #region Public Methods

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        public static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32", EntryPoint = "RegisterWindowMessageA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern Int32 RegisterWindowMessage(string lpString);

        [DllImport("user32", EntryPoint = "SendMessageTimeoutA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern Int32 SendMessageTimeout(IntPtr hWnd, Int32 msg, Int32 wParam, Int32 lParam, Int32 fuFlags, Int32 uTimeout, ref Int32 lpdwResult);

        [DllImport("oleacc", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern Int32 ObjectFromLresult(Int32 lResult, ref Guid riid, Int32 wParam, ref IHTMLDocument2 ppvObject);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern Int32 EnumChildWindows(IntPtr hWndParent, EnumChildProc lpEnumFunc, ref IntPtr lParam);

        [DllImport("user32", EntryPoint = "GetClassNameA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int GetClassName(IntPtr handleToWindow, StringBuilder className, int maxClassNameLength);

        #endregion
    }
}
