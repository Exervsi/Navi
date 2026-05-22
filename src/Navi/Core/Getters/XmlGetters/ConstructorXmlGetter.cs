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
            Method methodItem = _infoItem as Method;
            string result = $"M:{_constructorItem.Data.DeclaringType.FullName}.#ctor";

            if (_constructorItem.Data.IsGenericMethod)
                result += "``" + _constructorItem.TypeParameters.Length;

            if (_constructorItem.Parameters.Length == 0)
                return result;

            IEnumerable<string> parameterStrings = _constructorItem.Parameters.Select(x => x.Data.ParameterType.FullName);
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
