using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;

namespace TagHelperDemo.Shared.PaginationTagHelper;

public class PagingTagHelper : TagHelper
{
    public IPagination? Pagination { get; set; }
    public string FirstPageText { get; set; } = "First";
    public string LastPageText { get; set; } = "Last";
    public string Controller { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty;

    private readonly LinkGenerator _linkGenerator;
    private readonly IHttpContextAccessor _contextAccessor;

    public PagingTagHelper(LinkGenerator linkGenerator, IHttpContextAccessor contextAccessor)
    {
        _linkGenerator = linkGenerator;
        _contextAccessor = contextAccessor;
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (Pagination is null || string.IsNullOrEmpty(Controller) || string.IsNullOrEmpty(Action)) return;
        
        BuildMainTag(output);
        BuildFirstLastPageButton(FirstPageText, 1, Pagination.CurrentPage == 1, output);
        BuildFirstLastPageButton(@"«", Pagination.CurrentPage - 1, Pagination.CurrentPage == 1, output);
        BuildPageButtons(Pagination.CurrentPage, Pagination.TotalPages, output);
        BuildFirstLastPageButton(@"»", Pagination.CurrentPage + 1, Pagination.CurrentPage == Pagination.TotalPages, output);
        BuildFirstLastPageButton(LastPageText, Pagination.TotalPages, Pagination.CurrentPage == Pagination.TotalPages, output);
    }

    private static void BuildMainTag(TagHelperOutput output)
    {
        output.TagName = "ul";
        output.Attributes.Add("class", "pagination");
        output.Attributes.Add("aria-label", "Stronicowanie");
    }

    private void BuildFirstLastPageButton(string pageText, int pageNumber, bool disabled, TagHelperOutput output)
    {
        var li = new TagBuilder("li");
        li.Attributes.Add("class", disabled ? "page-item disabled" : "page-item");
        li.Attributes.Add("aria-label", pageText);
        li.TagRenderMode = TagRenderMode.StartTag;
        output.Content.AppendHtml(li);

        var path = _linkGenerator.GetPathByAction(_contextAccessor.HttpContext, Action, Controller,
            new { pageNumber, pageSize = Pagination!.PageSize });
        var link = $@"<a class=""page-link"" href=""{path}"">{pageText}</a>";
        output.Content.AppendHtml(link);
        output.Content.AppendHtml("</li>");
    }

    private void BuildPageButtons(int pageNumber, int totalPages, TagHelperOutput output)
    {
        if (totalPages < 8)
        {
            for (var i = 1; i <= totalPages; i++)
            {
                BuildPageButton(pageNumber, i, output);
            }
        }
        else
        {
            BuildSpacerButton(Pagination!.HasPrevious, output);
            for (var i = Math.Max(1, pageNumber - 2); i <= Math.Min(totalPages, pageNumber + 2); i++)
            {
                BuildPageButton(pageNumber, i, output);
            }

            BuildSpacerButton(Pagination.HasNext, output);
        }
    }

    private void BuildPageButton(int pageNumber, int buttonNumber, TagHelperOutput output)
    {
        var li = new TagBuilder("li");
        li.Attributes.Add("class", buttonNumber == pageNumber ? "page-item active" : "page-item");
        li.Attributes.Add("aria-label", buttonNumber.ToString());
        li.TagRenderMode = TagRenderMode.StartTag;
        output.Content.AppendHtml(li);

        var path = _linkGenerator.GetPathByAction(_contextAccessor.HttpContext, Action, Controller,
            new { pageNumber = buttonNumber, pageSize = Pagination!.PageSize });
        var link = $@"<a class=""page-link"" href=""{path}"">{buttonNumber}</a>";
        output.Content.AppendHtml(link);
        output.Content.AppendHtml("</li>");
    }

    private static void BuildSpacerButton(bool isVisible, TagHelperOutput output)
    {
        const string span = @"<span class=""page-link"">...</span>";

        if (!isVisible) return;

        var li = new TagBuilder("li");
        li.Attributes.Add("class", "page-item disabled");
        li.Attributes.Add("aria-hidden", bool.TrueString);
        li.TagRenderMode = TagRenderMode.StartTag;
        output.Content.AppendHtml(li);
        output.Content.AppendHtml(span);
        output.Content.AppendHtml("</li>");
    }
}