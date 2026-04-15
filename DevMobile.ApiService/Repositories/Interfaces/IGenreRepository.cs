using DevMobile.ApiService.Entities;

namespace DevMobile.ApiService.Repositories.Interfaces
{
    public interface IGenreRepository : IGenericRepository<Genre>
    {
        Task<List<Genre>> GetByIds(List<int> genreIds);
    }
}