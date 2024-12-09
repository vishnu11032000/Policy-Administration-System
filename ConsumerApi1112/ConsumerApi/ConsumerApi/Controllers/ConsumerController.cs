using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using ConsumerApi.Models;
using ConsumerApi.Services;
using ConsumerApi.Repository;
using ConsumerApi.Data;

namespace ConsumerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ConsumerController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ConsumerController> _logger;
        private readonly IConsumerService _consumerService;
        private readonly IConsumerRepository _consumerRepository;
        private readonly IBusinessRepository _businessRepository;
        private readonly IPropertyRepository _propertyRepository;

        public ConsumerController(
            ILogger<ConsumerController> logger,
            IConsumerService consumerService,
            AppDbContext context,
            IConsumerRepository consumerRepository,
            IBusinessRepository businessRepository,
            IPropertyRepository propertyRepository)
        {
            _logger = logger;
            _context = context;
            _consumerService = consumerService;
            _consumerRepository = consumerRepository;
            _businessRepository = businessRepository;
            _propertyRepository = propertyRepository;
        }

        [HttpPost("createConsumerBusiness")]
        public async Task<IActionResult> CreateConsumerBusiness([FromBody] ConsumerBusinessRequest inputRequest)
        {
            try
            {
                if (!_consumerService.CheckBusinessEligibility(inputRequest))
                {
                    return BadRequest("Sorry! You are not eligible");
                }

                var result = await _consumerService.CreateConsumerBusinessAsync(inputRequest);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating consumer business");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        [HttpPut("updateConsumerBusiness")]
        public async Task<IActionResult> UpdateConsumerBusiness([FromBody] UpdateRequest updateRequest)
        {
            try
            {
                var updateConsumerBusiness = await _businessRepository.UpdateConsumerBusinessAsync(updateRequest);
                if (updateConsumerBusiness != null)
                {
                    return Ok(updateConsumerBusiness);
                }
                return BadRequest("Failed to update the consumer business");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating consumer business");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        [HttpGet("viewConsumerBusiness")]
        public async Task<IActionResult> ViewConsumerBusinessResponse([FromQuery] long consumerId)
        {
            try
            {
                if (!await _consumerRepository.ExistsByIdAsync(consumerId))
                {
                    return NotFound("Consumer not found");
                }

                if (!await _businessRepository.ExistsByConsumerIdAsync(consumerId))
                {
                    return BadRequest("No Business Found!!");
                }

                var consumerBusinessDetails = await _consumerService.ViewConsumerBusinessAsync(consumerId);
                return Ok(consumerBusinessDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error viewing consumer business");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        [HttpPost("createBusinessProperty")]
        public async Task<IActionResult> CreateBusinessProperty([FromBody] BusinessInputRequest inputRequest)
        {
            try
            {
                if (await _propertyRepository.ExistsByBusinessIdAsync(inputRequest.BusinessId))
                {
                    return BadRequest("BusinessProperty already exists");
                }

                if (!await _consumerRepository.ExistsByIdAsync(inputRequest.ConsumerId))
                {
                    return BadRequest("Consumer doesn't exist");
                }

                if (!await _businessRepository.ExistsByIdAsync(inputRequest.BusinessId))
                {
                    return BadRequest("Business doesn't exist");
                }

                if (!_consumerService.CheckPropertyEligibility(inputRequest))
                {
                    return BadRequest("Sorry! You are not eligible");
                }

                var result = await _consumerService.CreateBusinessPropertyAsync(inputRequest);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating business property");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        [HttpPut("updateBusinessProperty")]
        public async Task<IActionResult> UpdateBusinessProperty([FromBody] BusinessUpdateRequest updateRequest)
        {
            try
            {
                var existingProperty = await _consumerService.UpdateBusinessPropertyAsync(updateRequest);
                if (existingProperty != null)
                {
                    return Ok(existingProperty);
                }
                return BadRequest("Failed to update the business property");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating business property");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        [HttpGet("viewConsumerProperty")]
        public async Task<IActionResult> ViewConsumerProperty([FromQuery] long consumerId, [FromQuery] long propertyId)
        {
            try
            {
                if (!await _propertyRepository.ExistsByIdAsync(propertyId))
                {
                    return BadRequest("No Property Found!!");
                }

                if (!await _consumerRepository.ExistsByIdAsync(consumerId))
                {
                    return BadRequest("No Consumer Found!!");
                }

                if (await _businessRepository.FindByConsumerIdAsync(consumerId) == null)
                {
                    return BadRequest("No Business Found!!");
                }

                var property = await _propertyRepository.FindByIdAsync(propertyId);
                return Ok(property);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error viewing consumer property");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }
    }
}
