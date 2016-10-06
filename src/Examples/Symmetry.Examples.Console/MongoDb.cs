using Symmetry.Comparers;
using Symmetry.Examples.Console.Models;
using Symmetry.MongoDb;
using MongoDB.Driver.Linq;

namespace Symmetry.Examples.Console
{
    public static class MongoDb
    {
        public static ISynchronizer GetSynchronizer()
        {
            var connectionString = "mongodb://localhost:27017";
            var collection = "Users";
            var source = "Symmetry-Source";
            var destination = "Symmetry-Destination";

            var dataSource = MongoDbDataSource<User>
                .Create(connectionString, source)
                .WithQuery(q => q.Where(x => x.Name != string.Empty))
                .WithCollection(collection)
                .Build();

            var dataDestination = MongoDbDataSource<User>
                .Create(connectionString, destination)
                .WithCollection(collection)
                .Build();

            var dataStore = MongoDbDataStore<User>
                .Create(connectionString, destination)
                .WithCollection(collection)
                .Build();

            var synchronizerConfiguration = SynchronizerConfiguration<User>
                .Create()
                .WithDataSource(() => dataSource)
                .WithDataDestination(() => dataDestination)
                .WithDataStore(() => dataStore)
                .WithComparer(() => ObjectComparer<User>.Create((a, b) => a.Name == b.Name))
                .Build();

            return Synchronizer<User>
                .Create(synchronizerConfiguration)
                .Build();
        }
    }
}