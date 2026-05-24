using Navi.Core.Getters.MarkdownGetters;
using Navi.Core.SystemExtensions.String;
using Navi.InfoItems;
using Navi.Markdown;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Navi.Core.Getters.MarkdownGetters;

internal struct ConstructorMarkdownGetter :InfoItemMarkdownGetter
{
    private Constructor _constructorItem;

    internal ConstructorMarkdownGetter(Constructor constructorItem)
    {
        _constructorItem = constructorItem;
    }


    public string Name
    {
        get
        {
            string result = _constructorItem.Parent.Name + '(';
            

            if (_constructorItem.Parameters.Length > 0)
            {
                foreach (InfoItems.Parameter parameter in _constructorItem.Parameters)
                    result += parameter.Data.ParameterType.Name + ',';
                result = result.Substring(0, Name.Length - 1);
            }
            result += ')';
            return result;
        }
        

    }

    public IMarkdownElement Markdown()
    {
        Page result = new Page();

        string header = _constructorItem.Name;

        if (!header.EndsWith(")"))
            header += "()";

        result.Elements.Add(new Header(header, 1));
        result.Elements.Add(new Text(_constructorItem.Value));

        string typeParameterString = "**Type Parameters :** \n\n";
        string parameterString = "**Parameters :** \n\n";

        foreach (InfoItems.Parameter parameter in _constructorItem.Parameters)
            parameterString += "`" + parameter.Data.ParameterType.Name + "` " + parameter.Name + " : " + parameter.Value + "\n\n";

        if (_constructorItem.Parameters.Length > 0)
        {
            parameterString = parameterString.Substring(0, parameterString.Length - 2);
            result.Elements.Add(new Text(parameterString));
        }
        return result;
    }


}
