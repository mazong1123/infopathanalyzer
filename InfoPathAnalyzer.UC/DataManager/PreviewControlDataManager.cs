//-----------------------------------------------------------------------
// <copyright file="PreviewControlDataManager.cs" company="GeekBangCN">
//     GeekBangCN. All rights reserved.
// </copyright>
// <author>Jim Ma</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml.Xsl;
using GeekBangCN.Common.Extensions;
using GeekBangCN.InfoPathAnalyzer.IBLL;
using GeekBangCN.InfoPathAnalyzer.Model;
using GeekBangCN.InfoPathAnalyzer.Utility;
using GeekBangCN.InfoPathAnalyzer.Utility.View;

namespace GeekBangCN.InfoPathAnalyzer.UC.DataManager
{
    public class PreviewControlDataManager : DataManagerBase
    {
        #region Private Fields

        private Dictionary<string, string> cachedViewContent = new Dictionary<string,string>();
        private WebBrowser previewWebBrowser;
        private Dictionary<HtmlElement, string> elementStyles = new Dictionary<HtmlElement, string>();
        private HtmlElement currentFocusedElement = null;
        private readonly string highlightStyleText = "; background-color: Yellow;";
        private string currentControlID = string.Empty;
        private string currentViewInternalFileName = string.Empty;

        #endregion

        #region Public Properties

        public WebBrowser PreviewWebBrowser
        {
            get
            {
                return this.previewWebBrowser;
            }
            set
            {
                this.previewWebBrowser = value;
                this.previewWebBrowser.IsWebBrowserContextMenuEnabled = false;
                this.previewWebBrowser.WebBrowserShortcutsEnabled = false;
                this.previewWebBrowser.DocumentCompleted -= new WebBrowserDocumentCompletedEventHandler(OnPreviewWebBrowserDocumentCompleted);
                this.previewWebBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(OnPreviewWebBrowserDocumentCompleted);
                this.previewWebBrowser.DocumentText = string.Empty; // Initialize the web browser.
            }
        }

        public IViewBLL ViewBLL
        {
            get;
            set;
        }

        #endregion

        #region Public Methods

        public void SetPreviewContent(string viewInternalFileName, string controlID)
        {
            this.ValidatePreviewWebBrowser();

            this.currentControlID = controlID;
            this.currentViewInternalFileName = viewInternalFileName;

            // Try to get the view content from cache. If the cache does not exist, get the view content from
            // underlying InfoPath file and cache it.
            if (this.cachedViewContent.Keys.Contains(viewInternalFileName))
            {
                this.PreviewWebBrowser.DocumentText = this.cachedViewContent[viewInternalFileName];
            }
            else
            {
                string viewContentForPreview = this.GetViewContentForPreview(viewInternalFileName);
                this.PreviewWebBrowser.DocumentText = viewContentForPreview;

                // Cache the view content.
                this.cachedViewContent.Add(viewInternalFileName, viewContentForPreview);
            }
        }

        public void ClearPreviewContent()
        {
            this.ValidatePreviewWebBrowser();

            this.currentControlID = string.Empty;
            this.currentViewInternalFileName = string.Empty;

            if (!string.IsNullOrWhiteSpace(this.previewWebBrowser.DocumentText)
                && !this.previewWebBrowser.DocumentText.Equals("<HTML></HTML>\0", StringComparison.InvariantCultureIgnoreCase)
                )
            {
                this.previewWebBrowser.DocumentText = string.Empty;
            }
        }

        #endregion

        #region Private Methods

        private void FocusOnControlByControlID(string controlID)
        {
            HtmlElement bodyElement = this.previewWebBrowser.Document.Body;
            foreach (HtmlElement subElement in bodyElement.All)
            {
                // Check "xd:CtrlId" attribute.
                string controlIDAttribute = string.Empty;

                // I don't know why it's always throw an "value does not expect in range" exception.
                // I have to ignore this exception.
                try
                {
                    controlIDAttribute = subElement.GetAttribute("xd:CtrlId");
                }
                catch (ArgumentException)
                {
                }

                if (string.IsNullOrEmpty(controlIDAttribute))
                {
                    continue;
                }

                // Whether the binding element is what we're looking for.
                if (!controlIDAttribute.Equals(controlID, StringComparison.InvariantCultureIgnoreCase))
                {
                    continue;
                }

                // Focus on the html control.
                this.currentFocusedElement = subElement;
                subElement.Focus();

                break;
            }
        }

        private string GetBindingElementNameFromApplyTemplatesXslMethod(XElement element)
        {
            string bindingElementName = string.Empty;
            IEnumerable<XElement> children = element.Elements();

            foreach (XElement subElement in children)
            {
                if (subElement.Name.LocalName.Equals("apply-templates", StringComparison.InvariantCultureIgnoreCase))
                {
                    XAttribute selectAttribute = subElement.Attribute("select");
                    if (selectAttribute != null)
                    {
                        bindingElementName = selectAttribute.Value;
                        break;
                    }
                }
            }

            return bindingElementName;
        }

        private string GetViewContentForPreview(string viewInternalFileName)
        {
            this.ValidateViewBLL();

            Model.View view = this.ViewBLL.GetViewByInternalFileName(viewInternalFileName, true);

            Stream templateXmlStream = this.ViewBLL.GetTemplateFileStream();
            string viewContentForPreview = this.GenerateViewHTML(view.Content, templateXmlStream);

            return viewContentForPreview;
        }

        private string GenerateViewHTML(Stream viewContentStream, Stream templateXmlStream)
        {
            // Load the template.xml file.
            XPathDocument templateXmlDoc = new XPathDocument(templateXmlStream);

            // Special process for the InfoPath view.
            viewContentStream = this.ProcessViewContent(viewContentStream);

            // Load view content to XmlReader.
            XmlReader xmlReader = XmlReader.Create(viewContentStream);

            // Load the xsl.
            XslCompiledTransform xslTrans = new XslCompiledTransform();
            xslTrans.Load(xmlReader);

            // Get the InfoPath special namespaces as the xsl argument list.
            XsltArgumentList infoPathSpecialXslArgumentList = this.GetInfoPathSpecialXsltArgumentList();

            // Create the output stream.
            using (StringWriter writer = new StringWriter())
            {
                // Do the actual transform of Xml.
                xslTrans.Transform(templateXmlDoc, infoPathSpecialXslArgumentList, writer);
                writer.Close();

                return writer.ToString();
            }
        }

        private XsltArgumentList GetInfoPathSpecialXsltArgumentList()
        {
            // XdXDocument extension namespace.
            GeekBangCN.InfoPathAnalyzer.InfoPathAnalyzerViewExtension.XdXDocument xdDocumentExtension = new InfoPathAnalyzerViewExtension.XdXDocument();

            // XdImage extension namespace.
            GeekBangCN.InfoPathAnalyzer.InfoPathAnalyzerViewExtension.XdImage xdImageExtension = new InfoPathAnalyzerViewExtension.XdImage();

            XsltArgumentList argumentList = new XsltArgumentList();
            argumentList.AddExtensionObject("http://schemas.microsoft.com/office/infopath/2003/xslt/xDocument", xdDocumentExtension);
            argumentList.AddExtensionObject("http://schemas.microsoft.com/office/infopath/2003/xslt/xImage", xdImageExtension);

            return argumentList;
        }

        private Stream ProcessViewContent(Stream viewContentStream)
        {
            using (StreamReader streamReader = new StreamReader(viewContentStream))
            {
                string viewContentText = streamReader.ReadToEnd();
                
                // Update extension function names.
                viewContentText = this.UpdateExtensionFunctions(viewContentText);

                // Remove conditional logics so that all controls are availiable in the preview pane.
                List<Helper.IViewXSLProcessor> processors = new List<Helper.IViewXSLProcessor>();
                processors.Add(new Helper.CommentElementRemover());
                processors.Add(new Helper.XSLChooseRemover());
                processors.Add(new Helper.XSLIfTestProcessor());
                viewContentText = this.RemoveConditionalLogic(viewContentText, processors);

                using (StringReader stringReader = new StringReader(viewContentText))
                {
                    XDocument doc = XDocument.Load(stringReader);
                    XNamespace xdNamespace = GeekBangCN.InfoPathAnalyzer.Utility.LinqHelper.GetNamespace(doc, "xd");
                    XNamespace xslNamespace = GeekBangCN.InfoPathAnalyzer.Utility.LinqHelper.GetNamespace(doc, "xsl");

                    // Add "xd:binding" attribute for expression box control.
                    IList<XElement> expressionBoxElementsWithoutBindingAttribute = this.FindExpressionBoxElementWithoutBindingAttribute(doc.Root, xdNamespace);
                    this.AddXdBindingAttributeForExpressionBoxControl(expressionBoxElementsWithoutBindingAttribute, xdNamespace);

                    // Add "xd:binding" attribute for section control.
                    IList<XElement> sectionElementsWithoutBindingAttribute = this.FindSectionElementWithoutBindingAttribute(doc.Root, xslNamespace);
                    this.AddXdBindingAttributeForSectionControl(sectionElementsWithoutBindingAttribute, xdNamespace, xslNamespace);

                    // Make unfocusable elements focusable.
                    IList<XElement> unFocusableElements = this.FindUnFocusableElements(doc.Root);
                    this.MakeUnFocusableElementFocusable(unFocusableElements);

                    // Save the processed view content to a memory stream.
                    MemoryStream ms = new MemoryStream();
                    doc.Save(ms);
                    ms.Position = 0;

                    return ms;
                }
            }
        }

        private void AddXdBindingAttributeForExpressionBoxControl(IList<XElement> expressionBoxElementsWithoutBindingAttribute, XNamespace xdNamespace)
        {
            foreach (XElement expressionBoxElement in expressionBoxElementsWithoutBindingAttribute)
            {
                // Get the binding element name from xsl method.
                // Sometimes the element do not have "xd:binding" attribute, but the value
                // is bound via xsl methods like:
                // <xsl:value-of select="my:Hidden/my:MasterAgreementEffectiveDateFormated" />
                string bindingElementName = ViewManager.GetExpressionBoxWithoutBindingAttributeBindingValue(expressionBoxElement, xdNamespace);

                // Create and add "xd:binding" attribute to the expression box element so that we can find
                // the expression box element by binding field name.
                XAttribute bindingAttribute = new XAttribute(xdNamespace + "binding", bindingElementName);
                expressionBoxElement.Add(bindingAttribute);
            }
        }

        private void AddXdBindingAttributeForSectionControl(IList<XElement> sectionElementsWithoutBindingAttribute, XNamespace xdNamespace, XNamespace xslNamespace)
        {
            foreach (XElement sectionElement in sectionElementsWithoutBindingAttribute.ToList())
            {
                string bindingElementName = this.GetBindingElementNameFromApplyTemplatesXslMethod(sectionElement);

                XAttribute bindingAttribute = new XAttribute(xdNamespace + "binding", bindingElementName);
                sectionElement.Add(bindingAttribute);
            }
        }

        private void MakeUnFocusableElementFocusable(IList<XElement> unfocusableElements)
        {
            // TODO: Does not take effect on "table" element.
            foreach (XElement unfocusableElement in unfocusableElements)
            {
                XAttribute tabIndexAttribute = unfocusableElement.Attribute("tabIndex");
                if (tabIndexAttribute == null)
                {
                    tabIndexAttribute = new XAttribute("tabIndex", "-1");
                    unfocusableElement.Add(tabIndexAttribute);
                }
                else
                {
                    tabIndexAttribute.SetValue("-1");
                }
            }
        }

        private string UpdateExtensionFunctions(string viewContent)
        {
            return viewContent.Replace("xdXDocument:get-Role()", "xdXDocument:get_Role()");
        }

        private string RemoveConditionalLogic(string viewContent, IList<Helper.IViewXSLProcessor> processors)
        {
            foreach (Helper.IViewXSLProcessor processor in processors)
            {
                viewContent = processor.Process(viewContent);
            }

            return viewContent;
        }

        private IList<XElement> FindUnFocusableElements(XElement parentElement)
        {
            List<XElement> foundElements = new List<XElement>();
            IEnumerable<XElement> children = parentElement.Elements();
            if (children.Count() > 0)
            {
                var query = from c in children
                            where c.Name.LocalName.Equals("div", StringComparison.InvariantCultureIgnoreCase)
                            || c.Name.LocalName.Equals("table", StringComparison.InvariantCultureIgnoreCase)
                            || c.Name.LocalName.Equals("span", StringComparison.InvariantCultureIgnoreCase)
                            || c.Name.LocalName.Equals("img", StringComparison.InvariantCultureIgnoreCase)
                            select c;

                if (query != null && query.Count() > 0)
                {
                    foundElements.AddRange(query);
                }

                foreach (XElement element in children)
                {
                    foundElements.AddRange(this.FindUnFocusableElements(element));
                }
            }

            return foundElements;
        }

        private IList<XElement> FindExpressionBoxElementWithoutBindingAttribute(XElement parentElement, XNamespace xdNamespace)
        {
            List<XElement> foundElements = new List<XElement>();
            IEnumerable<XElement> children = parentElement.Elements();
            if (children.Count() > 0)
            {
                var query = from c in children
                            where ViewManager.IsElementExpressionBoxWithoutBindingAttribute(c, xdNamespace)
                            select c;

                if (query != null && query.Count() > 0)
                {
                    foundElements.AddRange(query);
                }

                foreach (XElement element in children)
                {
                    foundElements.AddRange(this.FindExpressionBoxElementWithoutBindingAttribute(element, xdNamespace));
                }
            }

            return foundElements;
        }

        private IList<XElement> FindSectionElementWithoutBindingAttribute(XElement parentElement, XNamespace xslNamespace)
        {
            List<XElement> foundElements = new List<XElement>();
            IEnumerable<XElement> children = parentElement.Elements();
            if (children.Count() > 0)
            {
                var query = from c in children
                            where c.Name.LocalName.Equals("div", StringComparison.InvariantCultureIgnoreCase)
                            && c.Element(xslNamespace + "apply-templates") != null
                            select c;

                if (query != null && query.Count() > 0)
                {
                    foundElements.AddRange(query);
                }

                foreach (XElement element in children)
                {
                    foundElements.AddRange(this.FindSectionElementWithoutBindingAttribute(element, xslNamespace));
                }
            }

            return foundElements;
        }

        private void OnPreviewWebBrowserDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if ((string.IsNullOrWhiteSpace(this.previewWebBrowser.DocumentText)
                || this.previewWebBrowser.DocumentText.Equals("<HTML></HTML>\0", StringComparison.InvariantCultureIgnoreCase))
                && !string.IsNullOrWhiteSpace(this.currentControlID))
            {
                this.SetPreviewContent(this.currentViewInternalFileName, this.currentControlID);

                return;
            }

            this.previewWebBrowser.Document.Body.SetAttribute("contenteditable", "true");
            
            // Add GotFocus/LostFocus for all elements to handle highlighting.
            foreach (HtmlElement subElement in this.previewWebBrowser.Document.Body.All)
            {
                subElement.GotFocus += new HtmlElementEventHandler(OnElementGotFocus);
            }

            if (!string.IsNullOrWhiteSpace(this.currentControlID))
            {
                this.FocusOnControlByControlID(this.currentControlID);
                this.currentControlID = string.Empty;
            }
        }

        private void OnElementGotFocus(object sender, HtmlElementEventArgs e)
        {
            if (this.currentFocusedElement != null && !this.elementStyles.ContainsKey(this.currentFocusedElement))
            {
                // Recover previous focused element style.
                foreach (HtmlElement element in this.elementStyles.Keys)
                {
                    element.Style = this.elementStyles[element];
                }

                // Clear out the dictionary.
                elementStyles.Clear();

                // Add current focused element to the dictionary.
                string style = this.currentFocusedElement.Style;
                this.elementStyles.Add(this.currentFocusedElement, style);

                // Highlight the focused element.
                this.currentFocusedElement.Style = this.GenerateFocusedElementStyle(this.currentFocusedElement);
            }
        }

        private string GenerateFocusedElementStyle(HtmlElement focusedElement)
        {
            string style = focusedElement.Style;
            if (focusedElement.TagName != null && focusedElement.TagName.Equals("span", StringComparison.InvariantCultureIgnoreCase))
            {
                // If SPAN element style does not have 'WIDTH' attribulte, we need to add one so that
                // the background color can be displayed.
                if (style.IndexOf("width:", StringComparison.InvariantCultureIgnoreCase) == -1)
                {
                    style += "; WIDTH:20px;";
                }
            }

            style += this.highlightStyleText;

            return style;
        }

        private void ValidatePreviewWebBrowser()
        {
            if (this.PreviewWebBrowser == null)
            {
                throw new MemberAccessException("PreviewWebBrowser cannot be null.");
            }
        }

        private void ValidateViewBLL()
        {
            if (this.ViewBLL == null)
            {
                throw new MemberAccessException("ViewBLL cannot be null.");
            }
        }

        #endregion
    }
}
