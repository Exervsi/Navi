using Navi.Core.SystemExtensions.String;
using Navi.InfoItems;
using Navi.Markdown;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Navi.Core.Getters;

internal abstract class InfoItemXmlGetter
{
    internal InfoItem _infoItem { get; set; }

    internal XElement? Element => PathBuilders.LoadXml(_infoItem.Root.DocumentationPath, Key);

    internal abstract string Key { get; }

    internal abstract string Value { get; }


}
