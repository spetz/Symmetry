using System.Collections.Generic;
using System.Threading.Tasks;

namespace Symmetry.Data
{
    public interface IDataSource<T>
    {
        Task<IEnumerable<T>> GetAsync();
    }
}