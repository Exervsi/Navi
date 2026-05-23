using System.Xml.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Navi.InfoItems;
using Navi.Markdown;

namespace Navi.Core.SystemExtensions.String
{

    internal static class Docusuaurus
    {

        internal static void Print(DocuTree tree, string path)
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
                    string link = PathBuilders.BuildChildUrl(namespaceItem) + "/Index";
                    string path = PathBuilders.BuildGlobalPath(namespaceItem) + "/Index";
                    path = path.Replace("/", "\\") + ".mdx";
                    markdown.Print(path);

                    Print(namespaceItem.Children);
                }
                if (item is InfoItems.Type typeItem)
                {
                    Page markdown = typeItem.GetMarkdown() as Page;
                    markdown.Elements.Insert(0, new DocusaurusSidebar(typeItem.Name, index++));
                    string link = PathBuilders.BuildChildUrl(typeItem);
                    string path = PathBuilders.BuildGlobalPath(typeItem) + "/Index";
                    path = path.Replace("/", "\\") + ".mdx";
                    markdown.Print(path);

                    //we are going to handle constructors here/
                    Page constructorMarkdown = new Page();
                    foreach (Constructor constructor in typeItem.Constructors)
                        constructorMarkdown.Elements.AddRange((constructor.GetMarkdown() as Page).Elements);
                           
                    constructorMarkdown.Elements.Insert(0, new DocusaurusSidebar("Constructors", index++));
                    string constructorPath = PathBuilders.BuildGlobalPath(typeItem) + "/Constructors";
                    constructorPath = constructorPath.Replace("/", "\\") + ".mdx";
                    constructorMarkdown.Print(constructorPath);

                    Print(typeItem.Children);
                }

                if (item is Method methodItem)
                {
                    Page markdown = methodItem.GetMarkdown() as Page;
                    markdown.Elements.Insert(0, new DocusaurusSidebar(methodItem.Name, index++));
                    string path = PathBuilders.BuildGlobalPath(methodItem);
                    path = path.Replace("/", "\\") + ".mdx";
                    markdown.Print(path);
                }
            }

        }
    }
}
