using Navi.Core.SystemExtensions.String;
using Navi.InfoItems;
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

        System.Xml.Linq.XElement element = Wakka.LoadXml(xmlPath, "M:Navi.Core.SystemExtensions.String.Wakka.LoadXml(System.String,System.String)");

        string a = element.Element("summary").Value.Trim();

        bool dllExists = File.Exists(dllPath);

        DocuTree docuTree = new DocuTree(dllPath, xmlPath);
        //Assert.AreEqual(docuTree.Namespaces.Length, 1);

        Docusuaurs.Print(docuTree, "C:/Users/Matthewa/Desktop/NaviDocs/");

    }
}