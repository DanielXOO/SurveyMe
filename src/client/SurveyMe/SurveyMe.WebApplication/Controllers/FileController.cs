using Microsoft.AspNetCore.Mvc;
using SurveyMe.Services.Abstracts;

namespace SurveyMe.WebApplication.Controllers;

//TODO: Upload do not work
public class FileController : Controller
{
    private readonly IFileApi _fileApi;
    
    
    public FileController(IFileApi fileApi)
    {
        _fileApi = fileApi;
    }
    
    
    [HttpPost]
    public async Task<IActionResult> Upload([FromForm]IFormFile fileModel)
    {
        var fileInfo = await _fileApi.UploadAsync(fileModel);

        if (fileInfo == null)
        {
            //TODO: Throw exception
        }

        return Ok(fileInfo);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Load(Guid id)
    {
        var file = await _fileApi.LoadAsync(id);
            
        return file;
    }
}