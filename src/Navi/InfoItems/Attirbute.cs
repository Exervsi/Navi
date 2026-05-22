using System.Reflection;

namespace Navi.InfoItems
{
    
    public class Attribute
    {

        public string Name { get; }


        public CustomAttributeData Data { get; }

        public System.Type[] Aruguments => Data.ConstructorArguments.Select(x => x.ArgumentType).ToArray();



        public object[] Values => Data.ConstructorArguments.Select(x => x.Value).ToArray();

        public InfoItem Parent { get; }

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

        public InfoItem[] Children { get; }

        public Attribute(CustomAttributeData attribute, InfoItem parent)
        {
            Name = attribute.AttributeType.Name;
            Parent = parent;
            Data = attribute;
        }
    }
    
}
