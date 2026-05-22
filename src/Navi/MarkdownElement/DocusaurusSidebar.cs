using Navi.InfoItems;
using System.Net.Http.Headers;

namespace Navi.Markdown
{
    /// <summary>
    /// Represents a Docusaurus sidebar metadata block as a Markdown element.
    /// </summary>
    /// <remarks>
    /// This class generates a Markdown front matter block for Docusaurus documentation,
    /// specifying the sidebar label and position for the document.
    /// </remarks>
    public class DocusaurusSidebar : IMarkdownElement
    {
        /// <summary>
        /// Gets or sets the label to display in the Docusaurus sidebar.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the position of the document in the Docusaurus sidebar.
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// Gets the Markdown front matter block for the sidebar metadata.
        /// </summary>
        public string Markdown
            => "---\n" +
               $"sidebar_label: {Label}\n" +
               $"sidebar_position: {Position}\n" +
               "---\n\n";

        /// <summary>
        /// Initializes a new instance of the <see cref="DocusaurusSidebar"/> class.
        /// </summary>
        /// <param name="label">The label to display in the sidebar.</param>
        /// <param name="position">The position of the document in the sidebar.</param>
        public DocusaurusSidebar(string label, int position)
        {
            Label = label;
            Position = position;
        }
    }
}