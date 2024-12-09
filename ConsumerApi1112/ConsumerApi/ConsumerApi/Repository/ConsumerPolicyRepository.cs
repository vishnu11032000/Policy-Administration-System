using ConsumerApi.Models;
using Microsoft.EntityFrameworkCore;
using ConsumerApi.Data;

namespace ConsumerApi.Repository
{
    public class ConsumerPolicyRepository : IConsumerPolicyRepository
    {
        private readonly AppDbContext _context;

        public ConsumerPolicyRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ConsumerPolicy> FindByConsumerIdAsync(long consumerId)
        {
            return await _context.ConsumerPolicies
                .FirstOrDefaultAsync(cp => cp.ConsumerId == consumerId);
        }

        public async Task<ConsumerPolicy> FindByConsumerIdAndBusinessIdAsync(long consumerId, long businessId)
        {
            return await _context.ConsumerPolicies
                .FirstOrDefaultAsync(cp => cp.ConsumerId == consumerId && cp.BusinessId == businessId);
        }
        public async Task<bool> ExistsByConsumerIdAsync(long consumerId)
        {
            return await _context.ConsumerPolicies
                .AnyAsync(cp => cp.ConsumerId == consumerId);
        }

        public async Task<bool> ExistsByBusinessIdAsync(long businessId)
        {
            return await _context.ConsumerPolicies
                .AnyAsync(cp => cp.BusinessId == businessId);
        }
        public async Task<bool> ExistsByPolicyIdAsync(string policyId)
        {
            return await _context.ConsumerPolicies
                .AnyAsync(cp => cp.PolicyId == policyId);
        }
        public async Task<ConsumerPolicy> SaveAsync(ConsumerPolicy consumerPolicy)
        {
            _context.ConsumerPolicies.Update(consumerPolicy);
            await _context.SaveChangesAsync();
            return consumerPolicy;
        }
    }
}
