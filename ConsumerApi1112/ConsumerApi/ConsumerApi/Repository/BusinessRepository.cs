using ConsumerApi.Data;
using ConsumerApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ConsumerApi.Repository
{
    public class BusinessRepository : IBusinessRepository
    {
        private readonly AppDbContext _context;

        public BusinessRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Bussiness> FindByConsumerIdAsync(long consumerId)
        {
            return await _context.Bussinesses.FirstOrDefaultAsync(b => b.ConsumerId == consumerId);
        }

        public async Task<bool> ExistsByBusinessNameAsync(string businessName)
        {
            return await _context.Bussinesses.AnyAsync(b => b.BusinessName == businessName);
        }

        public async Task<bool> ExistsByConsumerIdAsync(long consumerId)
        {
            return await _context.Bussinesses.AnyAsync(b => b.ConsumerId == consumerId);
        }

        public async Task<Bussiness> GetByIdAsync(long id)
        {
            return await _context.Bussinesses.FindAsync(id);
        }

        public async Task<Bussiness> AddAsync(Bussiness business)
        {
            if (business == null)
            {
                throw new ArgumentNullException(nameof(business));
            }

            _context.Bussinesses.Add(business);
            await _context.SaveChangesAsync();

            return business;
        }

        public async Task UpdateAsync(Bussiness business)
        {
            if (business == null)
            {
                throw new ArgumentNullException(nameof(business));
            }

            _context.Bussinesses.Update(business);
            await _context.SaveChangesAsync();
        }

        public async Task<Bussiness> GetByConsumerIdAsync(long consumerId)
        {
            return await _context.Bussinesses.FirstOrDefaultAsync(b => b.ConsumerId == consumerId);
        }

        public async Task<bool> ExistsByIdAsync(long businessId)
        {
            return await _context.Bussinesses.AnyAsync(b => b.Id == businessId);
        }

        public  async Task<Bussiness> UpdateConsumerBusinessAsync(UpdateRequest updateRequest)
        {

            var ExistingBussiness = await _context.Bussinesses
                .Where(b => b.ConsumerId == updateRequest.ConsumerId)
                .FirstOrDefaultAsync();

            var consumerDetails = await _context.Consumers
                .Where(c => c.Id == updateRequest.ConsumerId)
                .FirstOrDefaultAsync();

            if (consumerDetails == null || ExistingBussiness == null)
            {
                return null;
            }

            consumerDetails.BusinessName = updateRequest.BusinessName;
            consumerDetails.FirstName = updateRequest.FirstName;
            consumerDetails.LastName = updateRequest.LastName;
            consumerDetails.Email = updateRequest.Email;
            consumerDetails.Pan = updateRequest.Pan;
            consumerDetails.Dob = updateRequest.Dob;
            consumerDetails.Validity = updateRequest.Validity;
            consumerDetails.AgentName = updateRequest.AgentName;
            consumerDetails.AgentId = updateRequest.AgentId;
            ExistingBussiness.BusinessName = updateRequest.BusinessName;
            ExistingBussiness.BusinessAge = updateRequest.BusinessAge;
            ExistingBussiness.BusinessTurnover = (long)updateRequest.BusinessTurnover;
            ExistingBussiness.TotalEmployees = updateRequest.TotalEmployees;
            ExistingBussiness.CapitalInvested = (long)updateRequest.CapitalInvested;
            ExistingBussiness.BusinessType = updateRequest.BusinessType;

            await _context.SaveChangesAsync();
            return ExistingBussiness;
        }
    }
}
