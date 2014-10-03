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
    public partial class ControlTreeViewControl : ControlTreeViewControlDataSeparatedBase
    {
        public event EventHandler<TreeViewEventArgs> OnTreeViewAfterSelect;

        public ControlTreeViewControl()
        {
            InitializeComponent();
        }

        public void Initialize(Stream xsnFileStream)
        {
            this.DataManager = new ControlTreeViewControlDataManager();
            this.DataManager.TreeViewControl = this.internalControlTreeView;
            this.DataManager.ControlBLL = new GeekBangCN.InfoPathAnalyzer.BLL.ControlBLL(xsnFileStream);
            this.DataManager.ViewBLL = new GeekBangCN.InfoPathAnalyzer.BLL.ViewBLL(xsnFileStream);
            this.AcceptButton = this.btnSearch;

            this.DataManager.Clear();
            this.lblSearchResult.Text = string.Empty;
            this.tbSearch.Text = string.Empty;
            this.cbIgnoreCase.Checked = true;
            this.cbMatchExact.Checked = false;
            this.btnSearch.Enabled = false;
        }

        public void PopulateControlTreeViewData()
        {
            this.DataManager.PopulateTreeViewData();
            this.btnSearch.Enabled = true;
        }

        public void FindTreeViewNode(Model.Control control)
        {
            this.DataManager.FindTreeViewNode(control);
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
            this.FindControlTreeViewNode();
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

        private void OnInternalControlTreeViewAfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.OnTreeViewAfterSelect != null)
            {
                this.DataManager.GetInnerControlAssociatedElement(e.Node);
                this.OnTreeViewAfterSelect(sender, e);
            }
        }

        private void FindControlTreeViewNode()
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
