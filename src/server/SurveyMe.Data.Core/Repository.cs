using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SurveyMe.Repositories;

namespace SurveyMe.Data.Core
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _db;


        protected readonly DbSet<T> Data;


        protected Repository(DbContext context)
        {
            _db = context;
            Data = context.Set<T>();
        }
        
        
        public void Create(T item)
        {
            Data.Add(item);
        }
        
        public void Delete(T item)
        {
            Data.Remove(item);
        }

        public void Update(T item)
        {
            _db.Update(item);
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
}