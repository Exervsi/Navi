using Navi.Core.Getters;
using Navi.Core.Getters.MarkdownGetters;
using Navi.Core.Getters.TreeGetters;
using Navi.Core.SystemExtensions.String;
using Navi.InfoItems;
using Navi.Markdown;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using static Microsoft.CodeAnalysis.CSharp.SyntaxTokenParser;

namespace Navi.InfoItems
{

    public class Constructor : InfoItem
    {
        private ConstructorMarkdownGetter _markdownGetter => new ConstructorMarkdownGetter(this);
        private ConstructorXmlGetter _xmlGetter => new ConstructorXmlGetter(this);
        private ConstructorTreeGetter _treeGetter => new ConstructorTreeGetter(this);

        public string Name { get; }

        public InfoItem this[string name] => Children.FirstOrDefault(x => x.Name == name);

        public string Path => Wakka.BuildUrl(this);

        public string Key => _xmlGetter.Key;
        public string Value => _xmlGetter.Value;

        public ConstructorInfo Data { get; }


        public InfoItem Parent { get; }

        public DocuTree Root => _treeGetter.Root;

        public InfoItem[] Children => _treeGetter.Children;

        public TypeParameter[] TypeParameters => _treeGetter.TypeParameters;
        public Parameter[] Parameters => _treeGetter.Parameters;

        public Attribute[] Attributes { get; }

        public Constructor(ConstructorInfo constructor, Type parent)
        {
            Name = constructor.Name;
            Data = constructor;
            Parent = parent;

            Attributes = new Attribute[0];
        }

        public IMarkdownElement GetMarkdown() => _markdownGetter.Markdown();

    }
}
