using Navi.Core.Getters.MarkdownGetters;
using Navi.Core.SystemExtensions.String;
using Navi.InfoItems;
using Navi.Markdown;
using System.Data.Common;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Navi.Core.Getters.MarkdownGetters;

internal struct MethodMarkdownGetter :InfoItemMarkdownGetter
{
    private Method _methodItem;

    internal MethodMarkdownGetter(Method methodItem)
    {
        _methodItem = methodItem;
    }

    public string Name => _methodItem.Name;

    public IMarkdownElement Markdown()
    {
        Page result = new Page();

        string header = _methodItem.Key.Replace($"M:{_methodItem.Data.DeclaringType.FullName}.", "");

        header = _methodItem.Return.Type + " " + header;

        if (!header.EndsWith(")"))
            header += "()";

        result.Elements.Add(new Header(header, 1));
        result.Elements.Add(new Text(_methodItem.Value));

        string typeParameterString = "**Type Parameters :** \n\n";
        string parameterString = "**Parameters :** \n\n";
        string returnString = $"**Returns :** `{_methodItem.Return.Type}`";
        if (_methodItem.Return.Type != "void")
            returnString += " : " + _methodItem.Return.Value + "\n\n";


        foreach (InfoItems.Parameter parameter in _methodItem.Parameters)
        parameterString += "`" + parameter.Data.ParameterType.Name + "` " + parameter.Name + " : " + parameter.Value + "\n\n";

        if (_methodItem.Parameters.Length > 0)
        {
            parameterString = parameterString.Substring(0, parameterString.Length - 2);
            result.Elements.Add(new Text(parameterString));
        }



        result.Elements.Add(new Text(returnString));

        return result;
    }


}
