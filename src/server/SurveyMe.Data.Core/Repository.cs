using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SurveyMe.Data.Contracts;

namespace SurveyMe.Data.Core;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly DbContext _db;


    protected readonly DbSet<T> Data;


    protected Repository(DbContext context)
    {
        _db = context;
        Data = context.Set<T>();
    }


    public async Task CreateAsync(T item)
    {
        Data.Add(item);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(T item)
    {
        Data.Remove(item);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(T item)
    {
        _db.Update(item);
        await _db.SaveChangesAsync();
    }

    public async Task<T> FindByIdAsync(params object[] keyValues)
    {
        var result = await Data.FindAsync(keyValues);

        return result;
    }

    public async Task<IReadOnlyCollection<T>> GetAllAsync()
    {
        var result = await Data.ToListAsync();

        return result;
    }
}