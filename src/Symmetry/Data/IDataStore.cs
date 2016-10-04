using System.Collections.Generic;
using System.Threading.Tasks;

namespace Symmetry.Data
{
    public interface IDataStore<in T>
    {
        Task SaveAsync(IEnumerable<T> values);
    }
}