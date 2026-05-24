using Navi.Core.SystemExtensions.String;
using Navi.Markdown;
using System.Reflection;
using System.Xml.Linq;

namespace Navi.InfoItems
{

    /// <summary>
    /// Represents a property within the documentation model, providing metadata and access to property information,
    /// XML documentation, and related attributes.
    /// </summary>
    public class Property : InfoItem
    {
        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the child <see cref="InfoItem"/> with the specified name, or <c>null</c> if not found.
        /// </summary>
        /// <param name="name">The name of the child item to retrieve.</param>
        public InfoItem this[string name] => Children.FirstOrDefault(x => x.Name == name);

        /// <summary>
        /// Gets the unique key for this property, used for documentation lookup.
        /// </summary>
        public string Key
        {
            get
            {
                string result = $"P:{(Parent as InfoItems.Type).Data.FullName}.{Name}";
                return result;
            }
        }

        /// <summary>
        /// Gets the summary value from the XML documentation for this property, or an empty string if not available.
        /// </summary>
        public string Value
        {
            get
            {
                if (Element is null || Element.Element("summary") is null)
                    return "";
                return Element.Element("summary").Value.Trim();
            }
        }

        /// <summary>
        /// Gets the underlying <see cref="PropertyInfo"/> for this property.
        /// </summary>
        public PropertyInfo Data { get; }

        /// <summary>
        /// Gets the type of the property.
        /// </summary>
        public System.Type Type => Data.PropertyType;

        /// <summary>
        /// Gets a value indicating whether the property can be read.
        /// </summary>
        public bool IsGettable => Data.CanRead;

        /// <summary>
        /// Gets a value indicating whether the property can be written to.
        /// </summary>
        public bool IsSettable => Data.CanWrite;

        /// <summary>
        /// Gets a value indicating whether the property is static.
        /// </summary>
        public bool IsStatic => (Data.GetMethod ?? Data.SetMethod).IsStatic;

        /// <summary>
        /// Gets the parent <see cref="InfoItem"/> of this property.
        /// </summary>
        public InfoItem Parent { get; }

        /// <summary>
        /// Gets the root <see cref="DocuTree"/> for this property.
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
        /// Gets or sets the child items of this property.
        /// </summary>
        public InfoItem[] Children { get; set; }

        /// <summary>
        /// Gets or sets the attributes applied to this property.
        /// </summary>
        public Attribute[] Attributes { get; set; }

        /// <summary>
        /// Gets the XML element representing this property from the documentation, or <c>null</c> if not found.
        /// </summary>
        internal XElement? Element => PathBuilders.LoadXml(Root.DocumentationPath, Key);

        /// <summary>
        /// Initializes a new instance of the <see cref="Property"/> class.
        /// </summary>
        /// <param name="property">The reflection property info.</param>
        /// <param name="parent">The parent type.</param>
        public Property(PropertyInfo property, Type parent)
        {
            Name = property.Name;
            Data = property;
            Parent = parent as InfoItem;
            Children = new InfoItem[0];
            Attributes = new Attribute[0];
        }

        /// <summary>
        /// Returns a markdown representation of this property.
        /// </summary>
        /// <returns>An <see cref="IMarkdownElement"/> representing the property.</returns>
        public IMarkdownElement GetMarkdown()
        {
            return new Markdown.Page();
        }
    }
}
