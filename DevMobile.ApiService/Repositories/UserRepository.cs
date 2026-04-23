using DevMobile.ApiService.Dbcontext;
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
            return await _context.Users.Include(u => u.Roles).FirstOrDefaultAsync(u => u.Email == userName);
        }

        public async Task<List<User>> SearchByName(string namePart)
        {
            return await _context.Users
                .Include(u => u.Roles)
                .Where(u => u.Name.ToLower().Contains(namePart.ToLower()) || u.Email.ToLower().Contains(namePart.ToLower()))
                .ToListAsync();
        }
    }
        
}