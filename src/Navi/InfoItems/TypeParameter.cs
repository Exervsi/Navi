using Navi.Core.Getters.MarkdownGetters;
using Navi.Core.Getters.TreeGetters;
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
        private TypeParameterMarkdownGetter _markdownGetter => new TypeParameterMarkdownGetter(this);
        private LeafTreeGetter _treeGetter => new LeafTreeGetter(this);

        /// <summary>
        /// Gets the name of the type parameter.
        /// </summary>
        public string Name => _markdownGetter.Name;

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
        public DocuTree  Root => _treeGetter.Root;

        /// <summary>
        /// Gets the child items of this type parameter.
        /// </summary>
        public InfoItem[] Children => _treeGetter.Children;

        /// <summary>
        /// Gets or sets the attributes associated with this type parameter.
        /// </summary>
        public Attribute[] Attributes => _treeGetter.Attributes;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeParameter"/> class.
        /// </summary>
        /// <param name="typeParameter">The <see cref="System.Type"/> representing the type parameter.</param>
        /// <param name="constructorItem">The parent <see cref="InfoItem"/> (typically a constructor or method).</param>
        public TypeParameter(System.Type typeParameter, InfoItem constructorItem)
        {
            Data = typeParameter;
            Parent = constructorItem;
        }

        /// <summary>
        /// Returns a markdown representation of this type parameter.
        /// </summary>
        /// <returns>A new <see cref="Markdown.Page"/> instance.</returns>
        public IMarkdownElement GetMarkdown() => _markdownGetter.Markdown();
        
    }
}
