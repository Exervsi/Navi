using Navi.Core.Getters.TreeGetters;
using Navi.Core.SystemExtensions.String;
using Navi.InfoItems;
using Navi.Markdown;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Navi.Core.Getters.XmlGetters;

internal class NamespaceXmlGetter : InfoItemXmlGetter
{
    internal NamespaceXmlGetter(Namespace namespaceItem)
    {
        _infoItem = namespaceItem;
    }

    internal override string Key => "";


    internal override string Value => "";
}
