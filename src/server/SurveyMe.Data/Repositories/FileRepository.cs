using Microsoft.EntityFrameworkCore;
using SurveyMe.Data.Core;
using SurveyMe.Data.Repositories.Abstracts;
using SurveyMe.DomainModels;
using FileInfo = SurveyMe.DomainModels.FileInfo;

namespace SurveyMe.Data.Repositories;

public sealed class FileRepository : Repository<FileInfo>, IFileRepository
{
    public FileRepository(DbContext context) : base(context)
    {
    }

    public async Task<FileInfo> GetByIdAsync(Guid id)
    {
        var file = await Data.FirstOrDefaultAsync(file => file.Id == id);

        return file;
    }
}