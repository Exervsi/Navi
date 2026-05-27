using Navi.Core.Getters.MarkdownGetters;
using Navi.Core.SystemExtensions.String;
using Navi.InfoItems;
using Navi.Markdown;
using Navi.MarkdownElement;
using System.Data.Common;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Navi.Core.Getters.MarkdownGetters;

internal struct ReturnMarkdownGetter :InfoItemMarkdownGetter
{
    private InfoItems.Return _returnItem;

    internal ReturnMarkdownGetter(InfoItems.Return returnItem)
    {
        _returnItem = returnItem;
    }

    public string Name => "return" + _returnItem.Data.ParameterType.Name;

    public IMarkdownElement Markdown() => new Page();

}
