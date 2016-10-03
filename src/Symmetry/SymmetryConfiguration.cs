namespace Symmetry
{
    public class SymmetryConfiguration
    {
        public static Builder Create() => new Builder();

        public class Builder
        {
            private readonly SymmetryConfiguration _configuration = new SymmetryConfiguration();

            public SymmetryConfiguration Build() => _configuration;
        }
    }
}