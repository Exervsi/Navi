
using Navi.Core.Getters;
using Navi.Core.Getters.MarkdownGetters;
using Navi.Core.Getters.TreeGetters;
using Navi.Core.Getters.XmlGetters;
using Navi.Markdown;
using System.Reflection;

namespace Navi.InfoItems
{
    /// <summary>
    /// Represents a namespace within the documentation tree, providing access to its child namespaces, types, and attributes.
    /// </summary>
    /// <remarks>
    /// The <see cref="Namespace"/> class is an <see cref="InfoItem"/> that models a .NET namespace, including its contained types and nested namespaces.
    /// It is constructed with a namespace name, a parent <see cref="DocuTree"/>, and an array of <see cref="System.Type"/> objects.
    /// </remarks>
    public class Namespace : InfoItem
    {
        private NamespaceMarkdownGetter _markdownGetter => new NamespaceMarkdownGetter(this, _name);
        private NamespaceXmlGetter _xmlGetter => new NamespaceXmlGetter(this);
        private NamespaceTreeGetter _treeGetter => new NamespaceTreeGetter(this);
        private string _name;


        public string Name => _markdownGetter.Name;

        /// <summary>
        /// Gets or sets the internal path of the namespace.
        /// </summary>
        internal string _path { get; set; }

        /// <summary>
        /// Gets the child <see cref="InfoItem"/> with a name ending with the specified string.
        /// </summary>
        /// <param name="name">The name suffix to search for.</param>
        /// <returns>The matching <see cref="InfoItem"/>, or null if not found.</returns>
        public InfoItem this[string name] => Children.FirstOrDefault(x =>
        {
            if (x.Name.Contains("<"))
            {
                string test = name + "<";
                x.Name.Contains(test);
                return true;
            }
            if (x.Name.EndsWith(name))
                return true;
            return false;
        });

    /// <summary>
    /// Gets the key for this info item. Always returns an empty string for namespaces.
    /// </summary>
    public string Key => _xmlGetter.Key;

    /// <summary>
    /// Gets the value for this info item. Always returns an empty string for namespaces.
    /// </summary>
    public string Value => _xmlGetter.Value;

    /// <summary>
    /// Gets the parent <see cref="InfoItem"/> of this namespace.
    /// </summary>
    public InfoItem Parent { get; private set; }

    /// <summary>
    /// Gets the root <see cref="DocuTree"/> for this namespace.
    /// </summary>
    public DocuTree Root => _treeGetter.Root;

    /// <summary>
    /// Gets the child items of this namespace, including both nested namespaces and types.
    /// </summary>
    public InfoItem[] Children
    {
        get
        {
            List<InfoItem> result = new List<InfoItem>();
            result.AddRange(Namespaces);
            result.AddRange(Types);
            return result.ToArray();
        }
    }

    /// <summary>
    /// Gets the nested namespaces within this namespace.
    /// </summary>
    public Namespace[] Namespaces { get; private set; }

    /// <summary>
    /// Gets the types defined within this namespace.
    /// </summary>
    public InfoItems.Type[] Types { get; }

    /// <summary>
    /// Gets the attributes applied to this namespace.
    /// </summary>
    public Attribute[] Attributes => _treeGetter.Attributes; 

    /// <summary>
    /// Sets the nested namespaces for this namespace.
    /// </summary>
    /// <param name="namespaces">An array of <see cref="Namespace"/> objects.</param>
    internal void SetNamespaces(Namespace[] namespaces)
    {
        Namespaces = namespaces;
    }

    /// <summary>
    /// Sets the parent <see cref="InfoItem"/> for this namespace.
    /// </summary>
    /// <param name="parent">The parent <see cref="InfoItem"/>.</param>
    internal void SetParent(InfoItem parent)
    {
        Parent = parent;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Namespace"/> class.
    /// </summary>
    /// <param name="name">The name of the namespace.</param>
    /// <param name="parent">The parent <see cref="DocuTree"/>.</param>
    /// <param name="types">The types defined in this namespace.</param>
    internal Namespace(string name, DocuTree parent, System.Type[] types)
    {
        _name = name;
        Parent = parent as InfoItem;
        Namespaces = new Namespace[0];
        Types = types.Where(x => x.Namespace == name && !x.Name.Contains("<>c")
        && x.IsPublic)
            .Select(x => new InfoItems.Type(x, this))
            .ToArray();
    }

    /// <summary>
    /// Returns a markdown representation of this namespace.
    /// </summary>
    /// <returns>An <see cref="IMarkdownElement"/> representing the namespace.</returns>
    public IMarkdownElement GetMarkdown()
        => _markdownGetter.Markdown();

    }
    
}
