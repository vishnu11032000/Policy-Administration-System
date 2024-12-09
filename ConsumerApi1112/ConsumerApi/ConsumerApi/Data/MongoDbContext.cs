using MongoDB.Driver;
using ConsumerApi.Models;

namespace ConsumerApi.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
          
            var connectionString = configuration.GetConnectionString("MongoDb");
            var client = new MongoClient(connectionString);

            _database = client.GetDatabase("AuthDb"); 
        }

        // Provide access to the Agents collection
        public IMongoCollection<Agent> Agents => _database.GetCollection<Agent>("Agents");
    }
}
