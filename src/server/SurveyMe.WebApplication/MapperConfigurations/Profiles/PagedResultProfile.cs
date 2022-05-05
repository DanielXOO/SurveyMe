﻿using AutoMapper;
using SurveyMe.Common.Pagination;
using SurveyMe.Data.Models;
using SurveyMe.DomainModels;
using SurveyMe.WebApplication.Models.ResponseModels;

namespace SurveyMe.WebApplication.MapperConfigurations.Profiles;

public sealed class PagedResultProfile : Profile
{
    public PagedResultProfile()
    {
        CreateMap<PagedResult<Survey>, PagedResultResponseModel<SurveyWithLinksResponseModel>>();
        
        CreateMap<PagedResult<UserWithSurveysCount>, PagedResultResponseModel<UserWithSurveysCountResponseModel>>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
            .ForMember(dest => dest.CurrentPage, opt => opt.MapFrom(src => src.CurrentPage))
            .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.PageSize));
    }
}