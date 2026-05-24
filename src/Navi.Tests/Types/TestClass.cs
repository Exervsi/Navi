using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navi.Tests.Types;

/// <summary>
/// Class for Testing
/// </summary>
public class TestClass
{
    /// <summary>
    /// This is a get property.
    /// </summary>
    public int ParameterGet { get; }

    /// <summary>
    /// This is both a get and set property.
    /// </summary>
    public bool ParameterGetSet { get; set; }

    /// <summary>
    /// This is a static property.
    /// </summary>
    public string StaticParameter { get; set; }

    /// <summary>
    /// And this an empty test constructor.
    /// </summary>
    public TestClass()
    {
        ParameterGet = 1;
    }

    /// <summary>
    /// And this a non-empty test constructor.
    /// </summary>
    /// <param name="parameterGet">This is the get parameter.</param>
    /// <param name="parameterGetSet">This is the get and set parameter.</param>
    public TestClass(int parameterGet, bool parameterGetSet)
    {
        ParameterGet = parameterGet;
        ParameterGetSet = parameterGetSet;
    }

    /// <summary>
    /// This is a test method.
    /// </summary>
    public void EmptyMethod()
    {
    }

    /// <summary>
    /// This is a test method with parameters.
    /// </summary>
    /// <param name="a">A</param>
    /// <param name="b">B</param>
    /// <returns>This returns zero.</returns>
    public int Method(int a, int b)
    {
        return 0;
    }

    public int NoCommentMethod(int a) { return 0; }

    /// <summary>
    /// This is a test method with some comments.
    /// </summary>
    /// <param name="a">A</param>
    public int SomeCommentMethod(int a, int b) { return 0; }

    /// <summary>
    /// This is a test method with a type parameter.
    /// </summary>
    /// <typeparam name="T">The type of the values to compare.</typeparam>
    /// <param name="a">A</param>
    /// <param name="b">B</param>
    /// <returns>A</returns>
    public T MethodWithATypeParameter<T>(T a, T b) { return a; }
}
