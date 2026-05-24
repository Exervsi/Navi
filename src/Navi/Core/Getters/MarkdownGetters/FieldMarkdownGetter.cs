using Navi.Core.Getters.MarkdownGetters;
using Navi.Core.SystemExtensions.String;
using Navi.InfoItems;
using Navi.Markdown;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Navi.Core.Getters.MarkdownGetters;

internal struct FieldMarkdownGetter :InfoItemMarkdownGetter
{
    private Field _fieldItem;

    internal FieldMarkdownGetter(Field fieldItem)
    {
        _fieldItem = fieldItem;
    }

    public string Name => _fieldItem.Name;

    public IMarkdownElement Markdown() => new Page();

}
