using Navi.Core.Getters.XmlGetters;
using Navi.Markdown;
using System.Reflection;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace Navi.InfoItems
{
    /// <summary>
    /// Represents a parameter within a documentation tree, providing access to its name, value, type, and related metadata.
    /// </summary>
    public class Parameter : InfoItem
    {

        private ParameterXmlGetter _xmlGetter => new(this);

        /// <summary>
        /// Gets the name of the parameter.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the child <see cref="InfoItem"/> with the specified name, or <c>null</c> if not found.
        /// </summary>
        /// <param name="name">The name of the child item to retrieve.</param>
        public InfoItem this[string name] => Children.FirstOrDefault(x => x.Name == name);

        /// <summary>
        /// Gets the key associated with the parameter. Always returns an empty string.
        /// </summary>
        public string Key => _xmlGetter.Key;

        /// <summary>
        /// Gets the XML documentation value for the parameter, if available.
        /// </summary>
        public string Value => _xmlGetter.Value;

        /// <summary>
        /// Gets the data type of this item, which is always "Parameter".
        /// </summary>
        public string DataType => "Parameter";

        /// <summary>
        /// Gets the reflection metadata for the parameter.
        /// </summary>
        public ParameterInfo Data { get; }

        /// <summary>
        /// Gets the parent <see cref="InfoItem"/> in the documentation tree.
        /// </summary>
        public InfoItem Parent { get; }

        /// <summary>
        /// Gets the root <see cref="DocuTree"/> of the documentation tree.
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
        /// Gets the child items of this parameter.
        /// </summary>
        public InfoItem[] Children { get; }

        /// <summary>
        /// Gets or sets the attributes associated with this parameter.
        /// </summary>
        public Attribute[] Attributes { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Parameter"/> class.
        /// </summary>
        /// <param name="parameter">The reflection metadata for the parameter.</param>
        /// <param name="constructorItem">The parent item, typically a constructor or method.</param>
        public Parameter(ParameterInfo parameter, InfoItem constructorItem)
        {
            Name = parameter.Name;
            Data = parameter;
            Parent = constructorItem;
        }

        /// <summary>
        /// Returns a Markdown representation of the parameter.
        /// </summary>
        /// <returns>A <see cref="IMarkdownElement"/> representing the parameter.</returns>
        public IMarkdownElement GetMarkdown()
        {
            return new Markdown.Page();
        }

    }
}
