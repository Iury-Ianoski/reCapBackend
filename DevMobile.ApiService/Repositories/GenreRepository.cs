using DevMobile.ApiService.Dbcontext;
using DevMobile.ApiService.Entities;
using DevMobile.ApiService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DevMobile.ApiService.Repositories
{
    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        public GenreRepository(AppDbContext context) : base(context)
        { }

        public async Task<List<Genre>> GetByIds(List<int> genreIds)
        {
            if (genreIds == null || !genreIds.Any())
                return new List<Genre>();

            return await _context.Genres
                .Where(g => genreIds.Contains(g.Id))
                .ToListAsync();
        }
    }
        
}