using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GeekBangCN.InfoPathAnalyzer.WinForm
{
    public partial class AboutForm : Form
    {
        public AboutForm(bool isTrial)
        {
            InitializeComponent();
            if (isTrial)
            {
                this.lblVersion.Text += " (Trial Version)";
            }
        }

        private void OnHomePageLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://mazong1123.wordpress.com/2011/08/18/infopath-analyzer-1-0-overview/");
            }
            catch
            {
            }
        }

        private void OnSupportLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("mailto:support@geekbangcn.com");
            }
            catch
            {
            }
        }

        private void OnOKButtonClick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
