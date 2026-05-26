
using Navi.Markdown;

namespace Navi.Attributes;

public class DocusaurusAttribute : Attribute
{

    private Text MarkdownBefore { get; }
    public Text MarkdownAfter { get; } 

    public string Path { get; }
    public DocusaurusAttribute(string markdownBefore, string markdownAfter, string path = "")
    {
        MarkdownBefore = new Markdown.Text(markdownBefore);
        MarkdownAfter = new Markdown.Text(markdownAfter);
        Path = path;
    }
}