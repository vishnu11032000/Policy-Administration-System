using ConsumerApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConsumerApi.Repository
{
    public interface IPropertyRepository
    {
        Task<Property> FindByConsumerIdAsync(long consumerId);
        Task<bool> ExistsByBusinessIdAsync(long id);
        Task<bool> ExistsByConsumerIdAsync(long id);
        Task<Property> GetByConsumerIdAsync(long consumerId);
        Task<Property> AddAsync(Property property);
        Task UpdateAsync(Property property);
        Task<bool> ExistsByIdAsync(long propertyId);
        Task<Property> FindByIdAsync(long propertyId);
        Task<Property> UpdateBusinessPropertyAsync( BusinessUpdateRequest updateRequest);






    }
}
