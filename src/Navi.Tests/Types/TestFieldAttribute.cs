using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navi.Tests.Types;

/// <summary>
/// Field Attribute for Testing
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
internal class TestFieldAttribute : Attribute
{
    /// <summary>
    /// This is variable A for testing.
    /// </summary>
    public string A { get; }
    /// <summary>
    /// This is variable B for testing.
    /// </summary>
    public string B { get; }

    /// <summary>
    /// This is a constructor taking variable A.
    /// </summary>
    /// <param name="a"></param>
    /// This is a constructor taking variable B.
    /// <param name="b"></param>
    public TestFieldAttribute(string a, string b)
    {
        A = a;
        B = b ;
    }
}
