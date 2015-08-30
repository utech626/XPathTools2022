This is an extension intended for Visual Studio 2012 or later. It's available through _Tools &rarr; Extensions and Updates... &rarr; Online_ from within Visual Studio, by searching for "XPath Information" or simply "xpath".

The extension displays the XPath of the XML-element or -attribute at the caret position when working in files containing XML markup (.config, .build, .properties etc.).  

![XPath of the caret shown in the status bar](https://github.com/uli-weltersbach/XPathInformation/blob/master/ReasonCodeExample.XPathInformation/VisualStudioIntegration/Resources/Screenshot-Statusbar.png)

The information is displayed in the statusbar and can be copied to the clipboard in various formats, using the _Copy XPath_ commands available through the context menu.  

![Copy XPath commands](https://github.com/uli-weltersbach/XPathInformation/blob/master/ReasonCodeExample.XPathInformation/VisualStudioIntegration/Resources/Screenshot-Copy%20XPath.png)

The XPath variations currently available are:

* Generic - matches one or more elements
* Absolute - matches the current element by index
* Distinct - attempts to determine an XPath which only matches the current element by using distinct attribute values

The "distinct" option has been added mainly for work with [Web.config](http://msdn.microsoft.com/en-us/library/w7w4sb0w.aspx) files.

It's possible to configure which attributes are copied to the clipboard from the XPath options page found in "Tools -> Options -> XPath Information". Check the option descriptions for details.

![XPath Information options page](https://github.com/uli-weltersbach/XPathInformation/blob/master/ReasonCodeExample.XPathInformation/VisualStudioIntegration/Resources/Screenshot-Options.png).

A "shallow XML structure" can be copied to the clipboard which contains the current element, it's ancestors and descendants; other siblings are excluded. This function is primarily intended for use with [Sitecore CMS](http://www.sitecore.net) [Web.config include files](http://www.sitecore.net/Community/Technical-Blogs/John-West-Sitecore-Blog/Posts/2011/05/All-About-Web-config-Include-Files-with-the-Sitecore-ASPNET-CMS.aspx).

![Copy XML structure](https://github.com/uli-weltersbach/XPathInformation/blob/master/ReasonCodeExample.XPathInformation/VisualStudioIntegration/Resources/Screenshot-XML%20Structure%20copy.png)

![Paste XML structure](https://github.com/uli-weltersbach/XPathInformation/blob/master/ReasonCodeExample.XPathInformation/VisualStudioIntegration/Resources/Screenshot-XML%20Structure%20paste.png)

The extension has been tested with Visual Studio 2012 and later but might work with older versions if tweaked; the source is available from https://github.com/uli-weltersbach/XPathInformation.

Please use https://github.com/uli-weltersbach/XPathInformation/issues for feature requests and bug reports.
