using System.Linq;
using System.Threading.Tasks;

namespace EstateAgency.DAL.Interface
{
    public interface IReadOnlyRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        Task<T> GetByIdAsync(int id);
    }
}
