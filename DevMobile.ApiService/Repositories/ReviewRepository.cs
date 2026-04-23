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
                .Include(r => r.User)
                .OrderByDescending(r => r.Id)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<List<Review>> GetByUserId(int userId)
        {
            return await _context.Reviews
                .Include(r => r.Book)
                    .ThenInclude(b => b.Genres)
                .Include(r => r.User)
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.Id)
                .ToListAsync();
        }

        public async Task<List<Review>> GetByBookId(int bookId)
        {
            return await _context.Reviews
                .Include(r => r.Book)
                    .ThenInclude(b => b.Genres)
                .Include(r => r.User)
                .Where(r => r.BookId == bookId)
                .OrderByDescending(r => r.Id)
                .ToListAsync();
        }

    }
        
}