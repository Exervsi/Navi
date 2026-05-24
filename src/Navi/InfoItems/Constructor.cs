using Navi.Core.Getters;
using Navi.Core.Getters.MarkdownGetters;
using Navi.Core.Getters.TreeGetters;
using Navi.Core.SystemExtensions.String;
using Navi.InfoItems;
using Navi.Markdown;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using static Microsoft.CodeAnalysis.CSharp.SyntaxTokenParser;

namespace Navi.InfoItems
{

    /// <summary>
    /// Represents a constructor information item within the documentation model.
    /// Provides access to metadata, parameters, attributes, and documentation tree structure
    /// for a specific <see cref="ConstructorInfo"/> instance.
    /// </summary>
    public class Constructor : InfoItem
    {
        /// <summary>
        /// Gets a markdown representation of the constructor documentation.
        /// </summary>
        private ConstructorMarkdownGetter _markdownGetter => new ConstructorMarkdownGetter(this);

        /// <summary>
        /// Gets an XML representation of the constructor documentation.
        /// </summary>
        private ConstructorXmlGetter _xmlGetter => new ConstructorXmlGetter(this);

        /// <summary>
        /// Gets a tree structure representation of the constructor documentation.
        /// </summary>
        private ConstructorTreeGetter _treeGetter => new ConstructorTreeGetter(this);

        /// <summary>
        /// Gets the name of the constructor.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets a child <see cref="InfoItem"/> by name, or null if not found.
        /// </summary>
        /// <param name="name">The name of the child item.</param>
        public InfoItem this[string name] => Children.FirstOrDefault(x => x.Name == name);

        /// <summary>
        /// Gets the documentation path for this constructor.
        /// </summary>
        public string Path => PathBuilders.BuildUrl(this);

        /// <summary>
        /// Gets the XML documentation key for this constructor.
        /// </summary>
        public string Key => _xmlGetter.Key;

        /// <summary>
        /// Gets the XML documentation value for this constructor.
        /// </summary>
        public string Value => _xmlGetter.Value;

        /// <summary>
        /// Gets the underlying <see cref="ConstructorInfo"/> instance.
        /// </summary>
        public ConstructorInfo Data { get; }

        /// <summary>
        /// Gets the parent <see cref="InfoItem"/> in the documentation tree.
        /// </summary>
        public InfoItem Parent { get; }

        /// <summary>
        /// Gets the root <see cref="DocuTree"/> for this constructor.
        /// </summary>
        public DocuTree Root => _treeGetter.Root;

        /// <summary>
        /// Gets the child items of this constructor.
        /// </summary>
        public InfoItem[] Children => _treeGetter.Children;

        /// <summary>
        /// Gets the parameters of the constructor.
        /// </summary>
        public Parameter[] Parameters => _treeGetter.Parameters;

        /// <summary>
        /// Gets the attributes applied to the constructor.
        /// </summary>
        public Attribute[] Attributes { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Constructor"/> class.
        /// </summary>
        /// <param name="constructor">The reflection information for the constructor.</param>
        /// <param name="parent">The parent type of the constructor.</param>
        public Constructor(ConstructorInfo constructor, Type parent)
        {
            Name = parent.Name + '(';
            Data = constructor;
            Parent = parent;

            Attributes = new Attribute[0];

            if (Parameters.Length > 0)
            {
                foreach (Parameter parameter in Parameters)
                    Name += parameter.Data.ParameterType.Name + ',';
                Name = Name.Substring(0, Name.Length - 1);
            }
            Name += ')';

        }

        /// <summary>
        /// Gets the markdown documentation element for this constructor.
        /// </summary>
        /// <returns>An <see cref="IMarkdownElement"/> representing the constructor documentation.</returns>
        public IMarkdownElement GetMarkdown() => _markdownGetter.Markdown();

    }
}
