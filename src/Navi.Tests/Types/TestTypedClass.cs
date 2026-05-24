using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navi.Tests.Types;

/// <summary>
/// Class for Testing
/// </summary>
public class TestTypedClass<T>
{
    /// <summary>
    /// This is a get property.
    /// </summary>
    public T ParameterGet { get; }

    /// <summary>
    /// This is both a get and set property.
    /// </summary>
    public T ParameterGetSet { get; set; }

    /// <summary>
    /// And this an empty test constructor.
    /// </summary>
    public TestTypedClass()
    {
        ParameterGet = default;
    }

    /// <summary>
    /// And this a non-empty test constructor.
    /// </summary>
    /// <param name="parameterGet">This is the get parameter.</param>
    /// <param name="parameterGetSet">This is the get and set parameter.</param>
    public TestTypedClass(T parameterGet, T parameterGetSet)
    {
        ParameterGet = parameterGet;
        ParameterGetSet = parameterGetSet;
    }
}
