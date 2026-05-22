using Navi.InfoItems;
using System.Net.Http.Headers;

namespace Navi.Markdown
{
    /// <summary>
    /// Represents a generic element that can be converted to Markdown format.
    /// Implementations of this interface should provide a Markdown representation
    /// of the element via the <see cref="Markdown"/> property.
    /// </summary>
    public interface IMarkdownElement
    {
        /// <summary>
        /// Gets the Markdown representation of the element.
        /// </summary>
        string Markdown { get; }
    }
}