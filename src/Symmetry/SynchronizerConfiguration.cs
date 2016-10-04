using System;
using Symmetry.Comparers;
using Symmetry.Data;

namespace Symmetry
{
    public class SynchronizerConfiguration<T>
    {
        public Func<IDataSource<T>> DataSource { get; protected set; }
        public Func<IDataSource<T>> DataDestination { get; protected set; }
        public Func<IDataStore<T>> DataStore { get; protected set; }
        public Func<IComparer<T>> Comparer { get; protected set; }

        protected SynchronizerConfiguration()
        {
        }

        public static Builder Create() => new Builder();

        public class Builder
        {
            private readonly SynchronizerConfiguration<T> _configuration = new SynchronizerConfiguration<T>();

            public SynchronizerConfiguration<T> Build() => _configuration;

            public Builder WithDataSource(Func<IDataSource<T>> dataSource)
            {
                _configuration.DataSource = dataSource;

                return this;
            }

            public Builder WithDataDestination(Func<IDataSource<T>> dataDestination)
            {
                _configuration.DataDestination = dataDestination;

                return this;
            }

            public Builder WithDataStore(Func<IDataStore<T>> dataStore)
            {
                _configuration.DataStore = dataStore;

                return this;
            }

            public Builder WithComparer(Func<IComparer<T>> comparer)
            {
                _configuration.Comparer = comparer;

                return this;
            }
        }
    }
}