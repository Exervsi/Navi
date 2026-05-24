using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navi.Tests.Types;

/// <summary>
/// Class for Testing
/// </summary>
public static class TestStaticClass
{

    /// <summary>
    /// This is a static Property.
    /// </summary>
    public static string StaticProperty { get; set; } = "This is a static property.";

    /// <summary>
    /// This is a test extension method
    /// </summary>
    /// <param name="a">A</param>
    /// <returns>returns the same string.</returns>
    public static string ExtensionMethod(this string a)
    {
        return a;
    }

    /// <summary>
    /// This is a test extension method with a generic type parameter.
    /// </summary>
    /// <typeparam name="T">T</typeparam>
    /// <param name="a">A</param>
    /// <returns></returns>
    public static List<T> TypedExtensionMethod<T>(this List<T> a)
    {
        return a;
    }
}
