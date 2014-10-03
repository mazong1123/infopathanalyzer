//-----------------------------------------------------------------------
// <copyright file="BodyEditor.cs" company="GeekBangCN">
//     GeekBangCN. All rights reserved.
// </copyright>
// <author>Jim Ma</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GeekBangCN.InfoPathAnalyzer.UC.Control.HtmlEditor
{
    /// <summary>
    /// A editor to edit the body html of InfoPath.
    /// </summary>
    public partial class BodyEditor : UserControl
    {
        /// <summary>
        /// When the InfoPath editor is ready to save.
        /// </summary>
        public event EventHandler OnSave = null;

        /// <summary>
        /// Initialize a BodyEditor class.
        /// </summary>
        public BodyEditor()
        {
            InitializeComponent();
            this.tbBodyEditor.OnSave += new EventHandler(OnBodyEditorSave);
        }

        /// <summary>
        /// Gets or sets the text of body editor.
        /// </summary>
        public String EditorText
        {
            get
            {
                return tbBodyEditor.Text;
            }
            set
            {
                tbBodyEditor.Text = value;
            }
        }

        /// <summary>
        /// BodyTextBox.OnSave event handler
        /// </summary>
        /// <param name="sender">The object raises this event.</param>
        /// <param name="e">A System.Windows.Forms.KeyEventArgs that contains the event data.</param>
        private void OnBodyEditorSave(object sender, EventArgs e)
        {
            // If BodyEditor.OnSave is subscribed, notify the subscriber.
            if (this.OnSave != null)
            {
                this.OnSave(this, new EventArgs());
            }
        }
    }
}
