using AutoMapper;
using SurveyMe.DomainModels.Answers;
using SurveyMe.WebApplication.Models.Requests.Files;

namespace SurveyMe.WebApplication.MapperConfigurations.Profiles;

public class FileProfile : Profile
{
    public FileProfile()
    {
        CreateMap<FileAnswer, FileInfoResponseModel>();
    }
}