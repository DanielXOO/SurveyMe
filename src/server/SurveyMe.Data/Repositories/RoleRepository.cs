using Microsoft.EntityFrameworkCore;
using SurveyMe.Data.Core;
using SurveyMe.Data.Repositories.Abstracts;
using SurveyMe.DomainModels;

namespace SurveyMe.Data.Repositories
{
    public sealed class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(DbContext context)
            : base(context)
        {
        }

        public async Task<Role> GetByNameAsync(string name)
        {
            var role = await Data
                .FirstOrDefaultAsync(user => user.Name == name);

            return role;
        }

        public async Task<Role> GetByIdAsync(Guid id)
        {
            var role = await Data
                .FirstOrDefaultAsync(role => role.Id == id);

            return role;
        }
    }
}