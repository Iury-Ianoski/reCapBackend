using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace DevMobile.ApiService.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Get(int id);
        Task<T> GetWithIncludes(int id, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAllWithIncludes(params Expression<Func<T, object>>[] includes);
        Task Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        Task SaveChangesAsync();
    }
}