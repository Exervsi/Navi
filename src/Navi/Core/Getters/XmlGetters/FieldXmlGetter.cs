using Navi.Core.SystemExtensions.String;
using Navi.InfoItems;
using Navi.Markdown;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Navi.Core.Getters;

internal class FieldXmlGetter : InfoItemXmlGetter
{
    private System.Reflection.FieldInfo _data;

    internal FieldXmlGetter(Field fieldItem)
    {
        _infoItem = fieldItem;
        _data = fieldItem.Data;
    }

    internal override string Key => $"F:{_data.DeclaringType.FullName}.{_data.Name}";

    internal override string Value
    {
        get
        {
            XElement? element = Element;
            if (element is null)
                return string.Empty;

            return element.Element("summary").Value.Trim();
        }
    }


}
