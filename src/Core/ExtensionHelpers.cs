using Microsoft.AspNet.Html.Abstractions;
using Microsoft.AspNet.Mvc.Rendering;

namespace NPress.Core
{
    public static class ExtensionHelpers
    {
        public static string ToHtml(this string markdown)
        {
            var md = new MarkdownDeep.Markdown();
            return md.Transform(markdown);
        }

        public static IHtmlContent ToHtmlContent(this string markdown)
        {
            return new HtmlString(markdown.ToHtml());
        }
    }
}
