using System.Threading.Tasks;

namespace NPress.Core.Repositories
{
    public interface IRepository<T> where T : IDataObject
    {
        Task<T> GetByIdAsync(string id);
        Task CreateAsync(T model);
        Task ReplaceAsync(T model);
        Task UpsertAsync(T model);
        Task DeleteById(string id);
        Task Delete(T model);
    }
}
