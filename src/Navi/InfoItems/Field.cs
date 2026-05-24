using Navi.Core.Getters;
using Navi.Core.Getters.MarkdownGetters;
using Navi.Core.Getters.TreeGetters;
using Navi.Core.SystemExtensions.String;
using Navi.Markdown;
using System.Reflection;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace Navi.InfoItems
{

    /// <summary>
    /// Represents a field within a documentation tree, providing access to metadata, attributes, and documentation comments.
    /// </summary>
    public class Field : InfoItem
    {
        private FieldMarkdownGetter _markdownGetter => new FieldMarkdownGetter(this);
        private FieldTreeGetter _treeGetter => new FieldTreeGetter(this);
        private FieldXmlGetter _xmlGetter => new FieldXmlGetter(this);

        /// <summary>
        /// Gets or sets the name of the field.
        /// </summary>
        public string Name => _markdownGetter.Name;

        /// <summary>
        /// Gets the child <see cref="InfoItem"/> with the specified name, or <c>null</c> if not found.
        /// </summary>
        /// <param name="name">The name of the child item.</param>
        public InfoItem this[string name] => Children.FirstOrDefault(x => x.Name == name);

        /// <summary>
        /// Gets a unique key for the field, used for documentation lookup.
        /// </summary>
        public string Key => _xmlGetter.Key;

        /// <summary>
        /// Gets the summary documentation value for the field, or an empty string if not available.
        /// </summary>
        public string Value => _xmlGetter.Value;

        /// <summary>
        /// Gets the type of the field.
        /// </summary>
        public System.Type Type => Data.FieldType;

        /// <summary>
        /// Gets the reflection metadata for the field.
        /// </summary>
        public FieldInfo Data { get; }

        /// <summary>
        /// Gets the parent <see cref="InfoItem"/> in the documentation tree.
        /// </summary>
        public InfoItem Parent { get; }

        /// <summary>
        /// Gets the root <see cref="DocuTree"/> of the documentation tree.
        /// </summary>
        public DocuTree Root => _treeGetter.Root;

        /// <summary>
        /// Gets or sets the child items of this field.
        /// </summary>
        public InfoItem[] Children => _treeGetter.Children;

        /// <summary>
        /// Gets or sets the attributes applied to this field.
        /// </summary>
        public Attribute[] Attributes => _treeGetter.Attributes;

        /// <summary>
        /// Gets the XML documentation element for this field, or <c>null</c> if not found.
        /// </summary>
        internal XElement? Element => PathBuilders.LoadXml(Root.DocumentationPath, Key);

        /// <summary>
        /// Initializes a new instance of the <see cref="Field"/> class using the specified field metadata and parent type.
        /// </summary>
        /// <param name="field">The reflection metadata for the field.</param>
        /// <param name="parent">The parent type in the documentation tree.</param>
        public Field(FieldInfo field, Type parent)
        {
            Data = field;
            Parent = parent as InfoItem;
        }

        /// <summary>
        /// Returns a markdown representation of the field.
        /// </summary>
        /// <returns>A <see cref="IMarkdownElement"/> representing the field.</returns>
        public IMarkdownElement GetMarkdown() => _markdownGetter.Markdown();
    }
}
