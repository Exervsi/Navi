using Navi.Core.SystemExtensions.String;
using Navi.InfoItems;
using Navi.Markdown;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Navi.Core.Getters.TreeGetters;

internal class ParameterTreeGetter : InfoItemTreeGetter
{

    internal ParameterTreeGetter(InfoItems.Parameter parameterItem)
    {
        _infoItem = parameterItem;
    }

    internal override InfoItem[] Children => new InfoItem[0];


}
