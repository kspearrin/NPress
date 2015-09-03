using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using NPress.Core.Domains;

namespace NPress.Core.Repositories.Sql
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(GlobalSettings settings)
            : this(settings.Sql.ConnectionString)
        { }

        public UserRepository(string connectionString)
            : base(connectionString)
        { }

        public async Task<User> GetByEmailAsync(string email)
        {
            var sql = $"SELECT * FROM [User] WHERE [Email] = @Email;";

            using(var connection = new SqlConnection(ConnectionString))
            {
                var users = await connection.QueryAsync<User>(sql, new { Email = email });
                return users.FirstOrDefault();
            }
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            var sql = $"SELECT * FROM [User] WHERE [Username] = @Username;";

            using(var connection = new SqlConnection(ConnectionString))
            {
                var users = await connection.QueryAsync<User>(sql, new { Username = username });
                return users.FirstOrDefault();
            }
        }
    }
}
