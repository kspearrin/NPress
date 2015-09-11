using System.Collections.Generic;
using System.Threading.Tasks;
using NPress.Core.Domains;

namespace NPress.Core.Services
{
    public interface IPageService
    {
        Task<Page> GetPageByIdAsync(string id);
        Task<Page> GetPageBySlugAsync(string slug);
        Task CreatePageAsync(Page page);
        Task UpdatePageAsync(Page page);
    }
}
