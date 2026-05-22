using Navi.Core.SystemExtensions.String;
using Navi.Markdown;
using System.Reflection;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace Navi.InfoItems
{
    /// <summary>
    /// Represents a type (class, struct, etc.) in the documentation model, providing access to its metadata,
    /// members, and XML documentation. Inherits from <see cref="InfoItem"/>.
    /// </summary>
    public class Type : InfoItem
    {
        /// <summary>
        /// Gets the name of the type.
        /// </summary>
        public string Name { get; }

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
        public string Key => $"T:{Data.FullName}";

        /// <summary>
        /// Gets the summary documentation value for this type, or an empty string if not available.
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
        /// Gets the reflection metadata for this type.
        /// </summary>
        public TypeInfo Data { get; }

        /// <summary>
        /// Gets the full name of the type.
        /// </summary>
        public string FullName => Data.FullName;

        /// <summary>
        /// Gets the parent <see cref="InfoItem"/> in the documentation tree.
        /// </summary>
        public InfoItem Parent { get; }

        /// <summary>
        /// Gets the root <see cref="DocuTree"/> of the documentation hierarchy.
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
        /// Gets the child members (constructors, methods, fields, properties) of this type.
        /// </summary>
        public InfoItem[] Children { get; }

        /// <summary>
        /// Gets the constructors defined in this type.
        /// </summary>
        public Constructor[] Constructors { get; }

        /// <summary>
        /// Gets the methods defined in this type.
        /// </summary>
        public Method[] Methods { get; }

        /// <summary>
        /// Gets the fields defined in this type.
        /// </summary>
        public Field[] Fields { get; }

        /// <summary>
        /// Gets the properties defined in this type.
        /// </summary>
        public Property[] Properties { get; }

        /// <summary>
        /// Gets or sets the attributes applied to this type.
        /// </summary>
        public Attribute[] Attributes { get; set; }

        /// <summary>
        /// Gets the XML documentation element for this type, if available.
        /// </summary>
        private XElement? Element => Wakka.LoadXml(Root.DocumentationPath, Key);

        /// <summary>
        /// Initializes a new instance of the <see cref="Type"/> class using reflection metadata and a parent namespace.
        /// </summary>
        /// <param name="type">The reflection type to represent.</param>
        /// <param name="parent">The parent namespace item.</param>
        public Type(System.Type type, Namespace parent)
        {
            Name = type.Name;
            Data = type.GetTypeInfo();

            // Parent and children initialization
            Parent = parent;
            List<InfoItem> list = new List<InfoItem>();
            Constructors = Data.GetConstructors()
                .Select(x => new Constructor(x, this))
                .ToArray();

            list.AddRange(Constructors);

            Methods = Data.GetMethods(BindingFlags.Instance |
                                      BindingFlags.Static |
                                      BindingFlags.Public |
                                      BindingFlags.DeclaredOnly)
                .Where(x => x.Name.StartsWith("get_") == false && x.Name.StartsWith("set_") == false)
                .Select(x => new Method(x, this))
                .ToArray();

            list.AddRange(Methods);

            Fields = Data.GetFields()
                .Select(x => new Field(x, this))
                .ToArray();

            list.AddRange(Fields);

            Properties = Data.GetProperties()
                .Select(x => new Property(x, this))
                .ToArray();

            list.AddRange(Properties);

            Children = list.ToArray();
        }

        /// <summary>
        /// Generates a Markdown representation of this type, including its summary, constructors, properties, fields, and methods.
        /// </summary>
        /// <returns>An <see cref="IMarkdownElement"/> representing the type documentation.</returns>
        public IMarkdownElement GetMarkdown()
        {
            // A type is represented as a collection of Markdown tables

            Markdown.Page result = new Markdown.Page();
            result.Elements.Add(new Markdown.Header(Name, 1));
            result.Elements.Add(new Markdown.Text(FullName));

            if (Value != "")
                result.Elements.Add(new Markdown.Text(Value));

            if (Constructors.Length > 0)
            {
                result.Elements.Add(new Markdown.Header("Constructors", 2));
                string[] tableHeaders = new string[2]
                {
                    "Constructor",
                    "Description"
                };

                if (Tables.TryGetTableValues2(Constructors, out string[,] tableValues))
                    result.Elements.Add(new Markdown.Table(tableHeaders, tableValues));
            }

            if (Properties.Length > 0)
            {
                result.Elements.Add(new Markdown.Header("Properties", 2));
                string[] tableHeaders = new string[3]
                {
                    "Type",
                    "Property",
                    "Description",
                };

                if (Tables.TryGetTableValues3(Properties, out string[,] tableValues))
                    result.Elements.Add(new Markdown.Table(tableHeaders, tableValues));
            }

            if (Fields.Length > 0)
            {
                result.Elements.Add(new Markdown.Header("Fields", 2));
                string[] tableHeaders = new string[3]
                {
                    "Type",
                    "Field",
                    "Description",
                };

                if (Tables.TryGetTableValues3(Fields, out string[,] tableValues))
                    result.Elements.Add(new Markdown.Table(tableHeaders, tableValues));
            }

            if (Methods.Length > 0)
            {
                result.Elements.Add(new Markdown.Header("Methods", 2));
                string[] tableHeaders = new string[3]
                {
                    "Type",
                    "Method",
                    "Description",
                };

                if (Tables.TryGetTableValues3(Methods, out string[,] tableValues))
                    result.Elements.Add(new Markdown.Table(tableHeaders, tableValues));
            }
            return result;
        }
    }
}
