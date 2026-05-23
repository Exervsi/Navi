using Navi.Core.Getters;
using Navi.Core.Getters.MarkdownGetters;
using Navi.Core.Getters.TreeGetters;
using Navi.Core.SystemExtensions.String;
using Navi.Markdown;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Navi.InfoItems
{
    /// <summary>
    /// Represents a method within the documentation model, encapsulating metadata, parameters, return type, and related information.
    /// </summary>
    public class Method : InfoItem
    {

        private MethodMarkdownGetter  _markdownGetter => new MethodMarkdownGetter(this);
        private MethodXmlGetter _xmlGetter => new MethodXmlGetter(this);
        private MethodTreeGetter _treeGetter => new MethodTreeGetter(this);

        /// <summary>
        /// Gets the name of the method.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the documentation URL path for this method.
        /// </summary>
        public string Path => PathBuilders.BuildUrl(this);

        /// <summary>
        /// Gets a child <see cref="InfoItem"/> by name, or null if not found.
        /// </summary>
        /// <param name="name">The name of the child item.</param>
        public InfoItem this[string name] => Children.FirstOrDefault(x => x.Name == name);

        /// <summary>
        /// Gets the unique key for this method, as provided by the getter.
        /// </summary>
        public string Key => _xmlGetter.Key;

        /// <summary>
        /// Gets the value representation for this method, as provided by the getter.
        /// </summary>
        public string Value => _xmlGetter.Value;

        internal XElement? Element => _xmlGetter.Element;

        /// <summary>
        /// Gets the underlying <see cref="MethodInfo"/> reflected from the method.
        /// </summary>
        public MethodInfo Data { get; }

        /// <summary>
        /// Gets the parent <see cref="InfoItem"/> of this method, if any.
        /// </summary>
        public InfoItem Parent { get; }

        /// <summary>
        /// Gets the root <see cref="DocuTree"/> for this method.
        /// </summary>
        public DocuTree Root => _treeGetter.Root;

        /// <summary>
        /// Gets the collection of child <see cref="InfoItem"/>s, including type parameters, parameters, and return value.
        /// </summary>
        public InfoItem[] Children => _treeGetter.Children;

        /// <summary>
        /// Gets the type parameters for this method, if any.
        /// </summary>
        public TypeParameter[] TypeParameters => _treeGetter.TypeParameters;
        /// <summary>
        /// Gets the parameters for this method.
        /// </summary>
        public Parameter[] Parameters => _treeGetter.Parameters;

        /// <summary>
        /// Gets the return value information for this method.
        /// </summary>
        public Return Return => _treeGetter.Return;

        /// <summary>
        /// Gets the attributes applied to this method.
        /// </summary>
        public Attribute[] Attributes { get; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Method"/> class using the specified <see cref="MethodInfo"/> and parent type.
        /// </summary>
        /// <param name="method">The reflected method information.</param>
        /// <param name="parent">The parent type containing this method.</param>
        public Method(MethodInfo method, Type parent)
        {
            Name = method.Name;

            if (Name.Contains("Method"))
                Name = method.Name;

            Data = method;
            Parent = parent as InfoItem;

            Attributes = new Attribute[0];
        }

        /// <summary>
        /// Generates a markdown representation of this method for documentation purposes.
        /// </summary>
        /// <returns>An <see cref="IMarkdownElement"/> representing the method.</returns>
        public IMarkdownElement GetMarkdown()
            => _markdownGetter.Markdown();

    }
}
