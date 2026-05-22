using Navi.Core.Getters.TreeGetters;
using Navi.Core.SystemExtensions.String;
using Navi.InfoItems;
using Navi.Markdown;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Navi.Core.Getters.XmlGetters;

internal class ParameterXmlGetter : InfoItemXmlGetter
{
    internal ParameterXmlGetter(InfoItems.Parameter parameterItem)
    {
        _infoItem = parameterItem;
    }

    internal override string Key => "";

    internal override string Value
    {
        get
        {
            string xmlKey = "";
            if (_infoItem.Parent is Constructor constructor)
            {
                XElement? element = new ConstructorXmlGetter(constructor).Element?.Elements("param").Where(x => (x.FirstAttribute.Value == _infoItem.Name)).FirstOrDefault();
                if (element is not null)
                    xmlKey = element.Value.Trim();
            }
            if (_infoItem.Parent is Method method)
            {
                XElement? element = new MethodXmlGetter(method).Element?.Elements("param").Where(x => (x.FirstAttribute.Value == _infoItem.Name)).FirstOrDefault();
                if (element is not null)
                    xmlKey = element.Value.Trim();
            }
            return xmlKey;
        }
    }
}
