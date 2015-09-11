using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using NPress.Core.Domains;

namespace NPress.Core.Repositories.Sql
{
    public class PageRepository : Repository<Page>, IPageRepository
    {
        public PageRepository(GlobalSettings settings)
            : this(settings.Sql.ConnectionString)
        { }

        public PageRepository(string connectionString)
            : base(connectionString)
        { }

        public async Task<Page> GetBySlugAsync(string slug)
        {
            var sql = $"SELECT * FROM [Page] WHERE [Slug] = @Slug;";

            using(var connection = new SqlConnection(ConnectionString))
            {
                var pages = await connection.QueryAsync<Page>(sql, new { Slug = slug });
                return pages.FirstOrDefault();
            }
        }
    }
}
