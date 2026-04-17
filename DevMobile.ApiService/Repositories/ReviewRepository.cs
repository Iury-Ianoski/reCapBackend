using DevMobile.ApiService.Dbcontext;
using DevMobile.ApiService.Entities;
using DevMobile.ApiService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DevMobile.ApiService.Repositories
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        public ReviewRepository(AppDbContext context) : base(context)
        { }

        public async Task<List<Review>> GetLatest(int limit)
        {
            return await _context.Reviews
                .Include(r => r.Book)
                    .ThenInclude(b => b.Genres)
                .OrderByDescending(r => r.Id)
                .Take(limit)
                .ToListAsync();
        }

    }
        
}