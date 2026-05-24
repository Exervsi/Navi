using Navi.Core.Getters.MarkdownGetters;
using Navi.Core.Getters.TreeGetters;
using Navi.Core.Getters.XmlGetters;
using Navi.Core.SystemExtensions.String;
using Navi.Markdown;
using System.Reflection;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Xml.Linq;

namespace Navi.InfoItems
{
    /// <summary>
    /// Represents a type (class, struct, etc.) in the documentation model, providing access to its metadata,
    /// members, and XML documentation. Inherits from <see cref="InfoItem"/>.
    /// </summary>
    public class Type : InfoItem
    {
        private TypeMarkdownGetter _markdownGetter => new TypeMarkdownGetter(this);
        private TypeTreeGetter _treeGetter => new TypeTreeGetter(this);
        private TypeXmlGetter _typeXmlGetter => new TypeXmlGetter(this);

        /// <summary>
        /// Gets the name of the type.
        /// </summary>
        public string Name => _markdownGetter.Name;

        /// <summary>
        /// Gets or sets the internal path associated with this type.
        /// </summary>
        internal string _path { get; set; }

        /// <summary>
        /// Gets the child <see cref="InfoItem"/> with the specified name, or null if not found.
        /// </summary>
        /// <param name="name">The name of the child item.</param>
        public InfoItem this[string name] => Children.FirstOrDefault(x => x.Name == name);

        /// <summary>
        /// Gets the unique documentation key for this type.
        /// </summary>
        public string Key => _typeXmlGetter.Key;

        /// <summary>
        /// Gets the summary documentation value for this type, or an empty string if not available.
        /// </summary>
        public string Value => _typeXmlGetter.Value;

        /// <summary>
        /// Gets the reflection metadata for this type.
        /// </summary>
        public TypeInfo Data { get; }

        /// <summary>
        /// Gets the parent <see cref="InfoItem"/> in the documentation tree.
        /// </summary>
        public InfoItem Parent { get; }

        /// <summary>
        /// Gets the root <see cref="DocuTree"/> of the documentation hierarchy.
        /// </summary>
        public DocuTree Root => _treeGetter.Root;

        /// <summary>
        /// Gets the child members (constructors, methods, fields, properties) of this type.
        /// </summary>
        public InfoItem[] Children => _treeGetter.Children;

        /// <summary>
        /// Gets the constructors defined in this type.
        /// </summary>
        public Constructor[] Constructors => _treeGetter.Constructors;

        /// <summary>
        /// Gets the methods defined in this type.
        /// </summary>
        public Method[] Methods => _treeGetter.Methods;

        /// <summary>
        /// Gets the fields defined in this type.
        /// </summary>
        public Field[] Fields => _treeGetter.Fields;

        /// <summary>
        /// Gets the properties defined in this type.
        /// </summary>
        public Property[] Properties => _treeGetter.Properties;

        public TypeParameter[] TypeParameters => _treeGetter.TypeParameters;

        /// <summary>
        /// Gets or sets the attributes applied to this type.
        /// </summary>
        public Attribute[] Attributes => _treeGetter.Attributes;

        /// <summary>
        /// Initializes a new instance of the <see cref="Type"/> class using reflection metadata and a parent namespace.
        /// </summary>
        /// <param name="type">The reflection type to represent.</param>
        /// <param name="parent">The parent namespace item.</param>
        public Type(System.Type type, Namespace parent)
        {
            Data = type.GetTypeInfo();
            Parent = parent;
        }

        /// <summary>
        /// Generates a Markdown representation of this type, including its summary, constructors, properties, fields, and methods.
        /// </summary>
        /// <returns>An <see cref="IMarkdownElement"/> representing the type documentation.</returns>
        public IMarkdownElement GetMarkdown() => _markdownGetter.Markdown();

    }
}
