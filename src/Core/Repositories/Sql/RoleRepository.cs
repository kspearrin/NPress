using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using NPress.Core.Domains;

namespace NPress.Core.Repositories.Sql
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(GlobalSettings settings)
            : this(settings.Sql.ConnectionString)
        { }

        public RoleRepository(string connectionString)
            : base(connectionString)
        { }

        public async Task<Role> GetByNormalizedNameAsync(string normalizedName)
        {
            var sql = $"SELECT * FROM [Role] WHERE [NormalizedName] = @NormalizedName;";

            using(var connection = new SqlConnection(ConnectionString))
            {
                var roles = await connection.QueryAsync<Role>(sql, new { NormalizedName = normalizedName });
                return roles.FirstOrDefault();
            }
        }
    }
}
