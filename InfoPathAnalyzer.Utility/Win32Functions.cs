//-----------------------------------------------------------------------
// <copyright file="Win32Functions.cs" company="GeekBangCN">
//     GeekBangCN. All rights reserved.
// </copyright>
// <author>Jim Ma</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using mshtml;

namespace GeekBangCN.InfoPathAnalyzer.Utility
{
    public static class Win32Functions
    {
        public static IHTMLDocument2 IEDOMFromhWnd(IntPtr hWnd)
        {
            Guid IID_IHTMLDocument2 = new Guid("626FC520-A41E-11CF-A731-00A0C9082637");

            Int32 lRes = 0;
            Int32 lMsg;
            Int32 hr;

            // Register the message
            lMsg = NativeMethods.RegisterWindowMessage("WM_HTML_GETOBJECT");

            // Get the object
            NativeMethods.SendMessageTimeout(hWnd, lMsg, 0, 0, NativeMethods.SMTO_ABORTIFHUNG, 1000, ref lRes);
            if (lRes != 0)
            {
                // Get the object from lRes
                IHTMLDocument2 ieDOMFromhWnd = null;
                hr = NativeMethods.ObjectFromLresult(lRes, ref IID_IHTMLDocument2, 0, ref ieDOMFromhWnd);
                if (hr != 0)
                {
                    throw new COMException("ObjectFromLresult has thrown an exception", hr);
                }
                return ieDOMFromhWnd;
            }
            
            return null;
        }

        public static bool EnumWindows(NativeMethods.EnumWindowsProc lpEnumFunc, IntPtr lParam)
        {
            return NativeMethods.EnumWindows(lpEnumFunc, lParam);
        }

        public static IntPtr FindIEWindowHandler(IntPtr parentHwnd)
        {
            IntPtr hwndIPIE = IntPtr.Zero;
            string className = string.Empty;
            while (!className.Equals("Internet Explorer_Server"))
            {
                NativeMethods.EnumChildWindows(parentHwnd, EnumChildWindowsProc, ref hwndIPIE);

                // If no child window, break from the loop. It indicates failed to find "Internet Explorer_Server" window.
                if ((hwndIPIE == IntPtr.Zero) || (hwndIPIE == parentHwnd))
                {
                    break;
                }

                // Get the class name of child window.
                className = Win32Functions.GetClassName(hwndIPIE);

                // The child window becomes parent window in the next loop.
                parentHwnd = hwndIPIE;
            }

            return hwndIPIE;
        }

        public static IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow)
        {
            return NativeMethods.FindWindowEx(hwndParent, hwndChildAfter, lpszClass, lpszWindow);
        }

        public static int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount)
        {
            return NativeMethods.GetWindowText(hWnd, lpString, nMaxCount);
        }

        public static string GetClassName(IntPtr hwnd)
        {
            StringBuilder className = new StringBuilder(255);

            Int32 lRes = NativeMethods.GetClassName(hwnd, className, className.MaxCapacity);
            if (lRes == 0)
            {
                return String.Empty;
            }

            return className.ToString();
        }

        public static bool CompareClassNames(IntPtr hWnd, string expectedClassName)
        {
            if (hWnd == IntPtr.Zero)
                return false;

            string className = GetClassName(hWnd);

            return className.Equals(expectedClassName);
        }

        public static IntPtr GetChildWindowHwnd(IntPtr parentHwnd, string className, NativeMethods.EnumChildProc enumMethod)
        {
            IntPtr hWnd = IntPtr.Zero;
            // enumChildWindowClassName = className;

            // Go throught the child windows of the dialog window
            NativeMethods.EnumChildWindows(parentHwnd, enumMethod, ref hWnd);

            // If a logon dialog window is found hWnd will be set.
            return hWnd;
        }

        private static bool EnumChildWindowsProc(IntPtr hWnd, ref IntPtr lParam)
        {
            // Get the handler of the first child window is enough for this tool.
            lParam = hWnd;

            return true;
        }
    }
}
