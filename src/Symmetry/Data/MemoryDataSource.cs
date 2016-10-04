using System.Collections.Generic;
using System.Threading.Tasks;

namespace Symmetry.Data
{
    public class MemoryDataSource<T> : IDataSource<T>
    {
        private readonly IEnumerable<T> _values;

        protected MemoryDataSource(IEnumerable<T> values)
        {
            _values = values;
        }

        public static Builder Create(IEnumerable<T> values) => new Builder(values);

        public class Builder
        {
            private readonly IEnumerable<T> _values;

            public Builder(IEnumerable<T> values)
            {
                _values = values;
            }

            public IDataSource<T> Build() => new MemoryDataSource<T>(_values);
        }

        public async Task<IEnumerable<T>> GetAsync() => await Task.FromResult(_values);
    }
}