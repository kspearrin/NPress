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

        public async Task<IEnumerable<Post>> PageAsync(string cursor, bool beforeCursor, int pageSize)
        {
            var sql = $@"
                SELECT * FROM [Post]
                WHERE @Cursor IS NULL OR [Id] {(beforeCursor ? "<" : ">")} @Cursor
                ORDER BY [Id] {(beforeCursor ? "DESC" : "ASC")}
                OFFSET 0 ROWS
                FETCH NEXT @PageSize ROWS ONLY;";

            using(var connection = new SqlConnection(ConnectionString))
            {
                var posts = await connection.QueryAsync<Post>(
                    sql,
                    new
                    {
                        Cursor = cursor,
                        PageSize = pageSize
                    });

                return posts;
            }
        }
    }
}
