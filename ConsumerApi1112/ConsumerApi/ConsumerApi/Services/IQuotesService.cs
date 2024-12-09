using ConsumerApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsumerApi.Services
{
    public interface IQuotesService
    {
        Task<string> GetQuoteAsync(long businessValue, long propertyValue, string propertyType);
        Task<List<QuotesMaster>> GetAllQuotesAsync();
    }
}
