using Navi.Markdown;
using System.Reflection;

namespace Navi.InfoItems
{
    /// <summary>
    /// Represents a type parameter within the documentation tree structure.
    /// Inherits from <see cref="InfoItem"/> and provides access to its name, type data, parent, children, and attributes.
    /// </summary>
    public class TypeParameter : InfoItem
    {
        /// <summary>
        /// Gets the name of the type parameter.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the child <see cref="InfoItem"/> with the specified name, or <c>null</c> if not found.
        /// </summary>
        /// <param name="name">The name of the child item to retrieve.</param>
        public InfoItem this[string name] => Children.FirstOrDefault(x => x.Name == name);

        /// <summary>
        /// Gets the key associated with this item. Always returns an empty string for <see cref="TypeParameter"/>.
        /// </summary>
        public string Key => "";

        /// <summary>
        /// Gets the value associated with this item. Always returns an empty string for <see cref="TypeParameter"/>.
        /// </summary>
        public string Value => "";

        /// <summary>
        /// Gets the data type descriptor for this item. Always returns "Parameter".
        /// </summary>
        public string DataType => "Parameter";

        /// <summary>
        /// Gets the underlying <see cref="System.Type"/> represented by this type parameter.
        /// </summary>
        public System.Type Data { get; }

        /// <summary>
        /// Gets the parent <see cref="InfoItem"/> in the documentation tree.
        /// </summary>
        public InfoItem Parent { get; }

        /// <summary>
        /// Gets the root <see cref="DocuTree"/> of the documentation tree to which this item belongs.
        /// </summary>
        public DocuTree Root
        {
            get
            {
                InfoItem parent = Parent;
                while (parent.Parent is not null)
                    parent = parent.Parent;
                return parent as DocuTree;
            }
        }

        /// <summary>
        /// Gets the child items of this type parameter.
        /// </summary>
        public InfoItem[] Children { get; }

        /// <summary>
        /// Gets or sets the attributes associated with this type parameter.
        /// </summary>
        public Attribute[] Attributes { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeParameter"/> class.
        /// </summary>
        /// <param name="typeParameter">The <see cref="System.Type"/> representing the type parameter.</param>
        /// <param name="constructorItem">The parent <see cref="InfoItem"/> (typically a constructor or method).</param>
        public TypeParameter(System.Type typeParameter, InfoItem constructorItem)
        {
            Name = typeParameter.Name;
            Data = typeParameter;
            Parent = constructorItem;
        }

        /// <summary>
        /// Returns a markdown representation of this type parameter.
        /// </summary>
        /// <returns>A new <see cref="Markdown.Page"/> instance.</returns>
        public IMarkdownElement GetMarkdown()
        {
            return new Markdown.Page();
        }
    }
}
