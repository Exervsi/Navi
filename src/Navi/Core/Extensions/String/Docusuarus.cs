using System.Xml.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Navi.InfoItems;
using Navi.Markdown;

namespace Navi.Core.SystemExtensions.String
{

    public static class Docusuaurs
    {

        public static void Print(DocuTree tree, string path)
        {
            tree.Path = path;
            Print(tree.Children);
        }

        internal static void Print(InfoItem[] items)
        {
            int index = 0;
            foreach (InfoItem item in items)
            {
                if (item is Namespace namespaceItem)
                {
                    Page markdown = namespaceItem.GetMarkdown() as Page;
                    markdown.Elements.Insert(0, new DocusaurusSidebar(namespaceItem.Name, index++));
                    string link = Wakka.BuildUrl(namespaceItem) + "/Index";
                    string path = Wakka.BuildGlobalPath(namespaceItem) + "/Index";
                    path = path.Replace("/", "\\") + ".mdx";
                    markdown.Print(path);

                    Print(namespaceItem.Children);
                }
                if (item is InfoItems.Type typeItem)
                {
                    Page markdown = typeItem.GetMarkdown() as Page;
                    markdown.Elements.Insert(0, new DocusaurusSidebar(typeItem.Name, index++));
                    string link = Wakka.BuildUrl(typeItem);
                    string path = Wakka.BuildGlobalPath(typeItem) + "/Index";
                    path = path.Replace("/", "\\") + ".mdx";
                    markdown.Print(path);
                    Print(typeItem.Children);
                }
                if (item is Constructor constructorItem)
                {
                    Page markdown = constructorItem.GetMarkdown() as Page;
                    markdown.Elements.Insert(0, new DocusaurusSidebar(constructorItem.Name, index++));
                    string path = Wakka.BuildGlobalPath(constructorItem) + "/Index";
                    path = path.Replace("/", "\\") + ".mdx";
                    markdown.Print(path);
                }
                if (item is Method methodItem)
                {
                    Page markdown = methodItem.GetMarkdown() as Page;
                    markdown.Elements.Insert(0, new DocusaurusSidebar(methodItem.Name, index++));
                    string path = Wakka.BuildGlobalPath(methodItem) + "/Index";
                    path = path.Replace("/", "\\") + ".mdx";
                    markdown.Print(path);
                }
            }

        }
    }
}
