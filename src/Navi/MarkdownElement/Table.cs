using Navi.InfoItems;
using System.Net.Http.Headers;
using System.Reflection.PortableExecutable;

namespace Navi.Markdown
{
    /// <summary>
    /// Represents a Markdown table element with headers and values.
    /// </summary>
    /// <remarks>
    /// The <see cref="Table"/> class provides a way to construct a Markdown-formatted table
    /// using specified headers and a two-dimensional array of values. The resulting Markdown
    /// string can be accessed via the <see cref="Markdown"/> property.
    /// </remarks>
    public class Table : IMarkdownElement
    {
        /// <summary>
        /// Gets or sets the headers for the Markdown table.
        /// </summary>
        public string[] Headers { get; set; }

        /// <summary>
        /// Gets or sets the values for the Markdown table.
        /// </summary>
        /// <remarks>
        /// The values are organized as a two-dimensional array, where each row corresponds to a table row.
        /// </remarks>
        public string[,] Values { get; set; }

        /// <summary>
        /// Gets the Markdown representation of the table.
        /// </summary>
        /// <remarks>
        /// Returns a Markdown-formatted string if the table can be created; otherwise, returns an empty string.
        /// </remarks>
        public string Markdown
        {
            get
            {
                if (Tables.TryCreateTable(Headers, Values, out string result))
                    return result;
                return string.Empty;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Table"/> class with the specified headers and values.
        /// </summary>
        /// <param name="headers">An array of strings representing the table headers.</param>
        /// <param name="values">A two-dimensional array of strings representing the table values.</param>
        public Table(string[] headers, string[,] values)
        {
            Headers = headers;
            Values = values;
        }
    }
}