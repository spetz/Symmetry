using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Symmetry.Comparers;
using Symmetry.Data;
using Symmetry.Examples.Console.Models;

namespace Symmetry.Examples.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var firstUserId = Guid.NewGuid();
            var sourceUsers = new List<User>
            {
                new User(firstUserId, "user1"),
                new User(Guid.NewGuid(), "user2"),
                new User(Guid.NewGuid(), "user3"),
            };

            var destinationUsers = new List<User>
            {
                new User(firstUserId, "user4"),
                new User(Guid.NewGuid(), "user5")
            };

            var memoryDataSource = MemoryDataSource<User>
                .Create(sourceUsers)
                .Build();

            var memoryDataDestination = MemoryDataSource<User>
                .Create(destinationUsers)
                .Build();

            var memoryDataStore = MemoryDataStore<User>
                .Create()
                .Build();

            var synchronizerConfiguration = SynchronizerConfiguration<User>
                .Create()
                .WithDataSource(() => memoryDataSource)
                .WithDataDestination(() => memoryDataDestination)
                .WithDataStore(() => memoryDataStore)
                .WithComparer(() => ObjectComparer<User>.Create((a, b) => a.Id == b.Id))
                .Build();

            var synchronizer = Synchronizer<User>
                .Create(synchronizerConfiguration)
                .Build();

            var configuration = SymmetryConfiguration
                .Create()
                .WithSynchronizer(() => synchronizer)
                .Build();

            var symmetry = SymmetryInstance.Create(configuration);
            Task.WaitAll(symmetry.RunAsync());
        }
    }
}
