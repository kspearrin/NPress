using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using NPress.Core.Domains;
using NPress.Core.Repositories;

namespace NPress.Core.Identity
{
    public class UserStore :
        IUserStore<User>,
        IUserPasswordStore<User>,
        IUserEmailStore<User>,
        IUserRoleStore<User>
    {
        private readonly IUserRepository m_userRepository;

        public UserStore(IUserRepository userRepository)
        {
            m_userRepository = userRepository;
        }

        public void Dispose() { }

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken = default(CancellationToken))
        {
            await m_userRepository.CreateAsync(user); // TODO: move to a service call with proper business logic checks
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken = default(CancellationToken))
        {
            await m_userRepository.DeleteAsync(user); // TODO: move to a service call with proper business logic checks
            return IdentityResult.Success;
        }

        public async Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await m_userRepository.GetByEmailAsync(normalizedEmail);
        }

        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await m_userRepository.GetByIdAsync(userId);
        }

        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await m_userRepository.GetByUsernameAsync(normalizedUserName);
        }

        public Task<string> GetEmailAsync(User user, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult<string>(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult<bool>(true); // all emails are confirmed
        }

        public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult<string>(user.Email);
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult<string>(user.Username);
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult<string>(user.Password);
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult<string>(user.Id);
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult<string>(user.Username);
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult<bool>(!string.IsNullOrWhiteSpace(user.Password));
        }

        public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken = default(CancellationToken))
        {
            user.Email = email;
            return Task.FromResult<Object>(null);
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken = default(CancellationToken))
        {
            // do nothing
            return Task.FromResult<Object>(null);
        }

        public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken = default(CancellationToken))
        {
            user.Email = normalizedEmail;
            return Task.FromResult<Object>(null);
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken = default(CancellationToken))
        {
            user.Username = normalizedName;
            return Task.FromResult<Object>(null);
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken = default(CancellationToken))
        {
            user.Password = passwordHash;
            return Task.FromResult<Object>(null);
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken = default(CancellationToken))
        {
            user.Username = userName;
            return Task.FromResult<Object>(null);
        }

        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken = default(CancellationToken))
        {
            await m_userRepository.ReplaceAsync(user); // TODO: move to a service call with proper business logic checks
            return IdentityResult.Success;
        }

        public async Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            user.Role = ParseRole(roleName);
            await m_userRepository.ReplaceAsync(user); // TODO: move to a service call with proper business logic checks
        }

        public async Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            if(user.Role != ParseRole(roleName))
            {
                return;
            }

            user.Role = null;
            await m_userRepository.ReplaceAsync(user); // TODO: move to a service call with proper business logic checks
        }

        public Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult<IList<string>>(new List<string>() { user.Role.ToString() } );
        }

        public Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Role == ParseRole(roleName));
        }

        public Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            return Task.FromResult<IList<User>>(new List<User>());
        }

        private Enums.Role ParseRole(string roleName)
        {
            Enums.Role role;
            if(!Enum.TryParse(roleName, out role))
            {
                throw new ArgumentException(nameof(roleName));
            }

            return role;
        }
    }
}
