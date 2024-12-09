using ConsumerApi.Models;
using System.Threading.Tasks;

namespace ConsumerApi.Repository
{
    public interface IConsumerPolicyRepository
    {
        Task<ConsumerPolicy> FindByConsumerIdAsync(long consumerId);//
        Task<ConsumerPolicy> FindByConsumerIdAndBusinessIdAsync(long consumerId, long businessId);//
        Task<bool> ExistsByConsumerIdAsync(long consumerId);
        Task<bool> ExistsByBusinessIdAsync(long businessId);
        Task<bool> ExistsByPolicyIdAsync(string policyId);
        Task<ConsumerPolicy> SaveAsync(ConsumerPolicy consumerPolicy);//
    }
}