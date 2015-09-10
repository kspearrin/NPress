using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using NPress.Core.Domains;

namespace NPress.Core.Repositories.Sql
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(GlobalSettings settings)
            : this(settings.Sql.ConnectionString)
        { }

        public PostRepository(string connectionString)
            : base(connectionString)
        { }

        public async Task<Post> GetBySlugAsync(string slug)
        {
            var sql = $"SELECT * FROM [Post] WHERE [Slug] = @Slug;";

            using(var connection = new SqlConnection(ConnectionString))
            {
                var posts = await connection.QueryAsync<Post>(sql, new { Slug = slug });
                return posts.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<Post>> PageAsync(string cursor, int page, int pageSize, bool ascending)
        {
            var offset = page > 0 ? (page - 1) * pageSize : 0;

            var sql = $@"
                SELECT * FROM [Post]
                WHERE @Cursor IS NULL OR [Id] {(ascending ? ">=" : "<=")} @Cursor
                ORDER BY [Id] {(ascending ? "ASC" : "DESC")}
                OFFSET @Offset ROWS
                FETCH NEXT @PageSize ROWS ONLY;";

            using(var connection = new SqlConnection(ConnectionString))
            {
                var posts = await connection.QueryAsync<Post>(
                    sql,
                    new
                    {
                        Cursor = cursor,
                        Offset = offset,
                        PageSize = pageSize
                    });

                return posts;
            }
        }
    }
}
