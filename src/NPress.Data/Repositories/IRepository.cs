using System.Threading.Tasks;
using NPress.Data.Models;

namespace NPress.Data.Repositories
{
    public interface IRepository<T> where T : IDataModel
    {
        Task<T> GetByIdAsync(string id);
        Task CreateAsync(T model);
        Task ReplaceAsync(T model);
        Task UpsertAsync(T model);
        Task DeleteById(string id);
        Task Delete(T model);
    }
}
