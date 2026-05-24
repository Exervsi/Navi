using Navi.Core.Getters.TreeGetters;
using Navi.Core.SystemExtensions.String;
using Navi.InfoItems;
using Navi.Markdown;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Navi.Core.Getters.XmlGetters;

internal class TypeXmlGetter : InfoItemXmlGetter
{
    private InfoItems.Type _typeItem;
    internal TypeXmlGetter(InfoItems.Type typeItem)
    {
        _infoItem = typeItem;
        _typeItem = typeItem;
    }

    internal override string Key => $"T:{_typeItem.Data.FullName}";


    internal override string Value
    {
        get
        {
            if (Element is null || Element.Element("summary") is null)
                return "";
            return Element.Element("summary").Value.Trim();
        }
    }
}
