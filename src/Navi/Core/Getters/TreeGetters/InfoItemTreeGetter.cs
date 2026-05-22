using Navi.Core.SystemExtensions.String;
using Navi.InfoItems;
using Navi.Markdown;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Navi.Core.Getters.TreeGetters;

internal abstract class InfoItemTreeGetter
{
    internal InfoItem _infoItem { get; set; }

    internal DocuTree Root
    {
        get
        {
            InfoItem parent = _infoItem.Parent;
            while (parent.Parent is not null)
                parent = parent.Parent;
            return parent as DocuTree;
        }
    }

    internal abstract InfoItem[] Children { get; }

}
