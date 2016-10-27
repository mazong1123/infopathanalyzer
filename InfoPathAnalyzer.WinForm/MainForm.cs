using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GeekBangCN.InfoPathAnalyzer.UC.Helper;
using GeekBangCN.InfoPathAnalyzer.UC.UIDataModel;

namespace GeekBangCN.InfoPathAnalyzer.WinForm
{
    public partial class MainForm : Form
    {
        private MemoryStream xsnFileStream;
        private Model.Control controlTreeViewSelectedControl;

        public MainForm()
        {
            InitializeComponent();
        }

        private void Initialize(string infoPathTemplateFilePath)
        {
            ProgressWindow progress = new ProgressWindow();
            progress.Text = "Initializing...";

            this.elementControlMapControl.OnListItemSelectChanged -= new EventHandler<ElementControlMapListViewData>(OnElementControlMapControlListItemSelectChanged);
            this.fieldTreeViewControl.OnTreeViewAfterSelect -= new EventHandler<TreeViewEventArgs>(OnFieldTreeViewControlAfterSelect);
            this.controlTreeViewControl.OnTreeViewAfterSelect -= new EventHandler<TreeViewEventArgs>(OnControlTreeViewControlAfterSelect);

            System.Threading.ThreadPool.QueueUserWorkItem(delegate(object status)
            {
                IProgressCallback callback = status as IProgressCallback;
                try
                {
                    callback.Begin(0, 100);

                    using (StreamReader sr = new StreamReader(infoPathTemplateFilePath))
                    {
                        callback.SetText("Reading template file...");
                        this.xsnFileStream = new MemoryStream();
                        sr.BaseStream.CopyTo(this.xsnFileStream);
                        this.xsnFileStream.Position = 0;
                        callback.StepTo(25);

                        callback.SetText("Reading data sources...");

                        this.fieldTreeViewControl.Invoke(new Action(delegate()
                        {
                            this.fieldTreeViewControl.Initialize(this.xsnFileStream);
                            this.fieldTreeViewControl.PopulateFieldTreeViewData();
                        }));

                        callback.StepTo(50);

                        callback.SetText("Reading views...");
                        this.controlTreeViewControl.Invoke(new Action(delegate()
                        {
                            this.controlTreeViewControl.Initialize(this.xsnFileStream);
                            this.controlTreeViewControl.PopulateControlTreeViewData();
                        }));
                        callback.StepTo(75);

                        callback.SetText("Initializing UI...");
                        this.previewControl.Initialize(this.xsnFileStream);
                    }

                    this.elementControlMapControl.Invoke(new Action(delegate()
                    {
                        this.elementControlMapControl.Initialize();
                    }));

                    this.elementControlMapControl.OnListItemSelectChanged += new EventHandler<ElementControlMapListViewData>(OnElementControlMapControlListItemSelectChanged);
                    this.fieldTreeViewControl.OnTreeViewAfterSelect += new EventHandler<TreeViewEventArgs>(OnFieldTreeViewControlAfterSelect);
                    this.controlTreeViewControl.OnTreeViewAfterSelect += new EventHandler<TreeViewEventArgs>(OnControlTreeViewControlAfterSelect);

                    callback.StepTo(100);
                }
                catch (System.Threading.ThreadAbortException)
                {
                }
                catch (System.Threading.ThreadInterruptedException)
                {
                }
                catch (IOException)
                {
                    MessageBox.Show("Failed to open the InfoPath template.", "IO Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception)
                {
                    MessageBox.Show("Unknow error occured. Please restart InfoPath Analyzer", "Unknown Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (callback != null)
                    {
                        callback.End();
                    }
                }
            }, progress);

            progress.ShowDialog();
        }

        private void OnElementControlMapControlListItemSelectChanged(object sender, ElementControlMapListViewData e)
        {
            this.previewControl.ClearView();

            if (e == null)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(e.ViewFileName))
            {
                return;
            }

            this.previewControl.SetView(e.ViewFileName, e.ControlID);
        }

        private void OnControlTreeViewControlAfterSelect(object sender, TreeViewEventArgs e)
        {
            ProgressWindow progress = new ProgressWindow();
            progress.Text = "Generating...";
            System.Threading.ThreadPool.QueueUserWorkItem(delegate(object status)
            {
                IProgressCallback callback = status as IProgressCallback;
                try
                {
                    callback.Begin(0, 100);
                    callback.SetText("Generating control data...");

                    this.Invoke(new Action(delegate()
                    {
                        this.ClearData();
                    }));

                    this.controlTreeViewSelectedControl = null;
                    callback.StepTo(25);

                    Model.Control control = TransferObjectBuilder.GetTagObjectFromTreeNode<Model.Control>(e.Node);
                    if (control == null)
                    {
                        this.Invoke(new Action(delegate()
                        {
                            this.fieldTreeViewControl.ClearNodeSelection();
                        }));
                        
                        return;
                    }
                    callback.StepTo(50);

                    // If the control has associated field element, select the field in the field tree view.
                    if (control.AssociatedElement != null)
                    {
                        this.Invoke(new Action(delegate()
                        {
                            this.elementControlMapControl.ClearElementControlMapListViewItems();
                            this.controlTreeViewSelectedControl = control;
                            this.fieldTreeViewControl.ClearNodeSelection();
                            this.fieldTreeViewControl.FindTreeViewNode(control.AssociatedElement);
                        }));

                        return;
                    }
                    callback.StepTo(75);

                    // If this is an unbound control, generate the map view data.
                    IList<ElementControlMapListViewData> mapViewDataList = TransferObjectBuilder.BuildElementControlMapListViewDataFromUnboundControlTreeNode(e.Node);

                    this.Invoke(new Action(delegate()
                    {
                        this.UpdateElementControlMapControlData(mapViewDataList);
                        this.fieldTreeViewControl.ClearNodeSelection();
                    }));
                    callback.StepTo(100);
                }
                catch (System.Threading.ThreadAbortException)
                {
                }
                catch (System.Threading.ThreadInterruptedException)
                {
                }
                finally
                {
                    if (callback != null)
                    {
                        callback.End();
                    }
                }
            }, progress);

            progress.ShowDialog();
        }

        private void OnFieldTreeViewControlAfterSelect(object sender, TreeViewEventArgs e)
        {
            ProgressWindow progress = new ProgressWindow();
            progress.Text = "Generating...";
            System.Threading.ThreadPool.QueueUserWorkItem(delegate(object status)
            {
                IProgressCallback callback = status as IProgressCallback;
                try
                {
                    callback.Begin(0, 100);
                    callback.SetText("Generating field data...");

                    this.Invoke(new Action(delegate()
                    {
                        this.ClearData();
                    }));
                    
                    IList<ElementControlMapListViewData> mapViewDataList = TransferObjectBuilder.BuildElementControlMapListViewDataFromElementTreeNode(e.Node);
                    callback.StepTo(50);

                    if (this.controlTreeViewSelectedControl == null)
                    {
                        this.Invoke(new Action(delegate()
                        {
                            this.UpdateElementControlMapControlData(mapViewDataList);
                        }));
                        callback.StepTo(100);
                    }
                    else // If there's a control selected in ControlTreeView, we need to select the corresponding items according to the control.
                    {
                        int defaultIndex = 0;
                        for (int i = 0; i < mapViewDataList.Count; i++)
                        {
                            ElementControlMapListViewData viewData = mapViewDataList[i];
                            if (viewData.ControlID.Equals(this.controlTreeViewSelectedControl.ID, StringComparison.InvariantCultureIgnoreCase)
                                && viewData.ViewFileName.Equals(this.controlTreeViewSelectedControl.ParentView.InternalFileName, StringComparison.InvariantCultureIgnoreCase))
                            {
                                defaultIndex = i;

                                break;
                            }
                        }
                        callback.StepTo(75);

                        this.Invoke(new Action(delegate()
                        {
                            this.UpdateElementControlMapControlData(mapViewDataList, defaultIndex);
                            this.controlTreeViewSelectedControl = null;
                        }));
                        callback.StepTo(100);
                    }
                }
                catch (System.Threading.ThreadAbortException)
                {
                }
                catch (System.Threading.ThreadInterruptedException)
                {
                }
                finally
                {
                    if (callback != null)
                    {
                        callback.End();
                    }
                }
            }, progress);

            progress.ShowDialog();
        }

        private void UpdateElementControlMapControlData(IList<ElementControlMapListViewData> mapViewDataList)
        {
            this.elementControlMapControl.AddElementControlItems(mapViewDataList);
        }

        private void UpdateElementControlMapControlData(IList<ElementControlMapListViewData> mapViewDataList, int defaultSelectIndex)
        {
            this.elementControlMapControl.AddElementControlItems(mapViewDataList, defaultSelectIndex);
        }

        private void ClearData()
        {
            this.elementControlMapControl.ClearElementControlMapListViewItems();
            this.previewControl.ClearView();
        }

        private void OnOpenMainMenuToolStripMenuItemClick(object sender, EventArgs e)
        {
            OpenFileDialog fileDlg = new OpenFileDialog();
            fileDlg.Filter = "InfoPath Template (*.xsn)|*.xsn";
            fileDlg.Multiselect = false;

            if (DialogResult.OK == fileDlg.ShowDialog())
            {
                string filePath = fileDlg.FileName;
                this.Initialize(filePath);
            }
        }

        private void OnExitMainMenuToolStripMenuItemClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OnAboutToolStripMenuItemClick(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

        private void OnHelpDocumentToolStripMenuItemClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/mazong1123/infopathanalyzer");
        }
    }
}
