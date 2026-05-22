using Navi.InfoItems;
using System.Net.Http.Headers;

namespace Navi.Markdown
{
    /// <summary>
    /// Represents a plain text element within a Markdown document.
    /// </summary>
    /// <remarks>
    /// This class implements <see cref="IMarkdownElement"/> and provides
    /// a simple wrapper for text content that can be rendered as Markdown.
    /// </remarks>
    public class Text : IMarkdownElement
    {
        /// <summary>
        /// Gets or sets the text value of this Markdown element.
        /// </summary>
        public string TextValue { get; set; }

        /// <summary>
        /// Gets the Markdown representation of the text, followed by two newlines.
        /// </summary>
        public string Markdown => TextValue + "\n\n";

        /// <summary>
        /// Initializes a new instance of the <see cref="Text"/> class with the specified text.
        /// </summary>
        /// <param name="text">The text content to be represented as a Markdown element.</param>
        public Text(string text)
        {
            TextValue = text;

        }
    }
}