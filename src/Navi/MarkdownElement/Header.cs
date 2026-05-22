using Navi.InfoItems;
using System.Net.Http.Headers;

namespace Navi.Markdown
{
    /// <summary>
    /// Represents a Markdown header element (e.g., # Header).
    /// </summary>
    public class Header : IMarkdownElement
    {
        /// <summary>
        /// Gets or sets the text content of the header.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the header level (1-6), where 1 is the largest header.
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Gets the Markdown representation of the header.
        /// </summary>
        public string Markdown
            => new string('#', Level) + $" {Text}\n\n";

        /// <summary>
        /// Initializes a new instance of the <see cref="Header"/> class with the specified text and level.
        /// </summary>
        /// <param name="text">The header text.</param>
        /// <param name="level">The header level (1-6).</param>
        public Header(string text, int level)
        {
            Text = text;
            Level = level;
        }
    }
}