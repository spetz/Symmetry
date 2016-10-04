####**Data synchronization framework**

**What is Symmetry?**
----------------

**Symmetry** is a simple, **cross-platform** library, built to **solve the problem of synchronizing the data** between different sources such as databases, microservices etc. 

**Is there any documentation?**
----------------

There **will be**, however it's not there yet - the repository has been recently created.

**Installation**
----------------

**It will be** available as a **[NuGet package](https://www.nuget.org/packages/Symmetry/)** one day. 
```
Install-Package Symmetry
```

**Quick start**:
----------------

This is the preview sample using a trivial in-memory collection in order to show its general purpose.
Let's start with defining the two _User_ collections - the _sourceUsers_ can be think of as source database, while the _destinationUsers_ might be another database that we would like to fill in with the data missing from the first database.

```csharp
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
```

Now, let's create an instance of _IDataSource_ - again, just assume these are 2 separate databases.

```csharp
var memoryDataSource = MemoryDataSource<User>
    .Create(sourceUsers)
    .Build();

var memoryDataDestination = MemoryDataSource<User>
    .Create(destinationUsers)
    .Build();
```

Now that we know how to fetch the values from the "databases", we need to provide an instance of _IDataStore_ in order to be able to save the missing _users_.

```csharp
var memoryDataStore = MemoryDataStore<User>
    .Create()
    .Build();
```

We're almost home, just configure our  _Synchronizer_ (you might use as many different _synchronizers_ as you wish) and we're good to go. Please note that we're setting _IComparer_ in order to quickly find out which values are unique and should be synchronized.

```csharp
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

```

And finally, start the **Symmetry** and let him do the synchronization for you!
```csharp
var symmetry = SymmetryInstance.Create(configuration);
Task.WaitAll(symmetry.RunAsync());
```