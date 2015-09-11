using System.Collections.Generic;
using System.Threading.Tasks;
using NPress.Core.Domains;

namespace NPress.Core.Repositories
{
    public interface IPageRepository : IRepository<Page>
    {
        Task<Page> GetBySlugAsync(string slug);
    }
}
