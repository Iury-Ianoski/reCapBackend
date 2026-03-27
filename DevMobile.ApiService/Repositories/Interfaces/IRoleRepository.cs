using DevMobile.ApiService.Entities;

namespace DevMobile.ApiService.Repositories.Interfaces
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task<Role> GetByRoleName(string roleName);
    }
}