using Navi.Core.SystemExtensions.String;
using Navi.Markdown;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Xml;
using Navi.Core.Extensions.Collections;

namespace Navi.InfoItems
{

    public class DocuTree : InfoItem
    {

        public string Name => Assembly.FullName;

        public string Path { get; set; }

        public InfoItem this[string name] => Children.FirstOrDefault(x => x.Name == name);

        public string Key => "";


        public string Value { get; set; }

        public InfoItem? Parent => null;

        public DocuTree Root => this;


        public InfoItem[] Children => Namespaces;


        public Attribute[] Attributes => null;

        public string AssemblyPath { get; }
        internal Assembly Assembly { get; }
        public string DocumentationPath { get; }
        internal Dictionary<string, string> Documentation { get; }

        public Namespace[] Namespaces { get; }



        public DocuTree(string assemblyPath, string documentationPath)
        {
            if (assemblyPath.TryLoadAssembly(out Assembly? assembly))
            {
                AssemblyPath = assemblyPath;
                Assembly = assembly;
            }
            if (documentationPath.TryLoadAssemblyDocumentation(out Dictionary<string, string> documentation))
            {
                DocumentationPath = documentationPath;
                Documentation = documentation;
            }

            //now lets plant the docuTree

            if (Assembly == null || Documentation == null)
                throw new Exception("Failed to load assembly or documentation.");

            List<Namespace> list = Assembly
                .GetTypes()
                .Select(x => x.Namespace)
                .Where(x => x != null)
                .Distinct()
                .OrderBy(x => x)
                .Where(x => Documentation.Keys.Where(y => y.Contains(x)).Any())
                .Select(x => new Namespace(x, this, Assembly.GetTypes()))
                .ToList();

            Dictionary<Namespace, List<Namespace>> dictionary = list.ToNamespaceDictionary();

            //for everynamespace
            for (int i = 0; i < list.Count; i++)
            {
                Namespace key = list[i];
                List<Namespace> dependency = dictionary[key];
                key.SetNamespaces(dictionary[key].ToArray());
                for (int j = 0; j < dictionary[key].Count; j++)
                {
                    int index = list.IndexOf(dictionary[key][j]);
                    list[index].SetParent(key);
                    list[index].SetNamespaces(dictionary[list[index]].ToArray());
                }
            }

            Namespaces = list.Where(x => x.Parent == this).ToArray();
        }

        ///Helper function used by extension methods.
        internal string XmlDocumentationKeyHelper(string typeFullNameString, string memberNameString)
        {
            string key = Regex.Replace(typeFullNameString, @"\[.*\]", string.Empty).Replace('+', '.');
            if (memberNameString != null)
                key += "." + memberNameString;

            return key;
        }

        public IMarkdownElement GetMarkdown()
        {
            return new Page();
        }
    }
}
