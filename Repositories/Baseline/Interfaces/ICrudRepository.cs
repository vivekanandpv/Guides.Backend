using System.Linq;
using System.Threading.Tasks;

namespace Guides.Backend.Repositories.Baseline.Interfaces
{
    public interface ICrudRepository<T>
    {
        IQueryable<T> Get();
        Task<T> Get(int id);
        Task<T> Add(T entity);
        Task Save(T entity);
    }
}