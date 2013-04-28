﻿using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Text.Editor;

namespace ReasonCodeExample.XPathInformation
{
    internal class XPathStatusbarInformation
    {
        private readonly ResultCachingXmlParser _parser = new ResultCachingXmlParser();
        private readonly XmlNodeRepository _repository = new XmlNodeRepository();
        private readonly XPathFormatter _formatter = new XPathFormatter();
        private readonly IVsStatusbar _statusbar;

        public XPathStatusbarInformation(ITextView view)
            : this(view, (IVsStatusbar)ServiceProvider.GlobalProvider.GetService(typeof(IVsStatusbar)))
        {
        }

        public XPathStatusbarInformation(ITextView view, IVsStatusbar statusbar)
        {
            if (view == null)
                throw new ArgumentNullException("view");
            if (statusbar == null)
                throw new ArgumentNullException("statusbar");
            view.Caret.PositionChanged += UpdateXPath;
            _statusbar = statusbar;
        }

        private void UpdateXPath(object sender, CaretPositionChangedEventArgs e)
        {
            try
            {
                string xpath = GetXPath(e);
                _statusbar.SetText(xpath);
            }
            catch (Exception ex)
            {
                _statusbar.SetText(ex.Message);
            }
        }

        private string GetXPath(CaretPositionChangedEventArgs e)
        {
            XElement rootElement = _parser.Parse(e.TextView.TextSnapshot.GetText());
            IXmlLineInfo caretPosition = new CaretPositionLineInfo(e);
            XElement selectedElement = _repository.GetElement(rootElement, caretPosition.LineNumber, caretPosition.LinePosition);
            XAttribute selectedAttribute = _repository.GetAttribute(selectedElement, caretPosition.LinePosition);
            return _formatter.Format(selectedElement) + _formatter.Format(selectedAttribute);
        }
    }
}