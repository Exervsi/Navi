using Navi.InfoItems;

string dllPath = @"..\..\..\..\Navi\bin\Debug\netstandard2.0\Navi.dll";
string xmlPath = @"..\..\..\..\Navi\bin\Debug\netstandard2.0\Navi.xml";

DocuTree theGreatDocuTree = new DocuTree(dllPath, xmlPath);
theGreatDocuTree.PrintDocusaurus(@"..\..\..\..\..\docs\docs\");