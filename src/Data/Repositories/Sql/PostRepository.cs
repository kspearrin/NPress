using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Text;
using NPress.Core.Data;

namespace NPress.Data.Repositories.Sql
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(string connectionString)
            : base(connectionString)
        { }

        public async Task<IEnumerable<Post>> PageAsync(string cursor, bool sortAscending, int pageSize)
        {
            var sql = $@"
                SELECT * FROM [Post]
                WHERE [Id] {(sortAscending ? "<" : ">")} @Cursor
                ORDER BY [Id] {(sortAscending ? "ASC" : "DESC")}
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
