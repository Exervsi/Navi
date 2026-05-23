using Navi.InfoItems;

string dllPath = @"..\..\..\..\Navi\bin\Debug\netstandard2.0\Navi.dll";
string xmlPath = @"..\..\..\..\Navi\bin\Debug\netstandard2.0\Navi.xml";

Console.WriteLine("Hey Listen! Generating tree...");

DocuTree theGreatDekuTree = new DocuTree(dllPath, xmlPath);

Console.WriteLine("Hey Listen! Writing Markdown...");

theGreatDekuTree.PrintDocusaurus(@"..\..\..\..\..\docs\docs\");

Console.WriteLine("Hey Listen! Tree written!");