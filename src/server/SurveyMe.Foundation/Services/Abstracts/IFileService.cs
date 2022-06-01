using System;
using System.Threading.Tasks;
using SurveyMe.Foundation.Models.Files;

namespace SurveyMe.Foundation.Services.Abstracts;

public interface IFileService
{
    Task UploadAsync(File file);
}