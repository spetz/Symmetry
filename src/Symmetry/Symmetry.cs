using System.Linq;
using System.Threading.Tasks;

namespace Symmetry
{
    public class Symmetry : ISymmetry
    {
        private readonly SymmetryConfiguration _configuration;

        public Symmetry(SymmetryConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task RunAsync()
        {
            var tasks = _configuration.Synchronizers.Select(x => x.SynchronizeAsync());
            await Task.WhenAll(tasks);
        }
    }
}