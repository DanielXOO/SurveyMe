using System;
using System.Threading.Tasks;
using SurveyMe.Surveys.Foundation.Models;

namespace SurveyMe.Surveys.Foundation.Services.Files
{
    public interface IFileService
    {
        Task UploadAsync(File file);

        Task<File> LoadAsync(Guid id);
    }
}