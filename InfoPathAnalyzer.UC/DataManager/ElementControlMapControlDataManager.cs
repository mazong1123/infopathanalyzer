//--------------------------------------------------------------------------------
// <copyright file="ElementControlMapControlDataManager.cs" company="GeekBangCN">
//     GeekBangCN. All rights reserved.
// </copyright>
// <author>Jim Ma</author>
//--------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GeekBangCN.Common.Extensions;

namespace GeekBangCN.InfoPathAnalyzer.UC.DataManager
{
    public class ElementControlMapControlDataManager : DataManagerBase
    {
        public ListView ElementControlMapListView
        {
            get;
            set;
        }

        public ContextMenuStrip ElementControlMapListViewContextMenu
        {
            get;
            set;
        }

        public string CurrentListViewSelectedItemText
        {
            get;
            set;
        }

        public string CurrentListViewSelectedRowText
        {
            get;
            set;
        }

        public void InitializeElementControlMapListView()
        {
            this.ValidateElementControlMapListView();

            this.ElementControlMapListView.GridLines = true;
            this.ElementControlMapListView.View = System.Windows.Forms.View.Details;
            this.ElementControlMapListView.FullRowSelect = true;
            this.ElementControlMapListView.Sorting = SortOrder.Ascending;
            this.ElementControlMapListView.ShowItemToolTips = true;

            this.ElementControlMapListView.Columns.Clear();

            this.ElementControlMapListView.Columns.Add("Data Source").Width = 80;
            this.ElementControlMapListView.Columns.Add("XPath").Width = 255;
            this.ElementControlMapListView.Columns.Add("Element Name").Width = 90;
            this.ElementControlMapListView.Columns.Add("Element Namespace").Width = 120;
            this.ElementControlMapListView.Columns.Add("Element Type").Width = 90;
            this.ElementControlMapListView.Columns.Add("Element Data Type").Width = 180;
            this.ElementControlMapListView.Columns.Add("View Name").Width = 80;
            this.ElementControlMapListView.Columns.Add("View File Name").Width = 150;
            this.ElementControlMapListView.Columns.Add("Control ID").Width = 90;
        }

        public void ClearElementControlMapListViewItems()
        {
            this.ValidateElementControlMapListView();

            this.ElementControlMapListView.Items.Clear();
        }

        public void AddElementToElementControlMapListView(UIDataModel.ElementControlMapListViewData listViewData)
        {
            ListViewItem listViewItem = new ListViewItem(this.ProcessListViewText(listViewData.DataSourceName));
            listViewItem.SubItems.Add(this.ProcessListViewText(listViewData.ElementXPath));
            listViewItem.SubItems.Add(this.ProcessListViewText(listViewData.ElementNameWithPrefix));
            listViewItem.SubItems.Add(this.ProcessListViewText(listViewData.ElementNamespace));
            listViewItem.SubItems.Add(this.ProcessListViewText(listViewData.ElementType));
            listViewItem.SubItems.Add(this.ProcessListViewText(listViewData.ElementDataType));
            listViewItem.SubItems.Add(this.ProcessListViewText(listViewData.ViewName));
            listViewItem.SubItems.Add(this.ProcessListViewText(listViewData.ViewFileName));
            listViewItem.SubItems.Add(this.ProcessListViewText(listViewData.ControlID));
            listViewItem.Tag = listViewData;

            this.ElementControlMapListView.Items.Insert(0, listViewItem);
        }

        public void HandleElementControlMapListViewCopyMenuItemClickEvent(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.CurrentListViewSelectedItemText))
            {
                Clipboard.SetText(this.CurrentListViewSelectedItemText);
            }
        }

        public void HandleElementControlMapListViewCopyTheWholeRowMenuItemClickEvent(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.CurrentListViewSelectedRowText))
            {
                Clipboard.SetText(this.CurrentListViewSelectedRowText);
            }
        }

        public void HandleElementControlMapListViewMouseClickEvent(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.ValidateElementControlMapListViewContextMenu();
                this.ValidateElementControlMapListView();

                this.CurrentListViewSelectedItemText = string.Empty;
                this.CurrentListViewSelectedRowText = string.Empty;

                this.ElementControlMapListViewContextMenu.Show(this.ElementControlMapListView, e.Location);

                ListViewHitTestInfo hitTestInfo = this.ElementControlMapListView.HitTest(e.Location);

                // Get the single item text.
                if (hitTestInfo.SubItem != null)
                {
                    this.CurrentListViewSelectedItemText = hitTestInfo.SubItem.Text;
                }

                // Get the whole row text.
                if (hitTestInfo.Item != null)
                {
                    foreach (ListViewItem.ListViewSubItem subItem in hitTestInfo.Item.SubItems)
                    {
                        this.CurrentListViewSelectedRowText += subItem.Text + "\t";
                    }
                }
            }
        }

        public void SelectListViewItem(int index)
        {
            if (index < 0)
            {
                return;
            }

            if (index >= this.ElementControlMapListView.Items.Count)
            {
                return;
            }

            this.ElementControlMapListView.Items[index].Selected = true;
        }

        private string ProcessListViewText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return "N/A";
            }

            return text;
        }

        private void ValidateElementControlMapListView()
        {
            if (this.ElementControlMapListView == null)
            {
                throw new MemberAccessException("ElementControlMapListView cannot be null.");
            }
        }

        private void ValidateElementControlMapListViewContextMenu()
        {
            if (this.ElementControlMapListViewContextMenu == null)
            {
                throw new MemberAccessException("ElementControlMapListViewContextMenu cannot be null.");
            }
        }
    }
}
