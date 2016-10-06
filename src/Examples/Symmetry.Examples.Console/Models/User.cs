using MongoDB.Bson.Serialization.Attributes;

namespace Symmetry.Examples.Console.Models
{
    [BsonIgnoreExtraElements]
    public class User
    {
        public string Name { get; protected set; }

        protected User()
        {
        }

        public User(string name)
        {
            Name = name;
        }
    }
}