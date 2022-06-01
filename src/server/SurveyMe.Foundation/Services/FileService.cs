using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SurveyMe.Data;
using SurveyMe.Foundation.Exceptions;
using SurveyMe.Foundation.Services.Abstracts;
using SurveyMe.Foundation.Models.Configurations;
using File = SurveyMe.Foundation.Models.Files.File;


namespace SurveyMe.Foundation.Services;

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
        var fullPath = $"{_configuration.Value.BasePath}/{file.Info.FileId}{fileExtension}";

        if (!Directory.Exists(_configuration.Value.BasePath))
        {
            Directory.CreateDirectory(_configuration.Value.BasePath);
        }

        await using (var streamWrite = new FileStream(fullPath, FileMode.Create))
        {
            await file.Data.CopyToAsync(streamWrite);
        }
            
        await _unitOfWork.Files.CreateAsync(file.Info);
    }
}