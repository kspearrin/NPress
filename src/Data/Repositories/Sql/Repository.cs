using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using NPress.Core;
using NPress.Core.Data;

namespace NPress.Data.Repositories.Sql
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
            using(var connection = new SqlConnection(ConnectionString))
            {
                var models = await connection.QueryAsync<T>($"SELECT * FROM [{TableName}] WHERE [Id] = '{id}';");
                return models.FirstOrDefault();
            }
        }

        public async Task CreateAsync(T model)
        {
            model.Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString();

            var propertyContainer = ParseProperties(model);

            var sqlFields = string.Join(", ", propertyContainer.EscapedProperties);
            var sqlParameters = string.Join(", ", propertyContainer.ParameterizedProperties);
            var sql = $"INSERT INTO [{TableName}] ([Id], {sqlFields}) VALUES(@Id, {sqlParameters});";

            using(var connection = new SqlConnection(ConnectionString))
            {
                await connection.ExecuteAsync(sql, model);
            }
        }

        public async Task ReplaceAsync(T model)
        {
            var propertyContainer = ParseProperties(model);

            var sqlPairs = string.Join(", ", propertyContainer.PairedProperties);
            var sql = $"UPDATE [{TableName}] SET {sqlPairs} WHERE [Id] = '{model.Id}';";

            using(var connection = new SqlConnection(ConnectionString))
            {
                await connection.ExecuteAsync(sql, model);
            }
        }

        public async Task UpsertAsync(T model)
        {
            if(string.IsNullOrWhiteSpace(model.Id))
            {
                await CreateAsync(model);
            }
            else
            {
                await ReplaceAsync(model);
            }
        }

        public async Task Delete(T model)
        {
            await DeleteById(model.Id);
        }

        public async Task DeleteById(string id)
        {
            using(var connection = new SqlConnection(ConnectionString))
            {
                await connection.ExecuteAsync($"DELETE FROM [{TableName}] WHERE [Id] = '{id}';");
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
