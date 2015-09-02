using System;
using System.Collections.Generic;

namespace NPress.Core
{
    public class GlobalSettings
    {
        public virtual MongoDBSettings MongoDB { get; set; } = new MongoDBSettings();
        public virtual SqlSettings Sql { get; set; } = new SqlSettings();

        public class MongoDBSettings
        {
            public string ConnectionString { get; set; }
            public string DatabaseName { get; set; }
        }

        public class SqlSettings
        {
            public string ConnectionString { get; set; }
        }
    }
}