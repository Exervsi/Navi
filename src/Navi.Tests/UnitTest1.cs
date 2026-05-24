using Navi.Core.SystemExtensions.String;
using Navi.InfoItems;
using System.Diagnostics;
using System.Reflection;

namespace Navi.Tests;

public class NamespaceTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        string dllPath = @"..\..\..\..\Navi\bin\Debug\netstandard2.0\Navi.dll";
        string xmlPath = @"..\..\..\..\Navi\bin\Debug\netstandard2.0\Navi.xml";

        //dllPath = @"C:\Users\matthewa\source\repos\Exervsi\geeWiz\src\geeWiz\bin\Debug\2026\geeWiz\geeWiz.dll";
        //xmlPath = @"C:\Users\matthewa\source\repos\Exervsi\geeWiz\src\geeWiz\bin\Debug\2026\geeWiz\geeWiz.xml";

        //dllPath = @"C:\Users\matthewa\source\repos\Revit\Rothelowman.Revit\Libraries\Revit2026\bin\Debug\net8.0-windows\Rothelowman.Architecture.dll";
        //xmlPath = @"C:\Users\matthewa\source\repos\Revit\Rothelowman.Revit\Libraries\Revit2026\bin\Debug\net8.0-windows\Rothelowman.Architecture.xml";

        bool pathExists = File.Exists(@"C:\Users\matthewa\source\repos\Exervsi\geeWiz\src\geeWiz\bin\Debug\2026\geeWiz\RevitAPIUI.dll");

        //Assembly a = Assembly.LoadFrom(@"C:\Users\matthewa\source\repos\Exervsi\geeWiz\src\geeWiz\bin\Debug\2026\geeWiz\RevitAPIUI.dll");
        //a = Assembly.LoadFile(@"C:\Program Files\Autodesk\Revit 2026\RevitAPI.dll");
        bool dllExists = File.Exists(dllPath);

        DocuTree docuTree = new DocuTree(dllPath, xmlPath);
        //Assert.AreEqual(docuTree.Namespaces.Length, 1);

        docuTree.PrintDocusaurus("C:/Users/Matthewa/Desktop/NaviDocs/");

    }
}