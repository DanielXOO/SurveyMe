using Microsoft.AspNetCore.Mvc;
using SurveyMe.Services.Abstracts;

namespace SurveyMe.WebApplication.Controllers;

public class FilesController : Controller
{
    private readonly IFileService _fileService;
    
    
    public FilesController(IFileService fileService)
    {
        _fileService = fileService;
    }
    
    
    [HttpPost]
    public async Task<IActionResult> Upload([FromForm]IFormFile fileModel)
    {
        var fileInfo = await _fileService.UploadAsync(fileModel);

        return Ok(fileInfo);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Load(Guid id)
    {
        var file = await _fileService.LoadAsync(id);
            
        return file;
    }
}