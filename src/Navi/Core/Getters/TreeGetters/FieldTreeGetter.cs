using Navi.Core.SystemExtensions.String;
using Navi.InfoItems;
using Navi.Markdown;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Navi.Core.Getters.TreeGetters;

internal class FieldTreeGetter : InfoItemTreeGetter
{
    internal FieldTreeGetter(Field fieldItem)
    {
        _infoItem = fieldItem;
    }

    internal override InfoItem[] Children => new InfoItem[0];
}
