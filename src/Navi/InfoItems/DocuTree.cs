using Navi.Core.SystemExtensions.String;
using Navi.Markdown;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Xml;
using Navi.Core.Extensions.Collections;
using System.Diagnostics;

namespace Navi.InfoItems
{

    /// <summary>
    /// Represents the root of a documentation tree for a .NET assembly, providing access to namespaces,
    /// types, and their associated XML documentation.
    /// </summary>
    public class DocuTree : InfoItem
    {
        /// <summary>
        /// Gets the full name of the loaded assembly.
        /// </summary>
        public string Name => Assembly.FullName;

        /// <summary>
        /// Gets or sets the path to the documentation or assembly.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets the child <see cref="InfoItem"/> with the specified name, or null if not found.
        /// </summary>
        /// <param name="name">The name of the child item.</param>
        public InfoItem this[string name] => Children.FirstOrDefault(x => x.Name == name);

        /// <summary>
        /// Gets a key for this item. Always returns an empty string for <see cref="DocuTree"/>.
        /// </summary>
        public string Key => "";

        /// <summary>
        /// Gets or sets the value associated with this item.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets the parent item. Always returns null for <see cref="DocuTree"/>.
        /// </summary>
        public InfoItem? Parent => null;

        /// <summary>
        /// Gets the root of the documentation tree, which is this instance.
        /// </summary>
        public DocuTree Root => this;

        /// <summary>
        /// Gets the immediate child items, which are the namespaces in the assembly.
        /// </summary>
        public InfoItem[] Children => Namespaces;

        /// <summary>
        /// Gets the attributes associated with this item. Always returns null for <see cref="DocuTree"/>.
        /// </summary>
        public Attribute[] Attributes => null;

        /// <summary>
        /// Gets the file path of the loaded assembly.
        /// </summary>
        public string AssemblyPath { get; }

        /// <summary>
        /// Gets the loaded <see cref="System.Reflection.Assembly"/> instance.
        /// </summary>
        internal Assembly Assembly { get; }

        /// <summary>
        /// Gets the file path of the loaded XML documentation.
        /// </summary>
        public string DocumentationPath { get; }

        /// <summary>
        /// Gets the loaded XML documentation as a dictionary mapping documentation keys to XML strings.
        /// </summary>
        internal Dictionary<string, string> Documentation { get; }

        /// <summary>
        /// Gets the namespaces defined in the loaded assembly.
        /// </summary>
        public Namespace[] Namespaces { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DocuTree"/> class, loading the specified assembly and its XML documentation.
        /// </summary>
        /// <param name="assemblyPath">The file path to the .NET assembly.</param>
        /// <param name="documentationPath">The file path to the XML documentation file.</param>
        /// <exception cref="Exception">Thrown if the assembly or documentation cannot be loaded.</exception>
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

            try
            {
                Assembly.GetTypes();
            }
            catch (System.Reflection.ReflectionTypeLoadException ex)
            {
                foreach (var inner in ex.LoaderExceptions)
                {
                    Debug.WriteLine(inner.Message);
                }
            }

            List<Namespace> list = Assembly
                .GetTypes()
                .Select(x => x.Namespace)
                .Where(x => x != null)
                .Distinct()
                .OrderBy(x => x)
                .Where(x => Documentation.Keys.Where(y => y.Contains(x)).Any())
                .Select(x => new Namespace(x, this, Assembly.GetTypes()))
                .ToList();

            //if you have no public children then you shouldn't render.
            for(int i = list.Count-1; i >= 0; i--)
                if (list[i].Children.Length == 0)
                    list.RemoveAt(i);

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

        /// <summary>
        /// Helper function used by extension methods to generate a documentation key for a type or member.
        /// </summary>
        /// <param name="typeFullNameString">The full name of the type.</param>
        /// <param name="memberNameString">The name of the member (optional).</param>
        /// <returns>The documentation key string.</returns>
        internal string XmlDocumentationKeyHelper(string typeFullNameString, string memberNameString)
        {
            string key = Regex.Replace(typeFullNameString, @"\[.*\]", string.Empty).Replace('+', '.');
            if (memberNameString != null)
                key += "." + memberNameString;

            return key;
        }

        /// <summary>
        /// Gets a markdown representation of the documentation tree.
        /// </summary>
        /// <returns>An <see cref="IMarkdownElement"/> representing the documentation as markdown.</returns>
        public IMarkdownElement GetMarkdown()
        {
            return new Page();
        }

/// <summary>
/// Exports the current <c>DocuTree</c> instance to a Docusaurus-compatible format at the specified path.
/// </summary>
/// <param name="path">
/// The file system path where the Docusaurus output should be written.
/// </param>
/// <remarks>
/// This method delegates the export operation to <c>Navi.Core.SystemExtensions.String.Docusuaurus.Print</c>,
/// passing the current <c>DocuTree</c> instance and the target path.
/// </remarks>
public void PrintDocusaurus(string path)
{
    Navi.Core.SystemExtensions.String.Docusuaurus.Print(this, path);
}
    }
}
