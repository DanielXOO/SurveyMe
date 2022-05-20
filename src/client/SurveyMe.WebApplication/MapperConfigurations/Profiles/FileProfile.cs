using AutoMapper;
using SurveyMe.DomainModels.Request.Files;
using SurveyMe.WebApplication.Models.ViewModels.Files;

namespace SurveyMe.WebApplication.MapperConfigurations.Profiles;

public sealed class FileProfile : Profile
{
    public FileProfile()
    {
        CreateMap<FileInfoViewModel, FileInfoRequestModel>();
    }
}