using Navi.Core.SystemExtensions.String;
using Navi.InfoItems;
using Navi.Markdown;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Navi.Core.Getters.TreeGetters;

internal class ConstructorTreeGetter : InfoItemTreeGetter
{
    private Constructor _constructorItem;
    internal ConstructorTreeGetter(Constructor constructorItem)
    {
        _infoItem = constructorItem;
        _constructorItem = constructorItem;
    }

    internal override InfoItem[] Children
    {
        get
        {
            int typeParameterCount = 0;

            List<InfoItem> result = new List<InfoItem>();
            result.AddRange(Parameters);
            return result.ToArray();
        }
    }

    internal InfoItems.Parameter[] Parameters => _constructorItem.Data.GetParameters()
                .Select(x => new InfoItems.Parameter(x, _constructorItem))
                .ToArray();
}
