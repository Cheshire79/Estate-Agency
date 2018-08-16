using System.Linq;
using System.Threading.Tasks;

namespace EstateAgency.DAL.Interface
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        Task<T> GetByIdAsync(int id);
        void Create(T item);
        void Update(T item);
        void Delete(T item);
    }
}
