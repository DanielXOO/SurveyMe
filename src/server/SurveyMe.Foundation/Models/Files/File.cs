using System.IO;
using SurveyMe.DomainModels.Answers;

namespace SurveyMe.Foundation.Models.Files;

public class File
{
    public Stream Data { get; set; }

    public FileAnswer Info { get; set; }
}