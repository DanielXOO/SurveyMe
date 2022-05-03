using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using SurveyMe.Foundation.Services.Abstracts;
using File = SurveyMe.Foundation.Models.File;
using FileInfo = SurveyMe.DomainModels.FileInfo;

namespace SurveyMe.WebApplication.Controllers;

[ApiController]
[Route("[controller]/[action]")]
[Authorize]
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
    public async Task<IActionResult> Upload(IFormFile fileModel)
    {
        var fileStream = fileModel.OpenReadStream();

        var file = new File()
        {
            Data = fileStream,
            Info = new FileInfo()
            {
                Id = Guid.NewGuid(),
                Name = fileModel.FileName
            }
        };
            
        var getContentResult = _fileExtensionContentTypeProvider
            .TryGetContentType(fileModel.FileName, out var mime);

        if (!getContentResult)
        {
            return BadRequest(new {message = "Content type not found"});
        }
            
        file.Info.ContentType = mime;
            
        await _fileService.UploadAsync(file);
        
        return Json(file.Info);
    }
        
    [HttpGet("{fileId:guid}")]
    public async Task<IActionResult> Load(Guid fileId)
    {
        var file = await _fileService.LoadAsync(fileId);

        if (file == null)
        {
            return NotFound();
        }
            
        return File(file.Data, file.Info.ContentType, file.Info.Name);
    }
}