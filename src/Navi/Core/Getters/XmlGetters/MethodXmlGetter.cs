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

            IEnumerable<string> parameterStrings = methodItem.Parameters.Select(x =>
            {
                var type = x.Data.ParameterType;
                List<string> typeParameterNames = methodItem.TypeParameters.Select(tp => tp.Name).ToList();

                string GetTypeString(System.Type t)
                {
                    if (typeParameterNames.Contains(t.Name))
                        return "``" + typeParameterNames.IndexOf(t.Name);
                    if (t.IsGenericType)
                    {
                        System.Type tt = t.GetGenericTypeDefinition();
                        string genericTypeName = t.GetGenericTypeDefinition().FullName.Split('`')[0];
                        string genericArgs = string.Join(",", t.GetGenericArguments().Select(GetTypeString));
                        return $"{genericTypeName}{{{genericArgs}}}";
                    }
                    return t.FullName;
                }

                return GetTypeString(type);
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
            return Element.Element("summary").Value.Trim();
        }
    }


}
