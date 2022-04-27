using System.IO;
using FileInfo = SurveyMe.DomainModels.FileInfo;

namespace SurveyMe.Surveys.Foundation.Models
{
    public class File
    {
        public Stream Data { get; set; }

        public FileInfo Info { get; set; }
    }
}