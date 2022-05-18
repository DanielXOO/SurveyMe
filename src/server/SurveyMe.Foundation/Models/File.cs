using System.IO;
using FileInfo = SurveyMe.DomainModels.Files.FileInfo;

namespace SurveyMe.Foundation.Models;

public class File
{
    public Stream Data { get; set; }

    public FileInfo Info { get; set; }
}