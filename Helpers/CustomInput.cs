using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRS.Helpers
{
    [HtmlTargetElement("custom-input", Attributes = ForAttributeName + "," + NotEditAttributeName)]
    public class CustomInput: TagHelper
    {
        private const string ForAttributeName = "asp-for";
        private const string NotEditAttributeName = "not-edit";

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        [HtmlAttributeName(ForAttributeName)]
        public ModelExpression For { get; set; }

        [HtmlAttributeName(NotEditAttributeName)]
        public bool NotEdit { get; set; }

        private IHtmlGenerator _htmlGenerator;
        public CustomInput(IHtmlGenerator htmlGenerator)
        {
            _htmlGenerator = htmlGenerator;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!NotEdit)
            {
                var inputContext = CrateTagHelperContext();
                var inputOutput = CreateTagHelperOutput("input");

                inputOutput.Attributes.Add("class", "form-control");

                var input = new InputTagHelper(_htmlGenerator)
                {
                    For = For,
                    ViewContext = ViewContext
                };

                input.Process(inputContext, inputOutput);
                output.Content.AppendHtml(inputOutput);
            }
            else
            {
                var labelContext = CrateTagHelperContext();
                var labelOutput = CreateTagHelperOutput("label");

                //labelOutput.Content.Append(LabelContent);

                if (For != null)
                {
                    labelOutput.Attributes.Add("for", For.Name);
                }

                var label = new LabelTagHelper(_htmlGenerator)
                {
                    ViewContext = ViewContext
                };

                label.Process(labelContext, labelOutput);
                output.Content.AppendHtml(labelOutput);
            }
        }

        private static TagHelperContext CrateTagHelperContext()
        {
            return new TagHelperContext(
                new TagHelperAttributeList(),
                new Dictionary<object, object>(),
                Guid.NewGuid().ToString("N"));
        }

        private static TagHelperOutput CreateTagHelperOutput(string tagName)
        {
            return new TagHelperOutput(
                tagName,
                new TagHelperAttributeList(),
                (a, b) =>
                {
                    var tagHelperContent = new DefaultTagHelperContent();
                    tagHelperContent.SetContent(string.Empty);
                    return Task.FromResult<TagHelperContent>(tagHelperContent);
                });
        }
    }
}
