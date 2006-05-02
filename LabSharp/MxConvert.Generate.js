var xml = WScript.CreateObject("Microsoft.XMLDOM");
xml.validateOnParse  =false;
xml.load("MxConvert.xml");

var xslFrom = WScript.CreateObject("Microsoft.XMLDOM");
xslFrom.validateOnParse = false;
xslFrom.load("MxConvert.FromMxArray.xsl");

var xslTo = WScript.CreateObject("Microsoft.XMLDOM");
xslTo.validateOnParse=false;
xslTo.load("MxConvert.ToMxArray.xsl");

var fso = WScript.CreateObject("Scripting.FileSystemObject");

var outputFrom = fso.CreateTextFile("MxConvert.FromMxArray.cs", true, false );
var outputTo = fso.CreateTextFile("MxConvert.ToMxArray.cs", true, false );

outputFrom.write( xml.transformNode( xslFrom.documentElement ));
outputTo.write( xml.transformNode( xslTo.documentElement ));
