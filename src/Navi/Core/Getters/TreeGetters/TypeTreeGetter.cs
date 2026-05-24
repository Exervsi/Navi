using Navi.Core.SystemExtensions.String;
using Navi.InfoItems;
using Navi.Markdown;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Navi.Core.Getters.TreeGetters;

internal class TypeTreeGetter : InfoItemTreeGetter
{
    private InfoItems.Type _typeItem;
    internal TypeTreeGetter(InfoItems.Type typeItem)
    {
        _infoItem = typeItem;
        _typeItem = typeItem;
    }

    internal override InfoItem[] Children
    {
        get
        {
            List<InfoItem> result = new List<InfoItem>();
            result.AddRange(Constructors);
            result.AddRange(Methods);
            result.AddRange(Fields);
            result.AddRange(Properties);
            return result.ToArray();
        }
    
    }

    internal Constructor[] Constructors =>
        _typeItem.Data.GetConstructors(BindingFlags.Instance |
                                       BindingFlags.Static |
                                       BindingFlags.Public |
                                       BindingFlags.DeclaredOnly)
                .Select(x => new Constructor(x, _typeItem))
                .ToArray();

    internal Method[] Methods =>
        _typeItem.Data.GetMethods(BindingFlags.Instance |
                                  BindingFlags.Static |
                                  BindingFlags.Public |
                                  BindingFlags.DeclaredOnly)
                .Where(x => x.Name.StartsWith("get_") == false && x.Name.StartsWith("set_") == false)
                .Select(x => new Method(x, _typeItem))
                .ToArray();

    internal Field[] Fields =>
            _typeItem.Data.GetFields(BindingFlags.Instance |
                                     BindingFlags.Static |
                                     BindingFlags.Public |
                                     BindingFlags.DeclaredOnly)
                .Select(x => new Field(x, _typeItem))
                .ToArray();

    internal Property[] Properties =>
            _typeItem.Data.GetProperties(BindingFlags.Instance |
                                         BindingFlags.Static |
                                         BindingFlags.Public |
                                         BindingFlags.DeclaredOnly)
                .Select(x => new Property(x, _typeItem))
                .ToArray();

    internal TypeParameter[] TypeParameters =>
        _typeItem.Data.GetGenericArguments()
            .Select(x => new TypeParameter(x, _typeItem))
            .ToArray();

    internal InfoItems.Attribute[] Attributes => new InfoItems.Attribute[0];


}
