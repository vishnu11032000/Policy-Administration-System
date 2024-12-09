// AgentRepository.cs
using System.Threading.Tasks;
using MongoDB.Driver;
using ConsumerApi.Models;

namespace ConsumerApi.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly MongoDbContext _context;

        public AuthRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<Agent> GetAgentByEmailAsync(string email)
        {
            return await _context.Agents.Find(a => a.Email == email).FirstOrDefaultAsync();
        }

        public async Task InsertAgentAsync(Agent agent)
        {
            await _context.Agents.InsertOneAsync(agent);
        }
    }
}
