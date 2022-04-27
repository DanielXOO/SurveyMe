using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SurveyMe.Data;
using SurveyMe.Surveys.Foundation.Models;
using File = SurveyMe.Surveys.Foundation.Models.File;

namespace SurveyMe.Surveys.Foundation.Services.Files
{
    public class FileService : IFileService
    {
        private readonly IOptions<FileServiceConfiguration> _configuration;
        private readonly ISurveyMeUnitOfWork _unitOfWork;
        
        
        public FileService(IOptions<FileServiceConfiguration> configuration, ISurveyMeUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }
        
        
        public async Task UploadAsync(File file)
        {
            var fileExtension = Path.GetExtension(file.Info.Name); 
            var fullPath = $"{_configuration.Value.BasePath}/{file.Info.Id}{fileExtension}";

            if (!Directory.Exists(_configuration.Value.BasePath))
            {
                Directory.CreateDirectory(_configuration.Value.BasePath);
            }
            
            await using (var streamWrite = new FileStream(fullPath, FileMode.Create))
            {
                await file.Data.CopyToAsync(streamWrite);
            }
        }

        public async Task<File> LoadAsync(Guid id)
        {
            var file = await _unitOfWork.Files.GetByIdAsync(id);
            var fileExtension = Path.GetExtension(file.Name); 
            var fullPath = $"{_configuration.Value.BasePath}/{file.Id}{fileExtension}";
            var streamWrite = new FileStream(fullPath, FileMode.Open);
            
            var fileModel = new File()
            {
                Data = streamWrite,
                Info = file
            };
            
            return fileModel;
        }
    }
}