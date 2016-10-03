using System.Threading.Tasks;

namespace Symmetry.Examples.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = SymmetryConfiguration.Create()
                .Build();
            var symmetry = SymmetryInstance.Create(configuration);
            Task.WaitAll(symmetry.RunAsync());
        }
    }
}
