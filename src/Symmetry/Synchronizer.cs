using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Symmetry
{
    public class Synchronizer<T> : ISynchronizer
    {
        private readonly SynchronizerConfiguration<T> _configuration;

        public Synchronizer(SynchronizerConfiguration<T> configuration)
        {
            _configuration = configuration;
        }

        public async Task SynchronizeAsync()
        {
            var uniqueValues = new HashSet<T>();
            var sourceData = await _configuration.DataSource().GetAsync();
            var destinationData = await _configuration.DataDestination().GetAsync();
            var comparer = _configuration.Comparer();
            foreach (var sourceValue in sourceData)
            {
                var valueExists = destinationData.Any(x => comparer.AreEqual(sourceValue, x));
                if(valueExists)
                    continue;

                uniqueValues.Add(sourceValue);
            }
            var dataStore = _configuration.DataStore();
            await dataStore.SaveAsync(uniqueValues);
        }

        public static Builder Create(SynchronizerConfiguration<T> configuration) => new Builder(configuration);

        public class Builder
        {
            private readonly SynchronizerConfiguration<T> _configuration;

            public Builder(SynchronizerConfiguration<T> configuration)
            {
                _configuration = configuration;
            }

            public ISynchronizer Build() => new Synchronizer<T>(_configuration);
        }
    }
}