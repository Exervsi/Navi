using Navi.Core.SystemExtensions.String;
using Navi.InfoItems;
using Navi.Markdown;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Navi.Core.Getters.TreeGetters;

internal class MethodTreeGetter : InfoItemTreeGetter
{
    private Method _methodItem;
    internal MethodTreeGetter(Method methodItem)
    {
        _infoItem = methodItem;
        _methodItem = methodItem;
    }

    internal override InfoItem[] Children
    {
        get
        {
            int typeParameterCount = 0;

            List<InfoItem> result = new List<InfoItem>();
            result.AddRange(TypeParameters);
            result.AddRange(Parameters);
            result.Add(Return);
            return result.ToArray();
        }
    }

    internal InfoItems.Parameter[] Parameters => _methodItem.Data.GetParameters()
                .Select(x => new InfoItems.Parameter(x, _methodItem))
                .ToArray();

    internal InfoItems.TypeParameter[] TypeParameters => _methodItem.Data.GetGenericArguments()
                .Select(x => new TypeParameter(x, _methodItem))
            .ToArray();

    internal InfoItems.Return Return => new Return(_methodItem.Data.ReturnParameter, _methodItem);

    internal InfoItems.Attribute[] Attributes => _methodItem.Data.CustomAttributes.Select(x => new InfoItems.Attribute(x, _methodItem)).ToArray();
}
