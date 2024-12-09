using ConsumerApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsumerApi.Repository
{
    public interface IQuotesRepository
    {
        Task<QuotesMaster> GetQuoteAsync(long businessValue, long propertyValue, string propertyType);
        Task<List<QuotesMaster>> GetAllQuotesAsync();
    }
}
