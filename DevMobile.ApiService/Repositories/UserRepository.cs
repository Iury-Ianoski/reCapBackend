using DevMobile.ApiService.Entities;
using DevMobile.ApiService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DevMobile.ApiService.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        { }
    
        public async Task<User> GetByUserName(string userName)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == userName);
        }
    }
        
}