using ConsumerApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConsumerApi.Repository
{
    public interface IBusinessRepository
    {
        Task<Bussiness> FindByConsumerIdAsync(long consumerId);
        Task<bool> ExistsByBusinessNameAsync(string businessName);
        Task<bool> ExistsByConsumerIdAsync(long consumerId);
        Task<Bussiness> UpdateConsumerBusinessAsync(UpdateRequest updateRequest);
        Task<Bussiness> GetByIdAsync(long id);
        Task<Bussiness> AddAsync(Bussiness business);
        Task UpdateAsync(Bussiness business);
        Task<Bussiness> GetByConsumerIdAsync(long consumerId);
        Task<bool> ExistsByIdAsync(long businessId);
    }
}
