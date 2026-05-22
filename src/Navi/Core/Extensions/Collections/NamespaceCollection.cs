using Navi.InfoItems;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Navi.Core.Extensions.Collections;

internal static class NamespaceCollectionExtensions
{

    internal static Dictionary<Namespace, List<Namespace>> ToNamespaceDictionary(this List<Namespace> list)
    {

        Dictionary<Namespace, List<Namespace>> result = new Dictionary<Namespace, List<Namespace>>();

        foreach (Namespace name in list)
            result.Add(name, new List<Namespace>());

        List<Namespace> EmbedNamespaces(List<Namespace> namespaces)
        {
            List<Namespace> roots = new List<Namespace>();
            List<Namespace> embedded = new List<Namespace>();

            //we are recursively 
            for (int i = 0; i < namespaces.Count; i++)
            {
                bool isRoot = true;
                for (int j = 0; j < namespaces.Count; j++)
                    if (j != i && namespaces[i].Name.StartsWith(namespaces[j].Name))
                        isRoot = false;
                if (isRoot)
                    roots.Add(namespaces[i]);
                else
                    embedded.Add(namespaces[i]);
            }

            if (embedded.Count > 0)
                embedded = EmbedNamespaces(embedded);

            for (int i = 0; i < embedded.Count; i++)
                for (int j = 0; j < roots.Count; j++)
                    if (embedded[i].Name.StartsWith(roots[j].Name))
                        result[roots[j]].Add(embedded[i]);

            return roots;

        }

        EmbedNamespaces(list);

        return result;
    }


}
