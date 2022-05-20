using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using SurveyMe.Common.Exceptions;
using SurveyMe.DomainModels.Answers;
using SurveyMe.Foundation.Services.Abstracts;
using SurveyMe.WebApplication.Models.Errors;
using SurveyMe.WebApplication.Models.Requests.Files;
using File = SurveyMe.Foundation.Models.Files.File;


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
    private readonly IMapper _mapper;
    
    
    public FilesController(IFileService fileService, 
        FileExtensionContentTypeProvider fileExtensionContentTypeProvider, IMapper mapper)
    {
        _fileService = fileService;
        _fileExtensionContentTypeProvider = fileExtensionContentTypeProvider;
        _mapper = mapper;
    }


    /// <summary>
    /// Action for saving file on server
    /// </summary>
    /// <returns>Info about file</returns>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileAnswer))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseErrorResponse))]
    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file)
    {

        var getContentResult = _fileExtensionContentTypeProvider
            .TryGetContentType(file.FileName, out var mime);

        if (!getContentResult)
        {
            throw new BadRequestException("No such file type");
        }
        
        var fileAnswer = new File()
        {
            Data = file.OpenReadStream(),
            Info = new FileAnswer()
            {
                Id = Guid.NewGuid(),
                Name = file.FileName,
                ContentType = mime
            }
        };
        
        await _fileService.UploadAsync(fileAnswer);

        var fileInfo = _mapper.Map<FileInfoResponseModel>(fileAnswer.Info);
        
        return Ok(fileInfo);
    }
    
    /// <summary>
    /// Action for display files or starts download of it
    /// </summary>
    /// <param name="id">file id</param>
    /// <returns>file</returns>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(File))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseErrorResponse))]
    [HttpGet("{id:guid}")]
    public async Task<File> Load(Guid id)
    {
        var file = await _fileService.LoadAsync(id);
            
        return file;
    }
}