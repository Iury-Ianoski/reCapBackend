using DevMobile.ApiService.Entities;

namespace DevMobile.ApiService.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetByUserName(string userName);
    }
}