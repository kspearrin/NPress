using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NPress.Core.Domains;

namespace NPress.Core.Repositories.Memory
{
    public class RoleRepository : IRoleRepository
    {
        private static IEnumerable<Role> m_roles = new List<Role>
        {
            new Role { Name = "Admin" }
        };

        public Task<Role> GetByNameAsync(string name)
        {
            var role = m_roles.Where(r => r.Name == name).FirstOrDefault();
            return Task.FromResult(role);
        }

        public Task<IEnumerable<Role>> GetAllAsync()
        {
            return Task.FromResult(m_roles);
        }
    }
}
