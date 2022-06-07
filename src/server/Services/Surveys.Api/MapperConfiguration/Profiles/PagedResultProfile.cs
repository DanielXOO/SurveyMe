using AutoMapper;
using Surveys.Api.Common.Pagination;
using Surveys.Api.Models.Response.Pages;
using Surveys.Api.Models.Response.Surveys;
using Surveys.Api.Models.Surveys;

namespace Surveys.Api.MapperConfiguration.Profiles;

public sealed class PagedResultProfile : Profile
{
    public PagedResultProfile()
    {
        CreateMap<PagedResult<Survey>, PagedResultResponseModel<SurveyResponseModel>>();
    }
}