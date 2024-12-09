using ConsumerApi.Models;
using ConsumerApi.Repository;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsumerApi.Services
{
    public class QuotesService : IQuotesService
    {
        private readonly IQuotesRepository _quotesRepository;
        private readonly ILogger<QuotesService> _logger;

        public QuotesService(IQuotesRepository quotesRepository, ILogger<QuotesService> logger)
        {
            _quotesRepository = quotesRepository;
            _logger = logger;
        }

        public async Task<string> GetQuoteAsync(long businessValue, long propertyValue, string propertyType)
        {
            var quoteMaster = await _quotesRepository.GetQuoteAsync(businessValue, propertyValue, propertyType);

            if (quoteMaster != null)
            {
                return quoteMaster.Quote;
            }
            return "No Quotes, Contact Insurance Provider";
        }

        public async Task<List<QuotesMaster>> GetAllQuotesAsync()
        {
            return await _quotesRepository.GetAllQuotesAsync();
        }
    }
}
