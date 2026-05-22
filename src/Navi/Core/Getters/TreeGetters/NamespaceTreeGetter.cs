using Navi.Core.SystemExtensions.String;
using Navi.InfoItems;
using Navi.Markdown;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Navi.Core.Getters.TreeGetters;

internal class NamespaceTreeGetter : InfoItemTreeGetter
{
    private Namespace _namespaceItem;

    internal NamespaceTreeGetter(Namespace namespaceItem)
    {
        _infoItem = namespaceItem;
        _namespaceItem = namespaceItem;
    }


    /// <summary>
    /// Gets the child items of this namespace, including both nested namespaces and types.
    /// </summary>
    internal override InfoItem[] Children
    {
        get
        {
            List<InfoItem> result = new List<InfoItem>();
            result.AddRange(_namespaceItem.Namespaces);
            result.AddRange(_namespaceItem.Types);
            return result.ToArray();
        }
    }


}
