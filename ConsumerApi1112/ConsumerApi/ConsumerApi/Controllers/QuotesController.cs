using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConsumerApi.Models;
using ConsumerApi.Services;

namespace ConsumerApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class QuotesController : ControllerBase
    {
        private readonly IQuotesService _quotesService;
        private readonly ILogger<QuotesController> _logger;

        public QuotesController(IQuotesService quotesService, ILogger<QuotesController> logger)
        {
            _quotesService = quotesService;
            _logger = logger;
        }

        [HttpGet("getQuotesForPolicy")]
        public async Task<ActionResult<string>> GetQuotes([FromQuery] long businessValue, [FromQuery] long propertyValue, [FromQuery] string propertyType)
        {
            try
            {
                var quote = await _quotesService.GetQuoteAsync(businessValue, propertyValue, propertyType);
                return Ok(quote);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving quote.");
                return StatusCode(500, "An error occurred while processing your request. Please try again later.");
            }
        }

        [HttpGet("getAllQuotes")]
        public async Task<ActionResult<List<QuotesMaster>>> GetAllQuotes()
        {
            try
            {
                var allQuotes = await _quotesService.GetAllQuotesAsync();
                return Ok(allQuotes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all quotes.");
                return StatusCode(500, "An error occurred while processing your request. Please try again later.");
            }
        }
    }
}
