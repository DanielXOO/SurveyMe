using Microsoft.EntityFrameworkCore;
using Surveys.Api.Data.Core.Abstracts;

namespace Surveys.Api.Data.Core;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly DbContext _dbContext;

    
    protected readonly DbSet<T> Data;


    public Repository(DbContext dbContext)
    {
        _dbContext = dbContext;
        Data = _dbContext.Set<T>();
    }
    
    
    public async Task CreateAsync(T data)
    {
        Data.Add(data);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IReadOnlyCollection<T>> ReadAllAsync()
    {
        var data = await Data.ToListAsync();

        return data;
    }

    public async Task UpdateAsync(T data)
    {
        Data.Update(data);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(T data)
    {
        Data.Remove(data);
        await _dbContext.SaveChangesAsync();
    }
}