﻿using AutoMapper;
using SurveyMe.DomainModels;
using SurveyMe.Foundation.Models;
using SurveyMe.WebApplication.Models.RequestModels.SurveyModels;
using SurveyMe.WebApplication.Models.ResponseModels;

namespace SurveyMe.WebApplication.MapperConfigurations.Profiles;

public sealed class SurveyProfile : Profile
{
    public SurveyProfile()
    {
        CreateMap<SurveyResponseModel, Survey>()
            .ReverseMap();
        
        CreateMap<SurveyRequestModel, Survey>()
            .ReverseMap();
        
        CreateMap<SurveyAnswersStatistic, SurveyAnswersStatisticResponseModel>();
    }
}