using AutoMapper;
using SurveyMe.WebApplication.Models.Requests.Files;
using SurveyMe.WebApplication.Models.Responses.Files;
using FileInfo = SurveyMe.DomainModels.Files.FileInfo;

namespace SurveyMe.WebApplication.MapperConfigurations.Profiles;

public sealed class FileProfile : Profile
{
    public FileProfile()
    {
        CreateMap<FileInfo, FileInfoResponseModel>();
        
        CreateMap<FileInfoRequestModel, FileInfo>();
    }
}