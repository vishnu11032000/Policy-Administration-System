// IAuthService.cs
using System.Threading.Tasks;
using ConsumerApi.Models;

namespace ConsumerApi.Services
{
    public interface IAuthService
    {
        Task<Agent> RegisterAsync(AgentRegister model);
        Task<string> LoginAsync(AgentLogin model);
    }
}
