//--------------------------------------------------------------------------------
// <copyright file="ElementControlMapControl.cs" company="GeekBangCN">
//     GeekBangCN. All rights reserved.
// </copyright>
// <author>Jim Ma</author>
//--------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GeekBangCN.InfoPathAnalyzer.UC.DataManager;

namespace GeekBangCN.InfoPathAnalyzer.UC.Control
{
    public partial class ElementControlMapControl : ElementControlMapControlDataSeparatedBase
    {
        public event EventHandler<UIDataModel.ElementControlMapListViewData> OnListItemSelectChanged;

        public ElementControlMapControl()
        {
            InitializeComponent();
        }

        public void Initialize()
        {
            this.elementControlMapListView.SelectedIndexChanged -= new EventHandler(OnElementControlMapListViewSelectedIndexChanged);
            
            this.DataManager = new ElementControlMapControlDataManager();
            this.DataManager.ElementControlMapListView = this.elementControlMapListView;
            this.DataManager.ElementControlMapListViewContextMenu = this.ElementControlMapListViewContextMenu;
            this.DataManager.InitializeElementControlMapListView();
            this.DataManager.ClearElementControlMapListViewItems();

            this.elementControlMapListView.SelectedIndexChanged += new EventHandler(OnElementControlMapListViewSelectedIndexChanged);
        }

        private void OnElementControlMapListViewSelectedIndexChanged(object sender, EventArgs e)
        {
            if (OnListItemSelectChanged != null)
            {
                if (this.elementControlMapListView.SelectedItems.Count <= 0)
                {
                    this.OnListItemSelectChanged(sender, null);
                }
                else
                {
                    ListViewItem selectedItem = this.elementControlMapListView.SelectedItems[0];
                    UIDataModel.ElementControlMapListViewData mapViewData = selectedItem.Tag as UIDataModel.ElementControlMapListViewData;

                    this.OnListItemSelectChanged(sender, mapViewData);
                }
            }
        }

        public void AddElementControlItems(IList<UIDataModel.ElementControlMapListViewData> viewDataList)
        {
            this.AddElementControlItems(viewDataList, -1); // -1 means no need to find the specific item to select.
        }

        public void AddElementControlItems(IList<UIDataModel.ElementControlMapListViewData> viewDataList, int defalutSelectIndex)
        {
            foreach (UIDataModel.ElementControlMapListViewData viewData in viewDataList)
            {
                this.DataManager.AddElementToElementControlMapListView(viewData);
            }

            if (defalutSelectIndex != -1)
            {
                UIDataModel.ElementControlMapListViewData selectedViewData = viewDataList[defalutSelectIndex];
                ListView.ListViewItemCollection listViewItems = this.DataManager.ElementControlMapListView.Items;
                foreach (ListViewItem listViewItem in listViewItems)
                {
                    string viewFileName = listViewItem.SubItems[7].Text;
                    string controlID = listViewItem.SubItems[8].Text;
                    if (viewFileName.Equals(selectedViewData.ViewFileName, StringComparison.InvariantCultureIgnoreCase)
                        && controlID.Equals(selectedViewData.ControlID, StringComparison.InvariantCultureIgnoreCase))
                    {
                        defalutSelectIndex = listViewItem.Index;

                        break;
                    }
                }
            }
            else
            {
                defalutSelectIndex = 0;
            }
            
            this.DataManager.SelectListViewItem(defalutSelectIndex);
        }

        public void ClearElementControlMapListViewItems()
        {
            this.DataManager.ClearElementControlMapListViewItems();
        }

        private void OnListViewCopyMenuItemClick(object sender, EventArgs e)
        {
            this.DataManager.HandleElementControlMapListViewCopyMenuItemClickEvent(sender, e);
        }

        private void OnListViewCopyTheWholeRowMenuItemClick(object sender, EventArgs e)
        {
            this.DataManager.HandleElementControlMapListViewCopyTheWholeRowMenuItemClickEvent(sender, e);
        }

        private void OnListViewMouseClick(object sender, MouseEventArgs e)
        {
            this.DataManager.HandleElementControlMapListViewMouseClickEvent(sender, e);
        }
    }
}
