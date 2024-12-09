// IAgentRepository.cs
using System.Threading.Tasks;
using ConsumerApi.Models;

namespace ConsumerApi.Data
{
    public interface IAuthRepository
    {
        Task<Agent> GetAgentByEmailAsync(string email);
        Task InsertAgentAsync(Agent agent);
    }
}
