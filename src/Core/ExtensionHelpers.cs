using MarkdownDeep;
using Microsoft.AspNet.Html.Abstractions;
using Microsoft.AspNet.Mvc.Rendering;

namespace NPress.Core
{
    public static class ExtensionHelpers
    {
        private static Markdown m_markdown;

        static ExtensionHelpers()
        {
            m_markdown = new Markdown();
            m_markdown.NewWindowForExternalLinks = true;
        }

        public static string ToHtml(this string markdownContent)
        {
            return m_markdown.Transform(markdownContent);
        }

        public static IHtmlContent ToHtmlContent(this string markdownContent)
        {
            return new HtmlString(markdownContent.ToHtml());
        }
    }
}
