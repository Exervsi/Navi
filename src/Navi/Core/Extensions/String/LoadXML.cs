using System.Xml.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Navi.InfoItems;

namespace Navi.Core.SystemExtensions.String
{

    public static class Wakka
    {
        /// <summary>
        /// Test Method to Load an XML file and find a method by its name.
        /// </summary>
        /// <param name="path">The path to the XML file.</param>
        /// <param name="methodName">The name of the method to find.</param>
        public static XElement? LoadXml(string path, string methodName)
        { 
            XDocument doc = XDocument.Load(path);
            XElement element = doc.Root;

            XElement? method = doc.Descendants("member").FirstOrDefault(x => x.Attribute("name").Value == methodName);

            return method;
        }



        internal static string BuildGlobalPath(InfoItems.InfoItem item)
        {
            if (item is DocuTree root)
                return root.Path;

            string name = item.Name;

            if (item is InfoItems.Constructor constructorItem)
                name = "Constructors";

            return BuildGlobalPath(item.Parent) + "/" + name;

        }

        internal static string BuildUrl(InfoItems.InfoItem item)
        {
            if (item is DocuTree root)
                return "../../";

            string name = item.Name;

            if (item is InfoItems.Constructor constructorItem)
                name = "Constructors";

            return (BuildUrl(item.Parent) + "/" + name).Replace("//","/");
        }


        internal static string HyperLink(string text, string path)
            => "[" + text + "](" + path + ")";
    }
}
