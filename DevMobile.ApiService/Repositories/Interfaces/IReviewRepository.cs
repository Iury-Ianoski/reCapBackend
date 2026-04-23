using DevMobile.ApiService.Entities;

namespace DevMobile.ApiService.Repositories.Interfaces
{
    public interface IReviewRepository : IGenericRepository<Review>
    {
        Task<List<Review>> GetLatest(int limit);
        Task<List<Review>> GetByUserId(int userId);
        Task<List<Review>> GetByBookId(int bookId);
    }
}