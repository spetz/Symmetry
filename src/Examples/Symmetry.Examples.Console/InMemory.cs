using System;
using System.Collections.Generic;
using Symmetry.Comparers;
using Symmetry.Data;
using Symmetry.Examples.Console.Models;

namespace Symmetry.Examples.Console
{
    public static class InMemory
    {
        public static ISynchronizer GetSynchronizer()
        {
            var sourceUsers = new List<User>
            {
                new User("user1"),
                new User("user2"),
                new User("user3"),
            };

            var destinationUsers = new List<User>
            {
                new User("user1"),
                new User("user5")
            };

            var dataSource = MemoryDataSource<User>
                .Create(sourceUsers)
                .Build();

            var dataDestination = MemoryDataSource<User>
                .Create(destinationUsers)
                .Build();

            var dataStore = MemoryDataStore<User>
                .Create()
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