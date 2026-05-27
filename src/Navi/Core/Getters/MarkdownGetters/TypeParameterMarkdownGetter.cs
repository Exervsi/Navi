using Navi.Core.Getters.MarkdownGetters;
using Navi.Core.SystemExtensions.String;
using Navi.InfoItems;
using Navi.Markdown;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Navi.Core.Getters.MarkdownGetters;

internal struct TypeParameterMarkdownGetter :InfoItemMarkdownGetter
{
    private TypeParameter _typeParameterItem;

    internal TypeParameterMarkdownGetter(TypeParameter typeParameterItem)
    {
        _typeParameterItem = typeParameterItem;
    }


    public string Name => _typeParameterItem.Name;
    


    public IMarkdownElement Markdown() => new Page();
    


}
