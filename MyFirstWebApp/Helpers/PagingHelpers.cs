using MyFirstWebApp.Models;
using System;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Html;
using System.Text.Encodings.Web;

namespace MyFirstWebApp.Helpers

{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public static class PagingHelpers
    {
        public static HtmlString PageLinks (this IHtmlHelper html,
            PageInfo pageInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            for (int i=1; i<=pageInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml.AppendHtml(i.ToString());
                
                if (i == pageInfo.PageNumber)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }
                tag.AddCssClass("btn btn-default");
                result.Append(GetString(tag));
            }
            return new HtmlString(result.ToString());
        }

        public static string GetString(IHtmlContent content)
        {
            using (var writer = new System.IO.StringWriter())
            {
                content.WriteTo(writer, HtmlEncoder.Default);
                return writer.ToString();
            }
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
