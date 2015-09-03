using System.Collections.Generic;
using System.Threading.Tasks;
using NPress.Core.Domains;

namespace NPress.Core.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<Role> GetByNormalizedNameAsync(string normalizedName);
    }
}
