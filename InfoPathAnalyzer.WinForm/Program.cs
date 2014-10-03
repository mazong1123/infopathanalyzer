using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace GeekBangCN.InfoPathAnalyzer.WinForm
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string infoPathAnalyzerAppDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\GeekBangCN\\InfoPath Analyzer";
            if (!Directory.Exists(infoPathAnalyzerAppDataFolder))
            {
                Directory.CreateDirectory(infoPathAnalyzerAppDataFolder);
            }

            Application.Run(new MainForm(false));
        }
    }
}
