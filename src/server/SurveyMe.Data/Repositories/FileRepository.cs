using Microsoft.EntityFrameworkCore;
using SurveyMe.Data.Core;
using SurveyMe.Data.Repositories.Abstracts;
using SurveyMe.DomainModels.Answers;

namespace SurveyMe.Data.Repositories;

public sealed class FileRepository : Repository<FileAnswer>, IFileRepository
{
    public FileRepository(DbContext context) : base(context)
    {
    }

    
    public async Task<FileAnswer> GetByIdAsync(Guid id)
    {
        var file = await Data.FirstOrDefaultAsync(file => file.Id == id);

        return file;
    }
}