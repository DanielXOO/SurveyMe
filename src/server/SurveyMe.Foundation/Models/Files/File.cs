using System.IO;
using FileInfo = SurveyMe.DomainModels.Files.FileInfo;

namespace SurveyMe.Foundation.Models.Files;

public class File
{
    public Stream Data { get; set; }

    public FileInfo Info { get; set; }
}