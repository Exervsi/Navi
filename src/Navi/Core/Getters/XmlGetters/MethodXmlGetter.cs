using Navi.Core.SystemExtensions.String;
using Navi.InfoItems;
using Navi.Markdown;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Navi.Core.Getters;

internal class MethodXmlGetter : InfoItemXmlGetter
{
    internal MethodXmlGetter(Method methodItem)
    {
        _infoItem = methodItem;
    }

    internal override string Key
    {
        get
        {
            Method methodItem = _infoItem as Method;

            string result = $"M:{methodItem.Data.DeclaringType.FullName}.{methodItem.Data.Name}";

            if (methodItem.Data.IsGenericMethod)
                result += "``" + methodItem.TypeParameters.Length;

            if (methodItem.Parameters.Length == 0)
                return result;

            IEnumerable<string> parameterStrings = methodItem.Parameters.Select(x => x.Data.ParameterType.FullName);
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
            return Element.Element("summary").Value.Trim();
        }
    }


}
