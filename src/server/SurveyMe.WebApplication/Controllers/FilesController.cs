using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using SurveyMe.Common.Exceptions;
using SurveyMe.Foundation.Services.Abstracts;
using SurveyMe.WebApplication.Models.Errors;
using File = SurveyMe.Foundation.Models.File;
using FileInfo = SurveyMe.DomainModels.FileInfo;

namespace SurveyMe.WebApplication.Controllers;

/// <summary>
/// Controller for interaction with files
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class FilesController : Controller
{
    private readonly IFileService _fileService;
    private readonly FileExtensionContentTypeProvider _fileExtensionContentTypeProvider;
        
    public FilesController(IFileService fileService, FileExtensionContentTypeProvider fileExtensionContentTypeProvider)
    {
        _fileService = fileService;
        _fileExtensionContentTypeProvider = fileExtensionContentTypeProvider;
    }

    
    /// <summary>
    /// Action for saving file on server
    /// </summary>
    /// <param name="fileModel">File model</param>
    /// <returns>Info about file</returns>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileInfo))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseErrorResponse))]
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
            throw new BadRequestException("No such file type");
        }
            
        file.Info.ContentType = mime;
            
        await _fileService.UploadAsync(file);
        
        return Ok(file.Info);
    }
    
    /// <summary>
    /// Action for display files or starts download of it
    /// </summary>
    /// <param name="id">file id</param>
    /// <returns>file</returns>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(File))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseErrorResponse))]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Load(Guid id)
    {
        var file = await _fileService.LoadAsync(id);
            
        return File(file.Data, file.Info.ContentType, file.Info.Name);
    }
}