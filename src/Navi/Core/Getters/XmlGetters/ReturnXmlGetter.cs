using Navi.Core.Getters.TreeGetters;
using Navi.Core.SystemExtensions.String;
using Navi.InfoItems;
using Navi.Markdown;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Navi.Core.Getters.XmlGetters;

internal class ReturnXmlGetter : InfoItemXmlGetter
{
    private Return _returnItem;
    internal ReturnXmlGetter(InfoItems.Return returnItem)
    {
        _infoItem = returnItem;
    }

    internal override string Key => "";


    internal override string Value
    {
        get
        {
            if (_infoItem.Parent is Method method)
            {
                XElement? element = method.Element?.Elements("returns").FirstOrDefault();
                if (element is not null)
                {
                    //TO DO : Find cref in tree.
                    string value = "";
                    foreach (XNode node in element.Nodes())
                    {
                        if (node is XElement elementNode &&
                            elementNode.Name == "see" &&
                            elementNode.Attribute("cref") is not null)
                        {
                            string crefValue = elementNode.Attribute("cref").Value.TrimStart('T', ':');
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
}
