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

public class TypedClassTests
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

        type = docuTree["Navi.Tests"]["Types"]["TestTypedClass<T>"] as InfoItems.Type;
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
        properties.Length.Should().Be(2);

        properties[0].Value.Should().Be("This is a get property.");
        properties[1].Value.Should().Be("This is both a get and set property.");

        properties[0].Type.Name.Should().Be("T");
        properties[1].Type.Name.Should().Be("T");

        properties[0].IsGettable.Should().BeTrue(); 
        properties[1].IsGettable.Should().BeTrue();
        properties[0].IsSettable.Should().BeFalse();
        properties[1].IsSettable.Should().BeTrue();
        properties[0].IsStatic.Should().BeFalse();
        properties[1].IsStatic.Should().BeFalse();
    }

    [Test]
    public void GetConstructors()
    {
        type.Should().NotBeNull();

        Constructor[] constructors = type.Constructors;
        constructors.Should().NotBeNull();
        constructors.Length.Should().Be(2);

        constructors[0].Value.Should().Be("And this an empty test constructor.");
        constructors[1].Value.Should().Be("And this a non-empty test constructor.");

        Parameter[] parameters = constructors[1].Parameters;
        parameters.Length.Should().Be(2);
        parameters[0].Value.Should().Be("This is the get parameter.");
        parameters[1].Value.Should().Be("This is the get and set parameter.");


        string result = constructors[0].GetMarkdown().Markdown;

        string expected = "# TestTypedClass<T>()\n\n" +
                          "And this an empty test constructor.\n\n";

        result.Should().Be(expected);

        result = constructors[1].GetMarkdown().Markdown;

        expected = "# TestTypedClass<T>(T,T)\n\n" +
                   "And this a non-empty test constructor.\n\n" +
                   "**Parameters :** \n\n" +
                   "`T` parameterGet : This is the get parameter.\n\n" +
                   "`T` parameterGetSet : This is the get and set parameter.\n\n";

        result.Should().Be(expected);

        //constructors[0].Path.Should().Be("./Navi.Tests/Navi.Tests.Types/TestClass/Constructors");

    }

    [Test]
    public void GetMethods()
    {
        type.Should().NotBeNull();

        Method[] methods = type.Methods;
        methods.Should().NotBeNull();
        methods.Length.Should().Be(0);


    }
    [Test]
    public void GetMarkdown()
    {

        type.Should().NotBeNull();
        string result = type.GetMarkdown().Markdown;
        result.Should().NotBeNullOrEmpty();



        string expected = "# TestTypedClass<T>\n\n" +
            "Navi.Tests.Types.TestTypedClass`1\n\n" +
            "Class for Testing\n\n" +
            "## Constructors\n\n" +
            "| Constructor                                   | Description                            |\n" +
            "| --------------------------------------------- | -------------------------------------- |\n" +
            "| [TestTypedClass<T>()](./Constructors)         | And this an empty test constructor.    |\n" +
            "| [TestTypedClass<T>(`T`, `T`)](./Constructors) | And this a non-empty test constructor. |\n\n" +
            "## Properties\n\n| Type | Property        | Description                          |\n" +
            "| ---- | --------------- | ------------------------------------ |\n" +
            "| `T`  | ParameterGet    | This is a get property.              |\n" +
            "| `T`  | ParameterGetSet | This is both a get and set property. |\n\n";

        result.Should().Be(expected);

    }
}