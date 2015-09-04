using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using NPress.Core.Domains;

namespace NPress.Core.Repositories.Sql
{
    public class BlogRepository : IBlogRepository
    {
        public BlogRepository(GlobalSettings settings)
            : this(settings.Sql.ConnectionString)
        { }

        public BlogRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        protected string ConnectionString { get; private set; }

        public async Task<Blog> GetAsync()
        {
            var sql = $"SELECT * FROM [Blog];";

            using(var connection = new SqlConnection(ConnectionString))
            {
                var objs = await connection.QueryAsync<Blog>(sql);
                return objs.FirstOrDefault();
            }
        }

        public async Task CreateAsync(Blog blog)
        {
            var propertyContainer = SqlHelpers.ParseProperties(blog);
            var sqlFields = string.Join(", ", propertyContainer.EscapedProperties);
            var sqlParameters = string.Join(", ", propertyContainer.ParameterizedProperties);

            var sql = $"INSERT INTO [Blog] ({sqlFields}) VALUES({sqlParameters});";

            using(var connection = new SqlConnection(ConnectionString))
            {
                await connection.ExecuteAsync(sql, blog);
            }
        }

        public async Task ReplaceAsync(Blog blog)
        {
            var propertyContainer = SqlHelpers.ParseProperties(blog);
            var sqlPairs = string.Join(", ", propertyContainer.PairedProperties);

            var sql = $"UPDATE [Blog] SET {sqlPairs};";

            using(var connection = new SqlConnection(ConnectionString))
            {
                await connection.ExecuteAsync(sql, blog);
            }
        }
    }
}
