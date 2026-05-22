using FluentAssertions;
using Navi.Core.SystemExtensions.String;
using Navi.InfoItems;
using Navi.Markdown;
using NUnit.Framework.Constraints;
using System.Reflection;

namespace Navi.Tests;

public class EnumTests
{
    [SetUp]
    public void Setup()
    {



    }

    [Test]
    public void GetEnumSummaries()
    {
        string dllPath = @"Navi.Tests.dll";
        string xmlPath = @"Navi.Tests.xml";

        bool dllExists = File.Exists(dllPath);


        //Assembly executingAssembly = Assembly.GetExecutingAssembly();
        //string a = executingAssembly.FullName;

        DocuTree docuTree = new DocuTree(dllPath, xmlPath);

        InfoItems.Type? type = docuTree["Navi.Tests"]["Types"]["TestEnum"] as InfoItems.Type;
        type.Should().NotBeNull();
        type.Value.Should().Be("Enum for Testing");

        string[] expected = ["This is none.", "This is first.", "This is second.", "And this is third."];

        int count = 0;

        foreach (Field field in type.Fields)
            if(field.Value != string.Empty)
                field.Value.Should().Be(expected[count++]);


        string summary = type.Value;
    }

    [Test]
    public void GetEnumAttributes()
    {
        string dllPath = @"Navi.Tests.dll";
        string xmlPath = @"Navi.Tests.xml";

        bool dllExists = File.Exists(dllPath);


        //Assembly executingAssembly = Assembly.GetExecutingAssembly();
        //string a = executingAssembly.FullName;

        DocuTree docuTree = new DocuTree(dllPath, xmlPath);
        Assert.AreEqual(docuTree.Namespaces.Length, 1);


        InfoItems.Type type = docuTree["Navi.Tests"]["Types"]["TestEnumWithAttributes"] as InfoItems.Type;

        char count = 'a';


        foreach (Field field in type.Fields)
            foreach (InfoItems.Attribute attribute in field.Attributes)
            {
                attribute.Name.Should().Be("TestFieldAttribute");
                attribute.Aruguments.Should().HaveCount(2);
                foreach (object value in attribute.Values)
                {
                    value.Should().BeOfType(typeof(string));
                    value.ToString()[0].Should().Be(count++);
                }
            }

        type.Value.Should().Be("Enum for Testing");

        string summary = type.Value;
    }

    [Test]
    public void GetEnumTable()
    {
        string dllPath = @"Navi.Tests.dll";
        string xmlPath = @"Navi.Tests.xml";

        bool dllExists = File.Exists(dllPath);


        //Assembly executingAssembly = Assembly.GetExecutingAssembly();
        //string a = executingAssembly.FullName;

        DocuTree docuTree = new DocuTree(dllPath, xmlPath);
        Assert.AreEqual(docuTree.Namespaces.Length, 1);


        InfoItems.Type type = docuTree["Navi.Tests"]["Types"]["TestEnumWithAttributes"] as InfoItems.Type;

        Markdown.Tables.TryGetEnumTableValues(type, out string[,] values, true, false, true).Should().BeTrue();
        Markdown.Tables.TryCreateTable(["Name", "Attribute A", "Attribute B"], values, out string result).Should().BeTrue();


        string expected =
            "| Name   | Attribute A | Attribute B |\n" +
            "| ------ | ----------- | ----------- |\n" +
            "| None   | a           | b           |\n" +
            "| First  | c           | d           |\n" +
            "| Second | e           | f           |\n" +
            "| Third  | g           | h           |\n\n";

        result.Should().Be(expected);
    }

}