var xml = WScript.CreateObject("Microsoft.XMLDOM");          //input
xml.validateOnParse=false;
xml.load("MxConvert.xml");

var xsl = WScript.CreateObject("Microsoft.XMLDOM");          //style
xsl.validateOnParse=false;
xsl.load("MxConvert.xsl");

var out = WScript.CreateObject("Scripting.FileSystemObject"); //output
var replace = true; var unicode = false; //output file properties
var hdl = out.CreateTextFile("MxConvert.Generated.cs", replace, unicode )

hdl.write( xml.transformNode( xsl.documentElement ));
