using System.Collections.Generic;
using System.Threading.Tasks;

namespace Symmetry.Data
{
    public class MemoryDataStore<T> : IDataStore<T>
    {
        private readonly ISet<T> _values = new HashSet<T>();

        protected MemoryDataStore()
        {
        }

        public static Builder Create() => new Builder();

        public class Builder
        {
            public IDataStore<T> Build() => new MemoryDataStore<T>();
        }

        public async Task SaveAsync(IEnumerable<T> values)
        {
            foreach (var value in values)
            {
                _values.Add(value);
            }
            await Task.CompletedTask;
        }
    }
}