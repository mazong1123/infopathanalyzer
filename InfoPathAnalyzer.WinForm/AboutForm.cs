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
        public AboutForm()
        {
            InitializeComponent();
        }

        private void OnHomePageLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://www.mazong1123.com/");
            }
            catch
            {
            }
        }

        private void OnSupportLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("mailto:mazong1123@gmail.com");
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
