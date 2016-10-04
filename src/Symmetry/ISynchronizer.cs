using System.Threading.Tasks;

namespace Symmetry
{
    public interface ISynchronizer
    {
        Task SynchronizeAsync();
    }
}