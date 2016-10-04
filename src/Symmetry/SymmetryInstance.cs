namespace Symmetry
{
    public static class SymmetryInstance
    {
        public static ISymmetry Create(SymmetryConfiguration configuration) => new Symmetry(configuration);
    }
}