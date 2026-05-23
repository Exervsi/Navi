using Navi.Markdown;
using System.Reflection;
using System.Xml.Linq;

namespace Navi.InfoItems
{
/// <summary>
/// Represents the return value information of a method, encapsulating metadata and documentation details.
/// </summary>
public class Return : InfoItem
{
    /// <summary>
    /// Gets the name of the return item, constructed from the parameter type name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets a child <see cref="InfoItem"/> by name, or <c>null</c> if not found.
    /// </summary>
    /// <param name="name">The name of the child item.</param>
    public InfoItem this[string name] => Children.FirstOrDefault(x => x.Name == name);

    /// <summary>
    /// Gets the key associated with this return item. Always returns an empty string.
    /// </summary>
    public string Key => "";

    /// <summary>
    /// Gets the XML documentation value for the return element, if available.
    /// </summary>
    public string Value
    {
        get
        {
            if (Parent is Method method)
            {
                XElement? element = method.Element?.Elements("returns").FirstOrDefault();
                if (element is not null)
                {
                    //TO DO : Find cref in tree.
                    string value = "";
                    foreach (XNode node in element.Nodes())
                    {
                        if (node is XElement el && el.Name == "see" && el.Attribute("cref") is not null)
                        {
                            string crefValue = el.Attribute("cref").Value.TrimStart('T', ':');
                                value += crefValue;
                        }
                        else
                        {
                            value += node.ToString();
                        }

                    }
                    return value;
                }
            }
                return string.Empty;
        }
    }

    /// <summary>
    /// Gets the full type name of the return parameter.
    /// </summary>
    public string Type => Data.ParameterType.FullName;

    /// <summary>
    /// Gets the <see cref="ParameterInfo"/> associated with the return value.
    /// </summary>
    public ParameterInfo Data { get; }

    /// <summary>
    /// Gets the parent <see cref="InfoItem"/> of this return item.
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
    /// Gets the child <see cref="InfoItem"/> elements of this return item.
    /// </summary>
    public InfoItem[] Children { get; }

    /// <summary>
    /// Gets or sets the attributes associated with this return item.
    /// </summary>
    public Attribute[] Attributes { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Return"/> class.
    /// </summary>
    /// <param name="parameter">The parameter info representing the return value.</param>
    /// <param name="methodItem">The parent method item.</param>
    public Return(ParameterInfo parameter, Method methodItem)
    {
        Name = "return" + parameter.ParameterType.Name;
        Data = parameter;
        Parent = methodItem;
    }

    /// <summary>
    /// Generates a markdown representation of the return item.
    /// </summary>
    /// <returns>A new <see cref="Markdown.Page"/> instance.</returns>
    public IMarkdownElement GetMarkdown()
    {
        return new Markdown.Page();
    }


    }
}
