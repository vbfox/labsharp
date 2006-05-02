@echo off

rem  Lab# - Matlab interaction library for .Net
rem  
rem  Copyright (C) 2006 Julien Roncaglia
rem 
rem  This library is free software; you can redistribute it and/or
rem  modify it under the terms of the GNU Lesser General Public
rem  License as published by the Free Software Foundation; either
rem  version 2.1 of the License, or (at your option) any later version.
rem 
rem  This library is distributed in the hope that it will be useful,
rem  but WITHOUT ANY WARRANTY; without even the implied warranty of
rem  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
rem  Lesser General Public License for more details.
rem 
rem  You should have received a copy of the GNU Lesser General Public
rem  License along with this library; if not, write to the Free Software
rem  Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA

rem This file call MxConvert.Generate.js in command line, it allow to read
rem errors in the command line window instead of in a MessageBox

cscript MxConvert.Generate.js //NoLogo
