using System;

namespace Symmetry.Comparers
{
    public class ObjectComparer<T> : IComparer<T>
    {
        private readonly Func<T, T, bool> _comparison;

        protected ObjectComparer(Func<T, T, bool> comparison)
        {
            _comparison = comparison;
        }

        public static IComparer<T> Create(Func<T, T, bool> comparison) => new ObjectComparer<T>(comparison);

        public bool AreEqual(T first, T second) => _comparison(first, second);
    }
}