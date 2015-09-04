using System;
using System.Collections.Generic;
using System.Linq;

namespace NPress.Core.Repositories.Sql
{
    internal class SqlHelpers
    {
        /// <summary>
        /// Retrieves a Dictionary with name and value 
        /// for all object properties matching the given criteria.
        /// </summary>
        internal static PropertyContainer ParseProperties<T>(T obj)
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

        internal class PropertyContainer
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
