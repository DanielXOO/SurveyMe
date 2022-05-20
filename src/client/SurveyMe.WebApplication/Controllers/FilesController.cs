using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Refit;
using SurveyMe.Common.Exceptions;
using SurveyMe.Services.Abstracts;
using File = SurveyMe.DomainModels.Common.File;
using FileInfo = SurveyMe.DomainModels.Common.FileInfo;

namespace SurveyMe.WebApplication.Controllers;

public class FilesController : Controller
{
    private readonly IFileService _fileService;
    private readonly FileExtensionContentTypeProvider _fileExtensionContentTypeProvider;
    
    
    public FilesController(IFileService fileService, FileExtensionContentTypeProvider fileExtensionContentTypeProvider)
    {
        _fileService = fileService;
        _fileExtensionContentTypeProvider = fileExtensionContentTypeProvider;
    }
    
    
    [HttpPost]
    public async Task<IActionResult> Upload([FromForm]IFormFile fileModel)
    {
        if (fileModel == null)
        {
            throw new BadRequestException("File upload error");
        }
        
        var fileStream = fileModel.OpenReadStream();

        var getContentResult = _fileExtensionContentTypeProvider
            .TryGetContentType(fileModel.FileName, out var mime);

        if (!getContentResult)
        {
            throw new BadRequestException("No such file type");
        }

        var file = new StreamPart(fileStream, fileModel.FileName, mime, fileModel.Name);
        
        var fileInfo = await _fileService.UploadAsync(file);
        
        return Ok(fileInfo);
    }
    
    /*[HttpGet("{id:guid}")]
    public async Task<IActionResult> Load(Guid id)
    {
        var file = await _fileService.LoadAsync(id);

        if (file == null)
        {
            throw new NotFoundException("No such file");
        }
        
        return File(file.Data, file.Info.ContentType, file.Info.Name);
    }*/
}