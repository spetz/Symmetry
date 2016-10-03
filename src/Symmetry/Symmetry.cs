using System.Threading.Tasks;

namespace Symmetry
{
    public class Symmetry : ISymmetry
    {
        public async Task RunAsync()
        {
            await Task.CompletedTask;
        }
    }
}