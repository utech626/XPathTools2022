using System.IO;
using System.Windows.Forms;
using NUnit.Framework;
using ReasonCodeExample.XPathTools.Tests.VisualStudioIntegration;
using ReasonCodeExample.XPathTools.VisualStudioIntegration;

namespace ReasonCodeExample.XPathTools.Tests.Workbench
{
    [TestFixture]
    [Category(TestCategory.Integration)]
    public class XPathWorkbenchTests
    {
        private readonly VisualStudioExperimentalInstance _visualStudio = new VisualStudioExperimentalInstance();
        private FileInfo _defaultXmlFile;

        [OneTimeSetUp]
        public void StartVisualStudio()
        {
            _visualStudio.ReStart();
            var xml = "<assemblyBinding xmlns=\"urn:schemas-microsoft-com:asm.v1\" xmlns:urn=\"urn:schemas-microsoft-com:asm.v1\"><dependentAssembly /></assemblyBinding>";
            _defaultXmlFile = _visualStudio.OpenXmlFile(xml, null);
            _visualStudio.ClickContextMenuEntry(PackageResources.ShowXPathWorkbenchCommandText);
        }

        [OneTimeTearDown]
        public void StopVisualStudio()
        {
            _visualStudio.Stop();
        }

        [Test]
        public void WorkbenchIsActivatedViaContextMenu()
        {
            // Act
            var xpathWorkbench = new XPathWorkbenchAutomationModel(_visualStudio.MainWindow);

            // Assert
            Assert.That(xpathWorkbench.IsVisible, Is.True);
        }

        [Test]
        public void WorkbenchRunsQueryEvenThoughNoNodeIsSelected()
        {
            // Act
            var xpathWorkbench = new XPathWorkbenchAutomationModel(_visualStudio.MainWindow);
            xpathWorkbench.Search("§ invalid XPath §");

            // Assert
            Assert.That(xpathWorkbench.SearchResultText, Does.Contain(PackageResources.XPathEvaluationErrorText));
        }

        [Test]
        public void WorkbenchShowsSearchResultCount()
        {
            // Arrange
            var xpathWorkbench = new XPathWorkbenchAutomationModel(_visualStudio.MainWindow);
            var expectedResultText = string.Format(PackageResources.SingleResultText, 1);

            // Act
            xpathWorkbench.Search("//*[local-name()='assemblyBinding']");

            // Assert
            Assert.That(xpathWorkbench.SearchResultText, Does.Contain(expectedResultText));
        }

        [Test]
        public void WorkbenchHandlesXmlNamespaces()
        {
            // Arrange
            var xpathWorkbench = new XPathWorkbenchAutomationModel(_visualStudio.MainWindow);
            var expectedResultText = string.Format(PackageResources.SingleResultText, 1);

            // Act
            xpathWorkbench.Search("/urn:assemblyBinding/urn:dependentAssembly");

            // Assert
            Assert.That(xpathWorkbench.SearchResultText, Does.Contain(expectedResultText));
        }

        [Test]
        public void WorkbenchActivatesCorrectDocumentWindow()
        {
            // Arrange
            var xpathWorkbench = new XPathWorkbenchAutomationModel(_visualStudio.MainWindow);
            xpathWorkbench.Search("/urn:assemblyBinding/urn:dependentAssembly");
            var xml = "<!-- This XML file is not the search result source --><root />";
            _visualStudio.OpenXmlFile(xml, null);

            // Act
            xpathWorkbench.GetSearchResult(0).LeftClick();

            // Assert
            Assert.That(_visualStudio.GetSelectedDocument().GetText(), Is.EqualTo(_defaultXmlFile.Name));
        }

        [Test]
        public void WorkbenchReattachesCorrectDocumentWindow()
        {
            // Arrange - open a document, run a search, close the document
            var xpathWorkbench = new XPathWorkbenchAutomationModel(_visualStudio.MainWindow);
            xpathWorkbench.Search("/urn:assemblyBinding/urn:dependentAssembly");
            xpathWorkbench.GetSearchResult(0).LeftClick();
            SendKeys.SendWait("^{F4}"); // Close the document using CTRL + F4

            // Act - click the search result
            xpathWorkbench.GetSearchResult(0).LeftClick();

            // Assert - verify that the document is reopened
            Assert.That(_visualStudio.GetSelectedDocument().GetText(), Is.EqualTo(_defaultXmlFile.Name));
        }
    }
}
