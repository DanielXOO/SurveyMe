using Microsoft.AspNetCore.Mvc;
using SurveyMe.Data.Abstracts;

namespace SurveyMe.WebApplication.Controllers;

//TODO: Upload do not work
public class FilesController : Controller
{
    private readonly IFileApi _fileApi;
    
    
    public FilesController(IFileApi fileApi)
    {
        _fileApi = fileApi;
    }
    
    
    [HttpPost]
    public async Task<IActionResult> Upload([FromForm]IFormFile fileModel)
    {
        var fileInfo = await _fileApi.UploadAsync(fileModel);

        return Ok(fileInfo);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Load(Guid id)
    {
        var file = await _fileApi.LoadAsync(id);
            
        return file;
    }
}