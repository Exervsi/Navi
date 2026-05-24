using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navi.Tests.Types;

/// <summary>
/// Enum for Testing
/// </summary>
public enum TestEnumWithAttributes
{
    /// <summary>
    /// This is none.
    /// </summary>
    [TestField("a","b")]
    None,
    /// <summary>
    /// This is first.
    /// </summary>
    [TestField("c", "d")]
    First,
    /// <summary>
    /// This is second.
    /// </summary>
    [TestField("e", "f")]
    Second,
    /// <summary>
    /// And this is third.
    /// </summary>
    [TestField("g", "h")]
    Third,
}
