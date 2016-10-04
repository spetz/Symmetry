using System;

namespace Symmetry.Examples.Console.Models
{
    public class User
    {
        public Guid Id { get; }
        public string Name { get; }

        public User(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}