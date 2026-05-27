using Navi.Core.SystemExtensions.String;
using Navi.InfoItems;
using Navi.Markdown;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Navi.Core.Getters.TreeGetters;

internal class LeafTreeGetter : InfoItemTreeGetter
{
    internal LeafTreeGetter(InfoItem leaf)
    {
        _infoItem = leaf;
    }

    internal override InfoItem[] Children => new InfoItem[0];

    internal InfoItems.Attribute[] Attributes => new InfoItems.Attribute[0];


}
