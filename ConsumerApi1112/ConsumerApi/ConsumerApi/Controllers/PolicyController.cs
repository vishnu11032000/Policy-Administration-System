using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ConsumerApi.Repository;
using ConsumerApi.Services;
using ConsumerApi.Payload.Request;
using ConsumerApi.Payload.Response;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace PolicyModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyController : ControllerBase
    {
        private readonly IPolicyService _policyService;
        private readonly IConsumerPolicyRepository _consumerPolicyRepository;
        private readonly ILogger<PolicyController> _logger;

        public PolicyController(
            IPolicyService policyService,
            IConsumerPolicyRepository consumerPolicyRepository,
            ILogger<PolicyController> logger)
        {
            _policyService = policyService;
            _consumerPolicyRepository = consumerPolicyRepository;
            _logger = logger;
        }

        [HttpPost("createPolicy")]
        public async Task<IActionResult> CreatePolicyAsync([FromBody] CreatePolicyRequest createPolicyRequest)
        {
            try
            {
                if (await _consumerPolicyRepository.ExistsByConsumerIdAsync(createPolicyRequest.ConsumerId))
                {
                    return BadRequest(new MessageResponse("Policy already created"));
                }

                var messageResponse = await _policyService.CreatePolicyAsync(createPolicyRequest);
                return Ok(messageResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating policy.");
                return StatusCode(500, "An error occurred while processing your request. Please try again later.");
            }
        }

        [HttpGet("viewPolicy")]
        public async Task<IActionResult> ViewPolicyAsync([FromQuery] long consumerId, [FromQuery] string policyId)
        {
            try
            {
                if (!await _consumerPolicyRepository.ExistsByConsumerIdAsync(consumerId))
                {
                    return BadRequest(new MessageResponse("Sorry!! No Consumer Found!!"));
                }

                var consumerPolicy = await _consumerPolicyRepository.FindByConsumerIdAsync(consumerId);
                if (consumerPolicy.PolicyId != policyId)
                {
                    return BadRequest(new MessageResponse($"Sorry!! No Consumer with consumerId {consumerId} and policy {policyId} Found!!"));
                }

                var policyDetailsResponse = await _policyService.ViewPolicyAsync(consumerId, policyId);
                return Ok(policyDetailsResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while viewing policy.");
                return StatusCode(500, "An error occurred while processing your request. Please try again later.");
            }
        }

        [HttpPost("issuePolicy")]
        public async Task<IActionResult> IssuePolicyAsync([FromBody] IssuePolicyRequest issuePolicyRequest)
        {
            try
            {
                if (!await _consumerPolicyRepository.ExistsByConsumerIdAsync(issuePolicyRequest.ConsumerId))
                {
                    return BadRequest(new MessageResponse("Sorry!! No Consumer Found!!"));
                }

                if (!await _consumerPolicyRepository.ExistsByBusinessIdAsync(issuePolicyRequest.BusinessId))
                {
                    return BadRequest(new MessageResponse("Sorry!! No Business Found!!"));
                }

                var consumerPolicy = await _consumerPolicyRepository.FindByConsumerIdAsync(issuePolicyRequest.ConsumerId);
                if (consumerPolicy.PolicyStatus == "Issued")
                {
                    return BadRequest(new MessageResponse("Policy already issued"));
                }

                if (issuePolicyRequest.PaymentDetails != "Success")
                {
                    return BadRequest(new MessageResponse("Sorry!! Payment Failed!! Try Again"));
                }

                if (issuePolicyRequest.AcceptanceStatus != "Accepted")
                {
                    return BadRequest(new MessageResponse("Sorry!! Accept Failed!! Try Again"));
                }

                var messageResponse = await _policyService.IssuePolicyAsync(issuePolicyRequest);
                return Ok(messageResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while issuing policy.");
                return StatusCode(500, "An error occurred while processing your request. Please try again later.");
            }
        }

        [NonAction]
        public MessageResponse SendPolicyErrorResponse()
        {
            return new MessageResponse("Policy Error Response !!!");
        }
    }
}
