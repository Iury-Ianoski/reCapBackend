using DevMobile.ApiService.Dbcontext;
using DevMobile.ApiService.Entities;
using DevMobile.ApiService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DevMobile.ApiService.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(AppDbContext context) : base(context)
        { }

        public async Task<Book> GetByBookTitle(string title)
        {
            return await _context.Books.OrderByDescending(b => b.Id).FirstOrDefaultAsync(b => b.Title == title);
        }
    }
        
}