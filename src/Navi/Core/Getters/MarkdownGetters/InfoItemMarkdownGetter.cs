using Navi.Core.SystemExtensions.String;
using Navi.InfoItems;
using Navi.Markdown;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Navi.Core.Getters.MarkdownGetters;

internal interface InfoItemMarkdownGetter
{
    string Name { get; }

    IMarkdownElement Markdown();
}
