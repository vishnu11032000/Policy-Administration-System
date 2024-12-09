using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ConsumerApi.Models
{
    public class Agent
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string AgentId { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }

        [BsonElement("role")]
        public string Role { get; set; }

        [BsonElement("username")]
        public string Username { get; set; }
    }
}
