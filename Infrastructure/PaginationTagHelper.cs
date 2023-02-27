using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mission9.Models.ViewModels;

namespace Mission9.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-blah")]
    public class PaginationTagHelper : TagHelper
    {
        // this is where we'll dynamically make page links 
        private IUrlHelperFactory uhf;

        public PaginationTagHelper(IUrlHelperFactory temp)
        {
            uhf = temp;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext vc { get; set; }
        // page Blah and page Action refers to the stuff in the step above 
        public PageInfo PageBlah { get; set; }
        public string PageAction { get; set; }
        public override void Process(TagHelperContext thc, TagHelperOutput tho)
        {
            IUrlHelper uh = uhf.GetUrlHelper(vc);

            // build the div
            TagBuilder final = new TagBuilder("div");

            // for each div:
            for (int i = 1; i < (PageBlah.TotalPages + 1); i++)
            {
                // make an a tag 
                TagBuilder tb = new TagBuilder("a");
                // give that a tag an href from the action and page number we're on
                tb.Attributes["href"] = uh.Action(PageAction, new { pageNum = i });
                // append that as string for the tag's inner HTML
                tb.InnerHtml.Append(i.ToString());

                // append the tagbuilder variable to the div 
                final.InnerHtml.AppendHtml(tb);
            }
            tho.Content.AppendHtml(final.InnerHtml);
        }
    }
}