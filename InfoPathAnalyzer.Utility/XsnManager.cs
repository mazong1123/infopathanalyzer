using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.ObjectModel;

namespace GeekBangCN.InfoPathAnalyzer.Utility
{
    /// <summary>
    /// Provide the ability to manipulate .xsn file.
    /// Provide the functionality to extract .xsn file and represents the paths of extracted files.
    /// </summary>
    public class XsnManager : IDisposable
    {
        #region Private Fields

        private Microsoft.Deployment.Compression.Cab.CabEngine cabEngine;
        private StreamReader xsnFileStreamReader;

        #endregion

        #region Constructor

        /// <summary>
        /// Initialize an instance of XSNManager class.
        /// <param name="xsnFile">Specifies a path of xsn file.</param>
        /// </summary>
        public XsnManager(string xsnFile)
        {
            this.xsnFileStreamReader = new StreamReader(xsnFile);
            cabEngine = new Microsoft.Deployment.Compression.Cab.CabEngine();
        }

        /// <summary>
        /// Initialize an instance of XSNManager class.
        /// </summary>
        /// <param name="xsnFileStream">Specifies a file stream of xsn file.</param>
        public XsnManager(Stream xsnFileStream)
        {
            MemoryStream ms = new MemoryStream();
            xsnFileStream.CopyTo(ms);
            xsnFileStream.Position = 0;
            this.xsnFileStreamReader = new StreamReader(ms);
            cabEngine = new Microsoft.Deployment.Compression.Cab.CabEngine();
        }

        #endregion

        #region Public Methods

        #region Common Methods

        public IDictionary<string, string> GetAllInnerFileContents(Func<string, bool> DoesIncludeFile)
        {
            IList<string> fileList = this.GetInnerFileList();
            Dictionary<string, string> fileContentsDict = new Dictionary<string, string>();

            foreach (string fileName in fileList)
            {
                if (DoesIncludeFile(fileName))
                {
                    string fileContents = this.GetInnerFileContents(fileName);

                    if (!fileContentsDict.ContainsKey(fileName))
                    {
                        fileContentsDict.Add(fileName, fileContents);
                    }
                }
            }

            return fileContentsDict;
        }

        public IDictionary<string, Stream> GetAllInnerFileStream(Func<string, bool> DoesIncludeFile)
        {
            IList<string> fileList = this.GetInnerFileList();
            Dictionary<string, Stream> fileContentsDict = new Dictionary<string, Stream>();

            foreach (string fileName in fileList)
            {
                if (DoesIncludeFile(fileName))
                {
                    Stream fileStream = this.GetInnerFileStream(fileName);

                    if (!fileContentsDict.ContainsKey(fileName))
                    {
                        fileContentsDict.Add(fileName, fileStream);
                    }
                }
            }

            return fileContentsDict;
        }

        public IList<string> GetInnerFileList()
        {
            IList<string> fileList = cabEngine.GetFiles(this.xsnFileStreamReader.BaseStream);

            return fileList;
        }

        public string GetInnerFileContents(string fileName)
        {
            Stream fileStream = this.GetFileStream(fileName);

            using (StreamReader sr = new StreamReader(fileStream))
            {
                string fileContents = sr.ReadToEnd();

                return fileContents;
            }
        }

        public Stream GetInnerFileStream(string fileName)
        {
            return this.GetFileStream(fileName);
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            try
            {
                if (this.cabEngine != null)
                {
                    this.cabEngine.Dispose();
                }

                this.xsnFileStreamReader.Close();
            }
            catch
            {
            }
        }

        #endregion

        #endregion

        #region Private Methods

        private Stream GetFileStream(string fileName)
        {
            // Extract file to Stream object.
            Stream fileStream = cabEngine.Unpack(this.xsnFileStreamReader.BaseStream, fileName);

            return fileStream;
        }

        #endregion
    }
}
