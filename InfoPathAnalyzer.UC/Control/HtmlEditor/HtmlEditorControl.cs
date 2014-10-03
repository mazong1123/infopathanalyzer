//-----------------------------------------------------------------------
// <copyright file="HtmlEditorControl.cs" company="GeekBangCN">
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
using GeekBangCN.InfoPathAnalyzer.InfoPathUnderlying.InfoPathSurface;

namespace GeekBangCN.InfoPathAnalyzer.UC.Control.HtmlEditor
{
    public partial class HtmlEditorControl : UserControl
    {
        private InfoPathHtmlDesignerManager infoPathHtmlDesingerManager;

        public HtmlEditorControl()
        {
            InitializeComponent();
            initializeMainForm();
        }

        private void initializeMainForm()
        {
            // Create an InfoPath html manager.
            this.infoPathHtmlDesingerManager = new InfoPathHtmlDesignerManager();

            // Select a default InfoPath html service.
            this.infoPathHtmlDesingerManager.DesignerService = new InfoPath2010HtmlDesignerService();

            // Initialize window select dropdown list.
            InitializeInfoPathWindowDDL();

            // Subscribe the OnSave event.
            this.tbBodyHtml.OnSave += new EventHandler(OnBodyHtmlSave);
        }

        private void InitializeInfoPathWindowDDL()
        {
            ddlInfoPathWindow.Items.Clear();

            // Find all InfoPath window.
            System.Collections.ObjectModel.ReadOnlyCollection<InfoPathWindow> ipWindowList = infoPathHtmlDesingerManager.FindAllInfoPathWindow();

            // Add InfoPath windows to the InfoPath window dropdown list.
            foreach (InfoPathWindow ipWin in ipWindowList)
            {
                ddlInfoPathWindow.Items.Add(ipWin);
            }

            // Set the default item.
            if (ddlInfoPathWindow.Items.Count > 0)
            {
                ddlInfoPathWindow.SelectedIndex = 0;
            }
        }

        private String GetInfoPathWindowCaption()
        {
            string ipWinCaption = string.Empty;

            // Get the selected window.
            object selectedItem = ddlInfoPathWindow.SelectedItem;
            if (selectedItem != null)
            {
                InfoPathWindow ipWin = selectedItem as InfoPathWindow;
                if (ipWin != null)
                {
                    ipWinCaption = ipWin.Caption;
                }
            }

            return ipWinCaption;
        }

        private void LoadInfoPathHtml()
        {
            string selectedWindowCaption = GetInfoPathWindowCaption();
            if (!string.IsNullOrEmpty(selectedWindowCaption))
            {
                string bodyHtml = infoPathHtmlDesingerManager.ExtractBodyHtmlFromInfoPath(selectedWindowCaption);
                tbBodyHtml.EditorText = bodyHtml;
            }
            else
            {
                MessageBox.Show("Cannot find the selected window", "Failed to load InfoPath Html");
            }
        }

        private void SaveInfoPathHtml()
        {
            // Set the html text to the selected InfoPath window.
            String selectedWindowCaption = GetInfoPathWindowCaption();
            if (!String.IsNullOrEmpty(selectedWindowCaption))
            {
                String bodyHtml = tbBodyHtml.EditorText;
                infoPathHtmlDesingerManager.SetBodyHtmlToInfoPath(selectedWindowCaption, bodyHtml);
            }
            else
            {
                MessageBox.Show("Cannot find the selected window", "Failed to save InfoPath Html");
            }
        }

        private void OnLoadIPWindowButtonClick(object sender, EventArgs e)
        {
            LoadInfoPathHtml();
        }

        private void OnSaveBodyHtmlButtonClick(object sender, EventArgs e)
        {
            SaveInfoPathHtml();
        }

        private void OnRefreshIPWindowDropdownListButtonClick(object sender, EventArgs e)
        {
            InitializeInfoPathWindowDDL();
        }

        private void OnBodyHtmlSave(object sender, EventArgs e)
        {
            SaveInfoPathHtml();
        }

        private void OnInfoPath2007ToolStripMenuItemClick(object sender, EventArgs e)
        {
            infoPath2007ToolStripMenuItem.Checked = true;
            infoPath2010ToolStripMenuItem.Checked = false;
            infoPathHtmlDesingerManager.DesignerService = new InfoPath2007HtmlDesignerService();
            InitializeInfoPathWindowDDL();
            this.tbBodyHtml.EditorText = string.Empty;
            this.lblCurrentCompatibility.Text = "Current Compatibility: InfoPath 2007";
        }

        private void OnInfoPath2010ToolStripMenuItemClick(object sender, EventArgs e)
        {
            infoPath2007ToolStripMenuItem.Checked = false;
            infoPath2010ToolStripMenuItem.Checked = true;
            infoPathHtmlDesingerManager.DesignerService = new InfoPath2010HtmlDesignerService();
            InitializeInfoPathWindowDDL();
            this.tbBodyHtml.EditorText = string.Empty;
            this.lblCurrentCompatibility.Text = "Current Compatibility: InfoPath 2010";
        }

    }
}
