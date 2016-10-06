using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Symmetry.Data;

namespace Symmetry.MongoDb
{
    public class MongoDbDataStore<T> : IDataStore<T>
    {
        private readonly Configuration _configuration;
        private readonly IMongoDatabase _database;

        private MongoDbDataStore(Configuration configuration)
        {
            _configuration = configuration;
            var client = new MongoClient(_configuration.ConnectionString);
            _database = client.GetDatabase(_configuration.Database, _configuration.Settings);
        }

        public static Builder Create(string connectionString, string database, MongoDatabaseSettings settings = null)
            => new Builder(connectionString, database, settings);

        public class Builder
        {

            private readonly Configuration _configuration;

            public Builder(string connectionString, string database, MongoDatabaseSettings settings = null)
            {
                _configuration = new Configuration
                {
                    ConnectionString = connectionString,
                    Database = database,
                    Settings = settings
                };
            }

            public Builder WithCollection(string name)
            {
                _configuration.Collection = name;

                return this;
            }

            public Builder WithQuery(Func<IMongoQueryable<T>, IMongoQueryable<T>> query)
            {
                _configuration.Query = query;

                return this;
            }

            public IDataStore<T> Build() => new MongoDbDataStore<T>(_configuration);
        }

        public async Task SaveAsync(IEnumerable<T> values)
        {
            if(!values.Any())
                return;

            await _database.GetCollection<T>(_configuration.Collection).InsertManyAsync(values);
        }

        private class Configuration
        {
            public MongoDatabaseSettings Settings { get; set; }
            public string Database { get; set; }
            public string ConnectionString { get; set; }
            public string Collection { get; set; }
            public Func<IMongoQueryable<T>, IMongoQueryable<T>> Query { get; set; }
        }
    }
}