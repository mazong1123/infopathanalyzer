//-----------------------------------------------------------------------
// <copyright file="DataSeparatedPage.cs" company="GeekBangCN">
//     GeekBangCN. All rights reserved.
// </copyright>
// <author>Jim Ma</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeekBangCN.Common.Extensions.Web
{
    /// <summary>
    /// A Page can separate its data from representation.
    /// </summary>
    /// <typeparam name="T">Specifies a corresponding DataManager which contains the logic to manipulate the data of the page.</typeparam>
    public class DataSeparatedPage<T> : System.Web.UI.Page where T : DataManagerBase
    {
        /// <summary>
        /// Instance of a DataManager.
        /// </summary>
        private T dataManager;

        /// <summary>
        /// Gets or sets a DataManager. DataManager contains the logic to manipulate the data of the page.
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
    }
}
