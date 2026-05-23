using System.Reflection;

namespace Navi.InfoItems
{
    
    /// <summary>
    /// Represents a custom attribute applied to a code element, providing access to its name, constructor arguments, values, and parent relationships.
    /// </summary>
    public class Attribute
    {
        /// <summary>
        /// Gets the name of the attribute type.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the underlying <see cref="CustomAttributeData"/> instance for this attribute.
        /// </summary>
        public CustomAttributeData Data { get; }

        /// <summary>
        /// Gets an array of argument types for the attribute's constructor arguments.
        /// </summary>
        public System.Type[] Aruguments => Data.ConstructorArguments.Select(x => x.ArgumentType).ToArray();

        /// <summary>
        /// Gets an array of values for the attribute's constructor arguments.
        /// </summary>
        public object[] Values => Data.ConstructorArguments.Select(x => x.Value).ToArray();

        /// <summary>
        /// Gets the parent <see cref="InfoItem"/> to which this attribute is attached.
        /// </summary>
        public InfoItem Parent { get; }

        /// <summary>
        /// Gets the root <see cref="DocuTree"/> of the documentation hierarchy containing this attribute.
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
        /// Gets the child <see cref="InfoItem"/> elements of this attribute, if any.
        /// </summary>
        public InfoItem[] Children { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Attribute"/> class with the specified attribute data and parent item.
        /// </summary>
        /// <param name="attribute">The custom attribute data.</param>
        /// <param name="parent">The parent <see cref="InfoItem"/>.</param>
        public Attribute(CustomAttributeData attribute, InfoItem parent)
        {
            Name = attribute.AttributeType.Name;
            Parent = parent;
            Data = attribute;
        }
    }

    
}
