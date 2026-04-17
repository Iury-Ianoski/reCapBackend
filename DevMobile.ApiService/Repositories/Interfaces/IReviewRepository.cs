using DevMobile.ApiService.Entities;

namespace DevMobile.ApiService.Repositories.Interfaces
{
    public interface IReviewRepository : IGenericRepository<Review>
    {
        Task<List<Review>> GetLatest(int limit);
    }
}