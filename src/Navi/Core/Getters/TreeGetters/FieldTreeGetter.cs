using Navi.Core.SystemExtensions.String;
using Navi.InfoItems;
using Navi.Markdown;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Navi.Core.Getters.TreeGetters;

internal class FieldTreeGetter : InfoItemTreeGetter
{
    private Field _fieldItem;


    internal FieldTreeGetter(Field fieldItem)
    {
        _infoItem = fieldItem;
        _fieldItem = fieldItem;
    }

    internal override InfoItem[] Children => new InfoItem[0];

    internal InfoItems.Attribute[] Attributes => _fieldItem.Data.CustomAttributes.Select(x => new InfoItems.Attribute(x, _fieldItem)).ToArray();

}
