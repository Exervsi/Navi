using Navi.InfoItems;
using System.Net.Http.Headers;
using static System.Net.Mime.MediaTypeNames;

namespace Navi.Markdown
{
    /// <summary>
    /// Represents a Markdown page composed of multiple Markdown elements.
    /// </summary>
    /// <remarks>
    /// The <see cref="Page"/> class implements <see cref="IMarkdownElement"/> and provides
    /// functionality to aggregate and render a collection of Markdown elements as a single document.
    /// </remarks>
    public class Page : IMarkdownElement
    {
        /// <summary>
        /// Gets or sets the list of Markdown elements that make up the page.
        /// </summary>
        public List<IMarkdownElement> Elements { get; set;  }

        /// <summary>
        /// Gets the combined Markdown representation of all elements in the page.
        /// </summary>
        public string Markdown => string.Join("", Elements.Select(e => e.Markdown));

        /// <summary>
        /// Writes the Markdown content of the page to the specified file path.
        /// Creates the directory if it does not exist.
        /// </summary>
        /// <param name="path">The file path where the Markdown content will be saved.</param>
        /// <returns>
        /// <c>true</c> if the file was written successfully; otherwise, <c>false</c>.
        /// </returns>
        public bool Print(string path)
        {
            try
            {
                string directory = Path.GetDirectoryName(path);
                if (!System.IO.Directory.Exists(directory))
                    System.IO.Directory.CreateDirectory(directory);

                File.WriteAllText(path, Markdown);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Page"/> class with an empty list of elements.
        /// </summary>
        public Page()
        {
            Elements = new List<IMarkdownElement>();
        }
    }
}