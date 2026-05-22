using Navi.Core.Getters.MarkdownGetters;
using Navi.Core.SystemExtensions.String;
using Navi.InfoItems;
using Navi.Markdown;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Navi.Core.Getters.MarkdownGetters;

internal struct NamespaceMarkdownGetter :InfoItemMarkdownGetter
{
    private Namespace _namespaceItem;

    internal NamespaceMarkdownGetter(Namespace namespaceItem)
    {
        _namespaceItem = namespaceItem;
    }

    public IMarkdownElement Markdown()
    {
        {
            //So a type is just a bunch of tables




            Markdown.Page result = new Markdown.Page();

            result.Elements.Add(new Markdown.Header(_namespaceItem.Name, 1));

            if (_namespaceItem.Value != "")
                result.Elements.Add(new Markdown.Text(_namespaceItem.Value));

            if (_namespaceItem.Namespaces.Length > 0)
            {
                result.Elements.Add(new Markdown.Header("Namespaces", 2));
                string[] tableHeaders = new string[2]
                {
                    "Namespace",
                    "Description"
                };

                if (Tables.TryGetTableValues2(_namespaceItem.Namespaces, out string[,] tableValues))
                    result.Elements.Add(new Markdown.Table(tableHeaders, tableValues));
            }

            if (_namespaceItem.Types.Length > 0)
            {
                result.Elements.Add(new Markdown.Header("Types", 2));
                string[] tableHeaders = new string[2]
                {
                    "Type",
                    "Description",
                };

                if (Tables.TryGetTableValues2(_namespaceItem.Types, out string[,] tableValues))
                    result.Elements.Add(new Markdown.Table(tableHeaders, tableValues));
            }

            return result;

        }
    }


}
