using System.Reflection;

namespace Navi.InfoItems
{

    /// <summary>
    /// Represents a generic information item within a hierarchical documentation or data structure.
    /// </summary>
    /// <remarks>
    /// Implementations of <see cref="InfoItem"/> provide access to named values, hierarchical relationships,
    /// and associated metadata, supporting both navigation and rendering scenarios (e.g., Markdown output).
    /// </remarks>
    public interface InfoItem
    {
        /// <summary>
        /// Gets the display name of the information item.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets a child <see cref="InfoItem"/> by its name.
        /// </summary>
        /// <param name="name">The name of the child item to retrieve.</param>
        InfoItem this[string name] { get; }

        /// <summary>
        /// Gets the unique key identifying this information item.
        /// </summary>
        string Key { get; }

        /// <summary>
        /// Gets the value associated with this information item.
        /// </summary>
        string Value { get; }

        /// <summary>
        /// Gets the parent <see cref="InfoItem"/> in the hierarchy, or <c>null</c> if this is the root.
        /// </summary>
        InfoItem Parent { get; }

        /// <summary>
        /// Gets the immediate child items of this information item.
        /// </summary>
        InfoItem[] Children { get; }

        /// <summary>
        /// Gets the root <c>DocuTree</c> object to which this item belongs.
        /// </summary>
        public DocuTree Root { get; }

        /// <summary>
        /// Gets the custom attributes associated with this information item.
        /// </summary>
        Attribute[] Attributes { get; }

        /// <summary>
        /// Returns a Markdown representation of this information item.
        /// </summary>
        /// <returns>An <c>IMarkdownElement</c> representing the item in Markdown format.</returns>
        Markdown.IMarkdownElement GetMarkdown();
    }
}
