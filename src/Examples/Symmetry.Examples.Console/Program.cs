using System.Threading.Tasks;

namespace Symmetry.Examples.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var inMemorySynchronizer = InMemory.GetSynchronizer();
            var mongoDbSynchronizer = MongoDb.GetSynchronizer();

            var configuration = SymmetryConfiguration
                .Create()
                .WithSynchronizer(() => inMemorySynchronizer)
                .WithSynchronizer(() => mongoDbSynchronizer)
                .Build();

            var symmetry = SymmetryInstance.Create(configuration);
            Task.WaitAll(symmetry.RunAsync());
        }
    }
}
