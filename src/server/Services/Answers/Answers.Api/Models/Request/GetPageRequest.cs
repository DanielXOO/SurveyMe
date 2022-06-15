namespace Answers.Api.Models.Request;

public sealed class GetPageRequest
{
    public int PageSize { get; set; } = 5;

    public int Page { get; set; } = 1;
}