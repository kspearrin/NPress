using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using NPress.Core.Domains;
using NPress.Core.Repositories;

namespace NPress.Core.Identity
{
    public class RoleStore : IRoleStore<Role>
    {
        private readonly IRoleRepository m_roleRepository;

        public RoleStore(IRoleRepository roleRepository)
        {
            m_roleRepository = roleRepository;
        }

        public void Dispose() { }

        public async Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
        {
            await m_roleRepository.CreateAsync(role); // TODO: move to a service call with proper business logic checks
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
        {
            await m_roleRepository.DeleteAsync(role); // TODO: move to a service call with proper business logic checks
            return IdentityResult.Success;
        }

        public async Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            return await m_roleRepository.GetByIdAsync(roleId);
        }

        public async Task<Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            return await m_roleRepository.GetByNormalizedNameAsync(normalizedRoleName);
        }

        public Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            return Task.FromResult<string>(role.NormalizedName);
        }

        public Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken)
        {
            return Task.FromResult<string>(role.Id);
        }

        public Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            return Task.FromResult<string>(role.Name);
        }

        public Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken cancellationToken)
        {
            role.NormalizedName = normalizedName;
            return Task.FromResult<Object>(null);
        }

        public Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken)
        {
            role.Name = roleName;
            return Task.FromResult<Object>(null);
        }

        public async Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
        {
            await m_roleRepository.ReplaceAsync(role); // TODO: move to a service call with proper business logic checks
            return IdentityResult.Success;
        }
    }
}
