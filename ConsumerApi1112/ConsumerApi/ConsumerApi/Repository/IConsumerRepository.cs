using ConsumerApi.Models;

namespace ConsumerApi.Repository
{
    public interface IConsumerRepository
    {
        Task<bool> ExistsByPanAsync(string pan);
        Task<Consumer> AddAsync(Consumer consumer);
        Task<bool> ExistsByIdAsync(long consumerId);
        Task<Consumer> GetByIdAsync(long consumerId);

    }
}
