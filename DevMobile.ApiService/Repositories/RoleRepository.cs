using DevMobile.ApiService.Entities;
using DevMobile.ApiService.Repositories.Interfaces;
using DevMobile.ApiService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DevMobile.ApiService.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(AppDbContext context) : base(context)
        { }

        public async Task<Role> GetByRoleName(string roleName)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
        }

        
    }
        
}