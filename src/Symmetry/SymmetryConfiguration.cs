using System;
using System.Collections.Generic;

namespace Symmetry
{
    public class SymmetryConfiguration
    {
        private readonly ISet<ISynchronizer> _synchronizers = new HashSet<ISynchronizer>();
        public IEnumerable<ISynchronizer> Synchronizers => _synchronizers;

        public static Builder Create() => new Builder();

        public class Builder
        {
            private readonly SymmetryConfiguration _configuration = new SymmetryConfiguration();

            public SymmetryConfiguration Build() => _configuration;

            public Builder WithSynchronizer(Func<ISynchronizer> synchronizer)
            {
                _configuration._synchronizers.Add(synchronizer());

                return this;
            }
        }
    }
}