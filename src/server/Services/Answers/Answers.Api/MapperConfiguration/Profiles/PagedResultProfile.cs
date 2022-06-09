using Answers.Api.Models.Response.Answers;
using Answers.Api.Models.Response.Pages;
using Answers.Common.Pagination;
using Answers.Models.Answers;
using AutoMapper;

namespace Answers.Api.MapperConfiguration.Profiles;

public sealed class PagedResultProfile : Profile
{
    public PagedResultProfile()
    {
        CreateMap<PagedResult<SurveyAnswer>, PagedResultResponseModel<SurveyAnswerResponseModel>>();
    }
}