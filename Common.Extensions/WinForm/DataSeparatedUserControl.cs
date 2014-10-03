//-----------------------------------------------------------------------
// <copyright file="DataSeparatedUserControl.cs" company="GeekBangCN">
//     GeekBangCN. All rights reserved.
// </copyright>
// <author>Jim Ma</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeekBangCN.Common.Extensions.WinForm
{
    /// <summary>
    /// A UserControl can separate its data from representation.
    /// </summary>
    /// <typeparam name="T">Specifies a corresponding DataManager which contains the logic to manipulate the data of the user control.</typeparam>
    public class DataSeparatedUserControl<T> : System.Windows.Forms.UserControl where T : DataManagerBase
    {
        /// <summary>
        /// Key down message ID.
        /// </summary>
        private const int WM_KEYDOWN = 0x0100;

        /// <summary>
        /// Instance of a DataManager.
        /// </summary>
        private T dataManager;

        /// <summary>
        /// Gets or sets a DataManager. DataManager contains the logic to manipulate the data of the user control
        /// </summary>
        public T DataManager
        {
            get
            {
                return this.dataManager;
            }
            set
            {
                this.dataManager = value;
            }
        }

        /// <summary>
        /// Gets or sets the defalut accept button for the user control.
        /// </summary>
        public System.Windows.Forms.Button AcceptButton
        {
            get;
            set;
        }

        /// <summary>
        /// Trap the "Enter" key for AcceptButton.
        /// </summary>
        /// <param name="m">Windows message.</param>
        protected override bool ProcessKeyPreview(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == WM_KEYDOWN)
            {
                if (this.AcceptButton != null && m.WParam.ToInt32() == (int)ConsoleKey.Enter)
                {
                    this.AcceptButton.PerformClick();
                }
            }

            return base.ProcessKeyPreview(ref m);
        }
    }
}