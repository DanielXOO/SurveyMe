using System;
using System.Threading.Tasks;
using SurveyMe.Foundation.Models;

namespace SurveyMe.Foundation.Services.Abstracts
{
    public interface IFileService
    {
        Task UploadAsync(File file);

        Task<File> LoadAsync(Guid id);
    }
}