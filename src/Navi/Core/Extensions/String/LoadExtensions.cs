using Navi.InfoItems;
using Navi.Markdown;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;


namespace Navi.Core.SystemExtensions.String
{

    /// <summary>
    /// Provides extension methods for loading assemblies and their XML documentation from file paths.
    /// </summary>
    public static class LoadExtensions
    {
        /// <summary>
        /// Attempts to load a .NET assembly from the specified file path.
        /// </summary>
        /// <param name="assemblyPath">The file path to the assembly (.dll) to load.</param>
        /// <param name="assembly">
        /// When this method returns, contains the loaded <see cref="Assembly"/> if the operation succeeded, or <c>null</c> if it failed.
        /// </param>
        /// <returns>
        /// <c>true</c> if the assembly was successfully loaded; otherwise, <c>false</c>.
        /// </returns>
        public static bool TryLoadAssembly(this string assemblyPath, out Assembly? assembly)
        {

            if (File.Exists(assemblyPath) && (assemblyPath.EndsWith(".dll")))
            {
                assembly = Assembly.LoadFrom(assemblyPath);
                return true;
            }
            assembly = null;
            return false;
        }

        /// <summary>
        /// Attempts to load XML documentation from the specified file path and parse its member documentation.
        /// </summary>
        /// <param name="documentationPath">The file path to the XML documentation file.</param>
        /// <param name="documentation">
        /// When this method returns, contains a dictionary mapping member names to their XML documentation if the operation succeeded, or <c>null</c> if it failed.
        /// </param>
        /// <returns>
        /// <c>true</c> if the XML documentation was successfully loaded and parsed; otherwise, <c>false</c>.
        /// </returns>
        internal static bool TryLoadAssemblyDocumentation(this string documentationPath, out Dictionary<string, string>? documentation)
        {
            documentation = new Dictionary<string, string>();
            if (File.Exists(documentationPath) && (documentationPath.EndsWith(".xml")))
            {
                using (XmlReader xmlReader = XmlReader.Create(documentationPath, new XmlReaderSettings()))
                    while (xmlReader.Read())
                        if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "member")
                        {
                            string name = xmlReader["name"];
                            documentation[name] = xmlReader.ReadInnerXml();
                        }
                return true;
            }

            documentation = null;
            return false;
            
        }

    }
}
