using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Entity
{
    [BsonIgnoreExtraElements]
    public class User
    {
        [BsonIgnoreIfDefault]
        public ObjectId Id { get; set; }

        [BsonIgnoreIfNull]
        public string Login { get; set; }

        [BsonIgnoreIfNull]
        public string FirstName { get; set; }

        [BsonIgnoreIfNull]
        public string LastName { get; set; }

        [BsonIgnoreIfNull]
        public string HashedPassword { get; set; }

        [BsonIgnoreIfNull]
        public List<string> Followers { get; set; }

        [BsonIgnoreIfNull]
        public List<string> Following { get; set; }

        [BsonIgnoreIfNull]
        public string LastLogin { get; set; }
    }
}
