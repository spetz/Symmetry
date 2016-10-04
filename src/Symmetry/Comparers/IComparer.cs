namespace Symmetry.Comparers
{
    public interface IComparer<in T>
    {
        bool AreEqual(T first, T second);
    }
}