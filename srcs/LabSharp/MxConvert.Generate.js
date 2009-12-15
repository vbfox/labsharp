/*
 * Lab# - Matlab interaction library for .Net
 * 
 * Copyright (C) 2006 Julien Roncaglia
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.

 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
 */

/*
 * This file transform the "MxConvert.xml" file with XSLT to create
 * "MxConvert.FromMxArray.cs" and "MxConvert.ToMxArray.cs".
 */

var fso = WScript.CreateObject("Scripting.FileSystemObject");

// Load the XML file
var xml = WScript.CreateObject("Microsoft.XMLDOM");
xml.validateOnParse  =false;
xml.load("MxConvert.xml");

// Load the two XSLT files
var xslFrom = WScript.CreateObject("Microsoft.XMLDOM");
xslFrom.validateOnParse = false;
xslFrom.load("MxConvert.FromMxArray.xsl");
var xslTo = WScript.CreateObject("Microsoft.XMLDOM");
xslTo.validateOnParse=false;
xslTo.load("MxConvert.ToMxArray.xsl");

// Create the output file
var outputFrom = fso.CreateTextFile("MxConvert.FromMxArray.cs", true, false );
var outputTo = fso.CreateTextFile("MxConvert.ToMxArray.cs", true, false );

// Apply the transformation
outputFrom.write( xml.transformNode( xslFrom.documentElement ));
outputTo.write( xml.transformNode( xslTo.documentElement ));
