﻿using System.Threading.Tasks;

namespace SurveyMe.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : class;

        Task SaveChangesAsync();
    }
}