using Navi.Core.Getters.MarkdownGetters;
using Navi.Core.SystemExtensions.String;
using Navi.InfoItems;
using Navi.Markdown;
using Navi.MarkdownElement;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Navi.Core.Getters.MarkdownGetters;

internal struct TypeMarkdownGetter :InfoItemMarkdownGetter
{
    private InfoItems.Type _typeItem;

    internal TypeMarkdownGetter(InfoItems.Type typeItem)
    {
        _typeItem = typeItem;
    }

    public string Name
    {
        get
        {
            string result = _typeItem.Data.Name;

            if (_typeItem.TypeParameters.Length > 0)
            {
                result = result.Split('`')[0] + '<';
                for (int i = 0; i < _typeItem.TypeParameters.Length; i++)
                    result += _typeItem.TypeParameters[i].Data.Name + ',';

                result = result.Substring(0, result.Length - 1);
                result += '>';
            }
            return result;
        }
    }

    public IMarkdownElement Markdown()
    {
        {
            // A type is represented as a collection of Markdown tables

            Markdown.Page result = new Markdown.Page();
            result.Elements.Add(new Markdown.Header(Name, 1));
            result.Elements.Add(new Markdown.Text(_typeItem.Data.FullName));

            if (_typeItem.Value != "")
                result.Elements.Add(new Markdown.Text(_typeItem.Value));

            if (_typeItem.Constructors.Length > 0)
            {
                result.Elements.Add(new Markdown.Header("Constructors", 2));
                string[] tableHeaders = new string[2]
                {
                    "Constructor",
                    "Description"
                };

                if (Tables.TryGetTableValues2(_typeItem.Constructors, out string[,] tableValues))
                    result.Elements.Add(new Markdown.Table(tableHeaders, tableValues));
            }

            if (_typeItem.Properties.Length > 0)
            {
                result.Elements.Add(new Markdown.Header("Properties", 2));
                string[] tableHeaders = new string[3]
                {
                    "Type",
                    "Property",
                    "Description",
                };

                if (Tables.TryGetTableValues3(_typeItem.Properties, out string[,] tableValues))
                    result.Elements.Add(new Markdown.Table(tableHeaders, tableValues));
            }

            if (_typeItem.Fields.Length > 0)
            {
                result.Elements.Add(new Markdown.Header("Fields", 2));
                string[] tableHeaders = new string[3]
                {
                    "Type",
                    "Field",
                    "Description",
                };

                if (Tables.TryGetTableValues3(_typeItem.Fields, out string[,] tableValues))
                    result.Elements.Add(new Markdown.Table(tableHeaders, tableValues));
            }

            if (_typeItem.Methods.Length > 0)
            {
                result.Elements.Add(new Markdown.Header("Methods", 2));
                string[] tableHeaders = new string[3]
                {
                    "Type",
                    "Method",
                    "Description",
                };

                if (Tables.TryGetTableValues3(_typeItem.Methods, out string[,] tableValues))
                    result.Elements.Add(new Markdown.Table(tableHeaders, tableValues));
            }
            return result;

        }
    }


}
