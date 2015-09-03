using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace NPress.Core.Repositories.Sql
{
    public class Repository<T> : IRepository<T> where T : IDataObject
    {
        public Repository(GlobalSettings settings, string tableName = null)
            : this(settings.Sql.ConnectionString, tableName)
        { }

        public Repository(string connectionString = null, string tableName = null)
        {
            ConnectionString = connectionString;

            if(string.IsNullOrWhiteSpace(tableName))
            {
                TableName = typeof(T).Name;
            }
            else
            {
                TableName = tableName;
            }
        }

        protected string ConnectionString { get; private set; }
        protected string TableName { get; private set; }

        public async Task<T> GetByIdAsync(string id)
        {
            var sql = $"SELECT * FROM [{TableName}] WHERE [Id] = @Id;";

            using(var connection = new SqlConnection(ConnectionString))
            {
                var objs = await connection.QueryAsync<T>(sql, new { Id = id });
                return objs.FirstOrDefault();
            }
        }

        public async Task CreateAsync(T obj)
        {
            obj.Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString();

            var propertyContainer = ParseProperties(obj);
            var sqlFields = string.Join(", ", propertyContainer.EscapedProperties);
            var sqlParameters = string.Join(", ", propertyContainer.ParameterizedProperties);

            var sql = $"INSERT INTO [{TableName}] ([Id], {sqlFields}) VALUES(@Id, {sqlParameters});";

            using(var connection = new SqlConnection(ConnectionString))
            {
                await connection.ExecuteAsync(sql, obj);
            }
        }

        public async Task ReplaceAsync(T obj)
        {
            var propertyContainer = ParseProperties(obj);
            var sqlPairs = string.Join(", ", propertyContainer.PairedProperties);

            var sql = $"UPDATE [{TableName}] SET {sqlPairs} WHERE [Id] = @Id;";

            using(var connection = new SqlConnection(ConnectionString))
            {
                await connection.ExecuteAsync(sql, obj);
            }
        }

        public async Task UpsertAsync(T obj)
        {
            if(string.IsNullOrWhiteSpace(obj.Id))
            {
                await CreateAsync(obj);
            }
            else
            {
                await ReplaceAsync(obj);
            }
        }

        public async Task DeleteAsync(T obj)
        {
            await DeleteByIdAsync(obj.Id);
        }

        public async Task DeleteByIdAsync(string id)
        {
            var sql = $"DELETE FROM [{TableName}] WHERE [Id] = @Id;";

            using(var connection = new SqlConnection(ConnectionString))
            {
                await connection.ExecuteAsync(sql, new { Id = id });
            }
        }

        /// <summary>
        /// Retrieves a Dictionary with name and value 
        /// for all object properties matching the given criteria.
        /// </summary>
        private PropertyContainer ParseProperties(T obj)
        {
            var propertyContainer = new PropertyContainer();
            var properties = typeof(T).GetProperties();

            foreach(var property in properties)
            {
                // Skip reference types (but still include string!)
                if(property.PropertyType.IsClass && property.PropertyType != typeof(string))
                {
                    continue;
                }

                // Skip methods without a public setter
                if(property.GetSetMethod() == null)
                {
                    continue;
                }

                var name = property.Name;

                // Skip Id
                if(name == "Id")
                {
                    continue;
                }

                propertyContainer.Properties.Add(name);
            }

            return propertyContainer;
        }

        private class PropertyContainer
        {
            internal ICollection<string> Properties { get; set; } = new List<string>();

            internal IEnumerable<string> EscapedProperties
            {
                get { return Properties.Select(k => $"[{k}]"); }
            }

            internal IEnumerable<string> ParameterizedProperties
            {
                get { return Properties.Select(k => $"@{k}"); }
            }

            internal IEnumerable<string> PairedProperties
            {
                get { return Properties.Select(k => $"[{k}]=@{k}"); }
            }
        }
    }
}
