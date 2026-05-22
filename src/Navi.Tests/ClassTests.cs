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

public class ClassTests
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

        type = docuTree["Navi.Tests"]["Types"]["TestClass"] as InfoItems.Type;
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
        properties.Length.Should().Be(3);

        properties[0].Value.Should().Be("This is a get property.");
        properties[1].Value.Should().Be("This is both a get and set property.");
        properties[2].Value.Should().Be("This is a static property.");

        properties[0].Type.Should().Be(typeof(int));
        properties[1].Type.Should().Be(typeof(bool));
        properties[2].Type.Should().Be(typeof(string));

        properties[0].IsGettable.Should().BeTrue(); 
        properties[1].IsGettable.Should().BeTrue();
        properties[2].IsGettable.Should().BeTrue();
        properties[0].IsSettable.Should().BeFalse();
        properties[1].IsSettable.Should().BeTrue();
        properties[2].IsSettable.Should().BeTrue();
        properties[0].IsStatic.Should().BeFalse();
        properties[1].IsStatic.Should().BeFalse();
        properties[2].IsGettable.Should().BeTrue();
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

        string expected = "# TestClass()\n\n" +
                          "And this an empty test constructor.\n\n";

        result.Should().Be(expected);

        result = constructors[1].GetMarkdown().Markdown;

        expected = "# TestClass(System.Int32,System.Boolean)\n\n" +
                   "And this a non-empty test constructor.\n\n" +
                   "**Parameters :** \n\n" +
                   "`System.Int32` parameterGet : This is the get parameter.\n\n" +
                   "`System.Boolean` parameterGetSet : This is the get and set parameter.\n\n";

        result.Should().Be(expected);

        //constructors[0].Path.Should().Be("./Navi.Tests/Navi.Tests.Types/TestClass/Constructors");

    }

    [Test]
    public void GetMethods()
    {
        type.Should().NotBeNull();

        Method[] methods = type.Methods;
        methods.Should().NotBeNull();
        methods.Length.Should().Be(3);

        methods[0].Value.Should().Be("This is a test method.");
        methods[1].Value.Should().Be("This is a test method with parameters.");
        methods[2].Value.Should().Be("");

        Parameter[] parameters = methods[1].Parameters;
        parameters.Length.Should().Be(2);
        parameters[0].Value.Should().Be("A");
        parameters[1].Value.Should().Be("B");

        parameters = methods[2].Parameters;
        parameters[0].Value.Should().Be("");

        Return returnItem = methods[1].Return;
        returnItem.Value.Should().Be("This returns zero.");

        string result = methods[0].GetMarkdown().Markdown;

        string expected = "# System.Void EmptyMethod()\n\n" +
                          "This is a test method.\n\n" +
                          "**Returns :** `System.Void`\n\n";

        result.Should().Be(expected);

        result = methods[1].GetMarkdown().Markdown;

        expected = "# System.Int32 Method(System.Int32,System.Int32)\n\n" +
                   "This is a test method with parameters.\n\n" +
                   "**Parameters :** \n\n" +
                   "`System.Int32` a : A\n\n`" +
                   "System.Int32` b : B\n\n" +
                   "**Returns :** `System.Int32`\n\n";

        result.Should().Be(expected);
        //methods[0].Path.Should().Be("./Navi.Tests/Navi.Tests.Types/TestClass/EmptyMethod");
        //methods[1].Path.Should().Be("./Navi.Tests/Navi.Tests.Types/TestClass/Method");
        //methods[2].Path.Should().Be("./Navi.Tests/Navi.Tests.Types/TestClass/NoCommentMethod");
    }
    [Test]
    public void GetMarkdown()
    {

        type.Should().NotBeNull();
        string result = type.GetMarkdown().Markdown;
        result.Should().NotBeNullOrEmpty();



        string expected = "# TestClass\n\nNavi.Tests.Types.TestClass\n\nClass for Testing\n\n## Constructors\n\n| Constructor                                                                          | Description                            |\n| ------------------------------------------------------------------------------------ | -------------------------------------- |\n| [.ctor(](../../Navi.Tests/Navi.Tests.Types/TestClass/Constructors)                   | And this an empty test constructor.    |\n| [.ctor(`Int32`, `Boolean`](../../Navi.Tests/Navi.Tests.Types/TestClass/Constructors) | And this a non-empty test constructor. |\n\n## Properties\n\n| Type             | Property        | Description                          |\n| ---------------- | --------------- | ------------------------------------ |\n| `System.Int32`   | ParameterGet    | This is a get property.              |\n| `System.Boolean` | ParameterGetSet | This is both a get and set property. |\n| `System.String`  | StaticParameter | This is a static property.           |\n\n## Methods\n\n| Type           | Method                                                                         | Description                            |\n| -------------- | ------------------------------------------------------------------------------ | -------------------------------------- |\n| `System.Void`  | [EmptyMethod](../../Navi.Tests/Navi.Tests.Types/TestClass/EmptyMethod)         | This is a test method.                 |\n| `System.Int32` | [Method](../../Navi.Tests/Navi.Tests.Types/TestClass/Method)                   | This is a test method with parameters. |\n| `System.Int32` | [NoCommentMethod](../../Navi.Tests/Navi.Tests.Types/TestClass/NoCommentMethod) |                                        |\n\n";


        result.Should().Be(expected);

    }
}