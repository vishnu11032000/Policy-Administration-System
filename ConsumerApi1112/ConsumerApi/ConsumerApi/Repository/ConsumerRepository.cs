using ConsumerApi.Data;
using ConsumerApi.Models;
using ConsumerApi.Repository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class ConsumerRepository : IConsumerRepository
{
    private readonly AppDbContext _context;

    public ConsumerRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<bool> ExistsByIdAsync(long consumerId)
    {
        return await _context.Consumers.AnyAsync(c => c.Id == consumerId);
    }

    public async Task<bool> ExistsByPanAsync(string pan)
    {
        return await _context.Consumers.AnyAsync(c => c.Pan == pan);
    }
    public async Task<Consumer> AddAsync(Consumer consumer)
        {
            if (consumer == null)
            {
                throw new ArgumentNullException(nameof(consumer));
            }

            _context.Consumers.Add(consumer);
            await _context.SaveChangesAsync();

            return consumer;
        }

    async Task<Consumer> IConsumerRepository.GetByIdAsync(long id)
    {
       return await _context.Consumers.FindAsync(id);
            
    }

}
