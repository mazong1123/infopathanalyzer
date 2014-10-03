//-----------------------------------------------------------------------
// <copyright file="BodyTextBox.cs" company="GeekBangCN">
//     GeekBangCN. All rights reserved.
// </copyright>
// <author>Jim Ma</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GeekBangCN.InfoPathAnalyzer.UC.Control.HtmlEditor
{
    /// <summary>
    /// Specilized text box for InfoPath editor.
    /// </summary>
    public partial class BodyTextBox : TextBox
    {
        /// <summary>
        /// When the text box is ready to save.
        /// </summary>
        public event EventHandler OnSave = null;

        /// <summary>
        /// Initialize a BodyTextBox class.
        /// </summary>
        public BodyTextBox()
        {
            InitializeComponent();
            this.MaxLength = Int32.MaxValue;
        }

        /// <summary>
        /// Raises the System.Windows.Forms.Control.KeyDown event.
        /// </summary>
        /// <param name="e">A System.Windows.Forms.KeyEventArgs that contains the event data.</param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            // If pressed Ctrl + A, select all text.
            if (e.Control && e.KeyCode == Keys.A)
            {
                this.SelectAll();
            }

            // If the BodyTextBox.OnSave event is subscribed, notify the subscriber.
            if (OnSave != null)
            {
                // if pressed Ctrl + S, raise OnSave event.
                if (e.Control && e.KeyCode == Keys.S)
                {
                    OnSave(this, new EventArgs());
                }
            }
        }
    }
}
