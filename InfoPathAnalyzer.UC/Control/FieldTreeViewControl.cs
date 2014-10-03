using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GeekBangCN.InfoPathAnalyzer.UC.DataManager;

namespace GeekBangCN.InfoPathAnalyzer.UC.Control
{
    public partial class FieldTreeViewControl : FieldTreeViewControlDataSeparatedBase
    {
        public event EventHandler<TreeViewEventArgs> OnTreeViewAfterSelect;

        public FieldTreeViewControl()
        {
            InitializeComponent();
        }

        public void Initialize(Stream xsnFileStream)
        {
            this.DataManager = new FieldTreeViewControlDataManager();
            this.DataManager.TreeViewControl = this.internalFieldTreeView;
            this.DataManager.DataSourceBLL = new GeekBangCN.InfoPathAnalyzer.BLL.DataSourceBLL(xsnFileStream);
            this.DataManager.ElementBLL = new GeekBangCN.InfoPathAnalyzer.BLL.ElementBLL(xsnFileStream);
            this.AcceptButton = this.btnSearch;
            
            this.DataManager.Clear();
            this.lblSearchResult.Text = string.Empty;
            this.tbSearch.Text = string.Empty;
            this.cbIgnoreCase.Checked = true;
            this.cbMatchExact.Checked = false;
            this.btnSearch.Enabled = false;
        }

        public void PopulateFieldTreeViewData()
        {
            this.DataManager.PopulateTreeViewData();
            this.btnSearch.Enabled = true;
        }

        public void FindTreeViewNode(Model.Element element)
        {
            this.DataManager.FindTreeViewNode(element);
        }

        public void ClearNodeSelection()
        {
            this.DataManager.ClearNodeSelection();
        }

        public void Clear()
        {
            this.DataManager.Clear();
        }

        private void OnSearchButtonClick(object sender, EventArgs e)
        {
            this.FindFieldTreeViewNode();
            this.lblSearchResult.Text = this.DataManager.GenerateFoundResultText();
            this.UpdateUI();
        }

        private void OnPrevButtonClick(object sender, EventArgs e)
        {
            this.DataManager.SelectPrevFoundNode();
            this.UpdateUI();
        }

        private void OnNextButtonClick(object sender, EventArgs e)
        {
            this.DataManager.SelectNextFoundNode();
            this.UpdateUI();
        }

        private void OnSearchTextBoxTextChanged(object sender, EventArgs e)
        {
            // TODO: Add on-fly search functionality.
        }

        private void OnInternalFieldTreeViewAfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.OnTreeViewAfterSelect != null)
            {
                this.DataManager.GetInnerElementBoundControl(e.Node);
                this.OnTreeViewAfterSelect(sender, e);
            }
        }

        private void FindFieldTreeViewNode()
        {
            bool isIgnoreCase = this.cbIgnoreCase.Checked;
            bool isMatchExact = this.cbMatchExact.Checked;
            this.DataManager.FindTreeViewNode(this.tbSearch.Text, isIgnoreCase, isMatchExact);
        }

        private void UpdateUI()
        {
            this.btnPrev.Enabled = !this.DataManager.IsCurrentFirstFoundNode();
            this.btnNext.Enabled = !this.DataManager.IsCurrentLastFoundNode();
        }
    }
}
