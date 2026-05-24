using FluentAssertions;
using Navi.Core.SystemExtensions.String;
using Navi.InfoItems;
using Navi.Markdown;
using Navi.Tests.Types;
using Newtonsoft.Json.Linq;
using NUnit.Framework.Constraints;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Navi.Tests;

public class StaticClassTests
{
    InfoItems.Type? type;


    [SetUp]
    public void Setup()
    {
        string dllPath = @"Navi.Tests.dll";
        string xmlPath = @"Navi.Tests.xml";

        bool dllExists = File.Exists(dllPath);

        DocuTree docuTree = new DocuTree(dllPath, xmlPath);
        docuTree.Path = @".";


        type = docuTree["Navi.Tests"]["Types"]["TestStaticClass"] as InfoItems.Type;
    }

    [Test]
    public void GetTypeSummaries()
    {
        type.Should().NotBeNull();
        type.Value.Should().Be("Class for Testing");
    }

    [Test]
    public void GetProperties()
    {
        type.Should().NotBeNull();

        Property[] properties = type.Properties;
        properties.Should().NotBeNull();
        properties.Length.Should().Be(1);

        properties[0].Value.Should().Be("This is a static Property.");

        properties[0].Type.Should().Be(typeof(string));

        properties[0].IsGettable.Should().BeTrue(); 
        properties[0].IsSettable.Should().BeTrue();
        properties[0].IsStatic.Should().BeTrue();
    }

    [Test]
    public void GetConstructors()
    {
        type.Should().NotBeNull();

        Constructor[] constructors = type.Constructors;
        constructors.Should().NotBeNull();
        constructors.Length.Should().Be(0);
    }

    [Test]
    public void GetMethods()
    {
        type.Should().NotBeNull();

        Method[] methods = type.Methods;
        methods.Should().NotBeNull();
        methods.Length.Should().Be(1);

        methods[0].Value.Should().Be("This is a test extension method");

        Return returnItem = methods[0].Return;
        methods[0].Return.Value.Should().Be("returns the same string.");


        string result = methods[0].GetMarkdown().Markdown;

        string expected = "# String ExtensionMethod(System.String)\n\n" +
            "This is a test extension method\n\n" +
            "**Parameters :** \n\n" +
            "`String` a : A\n\n" +
            "**Returns :** `String` : returns the same string.\n\n\n\n";
    }
    [Test]
    public void GetMarkdown()
    {

        type.Should().NotBeNull();
        string result = type.GetMarkdown().Markdown;
        result.Should().NotBeNullOrEmpty();



        string expected = "# TestStaticClass\n\n" +
            "Navi.Tests.Types.TestStaticClass\n\n" +
            "Class for Testing\n\n" +
            "## Properties\n\n" +
            "| Type     | Property       | Description                |\n" +
            "| -------- | -------------- | -------------------------- |\n" +
            "| `String` | StaticProperty | This is a static Property. |\n\n" +
            "## Methods\n\n" +
            "| Type     | Method                                                                               | Description                     |\n" +
            "| -------- | ------------------------------------------------------------------------------------ | ------------------------------- |\n" +
            "| `String` | [ExtensionMethod](../../Navi.Tests/Navi.Tests.Types/TestStaticClass/ExtensionMethod) | This is a test extension method |\n\n";

        result.Should().Be(expected);

    }
}