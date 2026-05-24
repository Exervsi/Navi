using Navi.Core.SystemExtensions.String;
using Navi.InfoItems;
using Navi.Markdown;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Navi.Core.Getters;

internal class ConstructorXmlGetter : InfoItemXmlGetter
{
    Constructor _constructorItem;
    internal ConstructorXmlGetter(Constructor constructorItem)
    {
        _infoItem = constructorItem;
        _constructorItem = constructorItem;
    }

    internal override string Key
    {
        get
        {
            string result = $"M:{_constructorItem.Data.DeclaringType.FullName}.#ctor";

            if (_constructorItem.Parameters.Length == 0)
                return result;

            IEnumerable<string> parameterStrings = _constructorItem.Parameters.Select(x =>
            {
                var type = x.Data.ParameterType;
                List<string> typeParameterNames = (_constructorItem.Parent as InfoItems.Type).TypeParameters.Select(x => x.Name).ToList();
                if (typeParameterNames.Contains(type.Name))
                    return "`" + typeParameterNames.IndexOf(type.Name);

                return type.FullName;
            });


            result += $"({string.Join(",", parameterStrings)})";

            return result;
        }
    }

    internal override string Value
    {
        get
        {
            if (Element is null)
                return "";
            XElement element = Element.Element("summary");
            if (element is null)
                return "";

            return element.Value.Trim();
        }
    }


}
