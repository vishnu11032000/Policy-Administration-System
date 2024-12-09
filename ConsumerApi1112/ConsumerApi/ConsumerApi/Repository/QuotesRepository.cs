using ConsumerApi.Data;
using ConsumerApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsumerApi.Repository
{
    public class QuotesRepository : IQuotesRepository
    {
        private readonly AppDbContext _context;

        public QuotesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<QuotesMaster> GetQuoteAsync(long businessValue, long propertyValue, string propertyType)
        {
            return await _context.Quotes
                .FirstOrDefaultAsync(q => q.BusinessValue == businessValue && q.PropertyValue == propertyValue && q.PropertyType == propertyType);
        }

        public async Task<List<QuotesMaster>> GetAllQuotesAsync()
        {
            return await _context.Quotes.ToListAsync();
        }
    }
}
