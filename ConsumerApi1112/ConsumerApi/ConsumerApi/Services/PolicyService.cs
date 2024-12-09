using ConsumerApi.Data;

using ConsumerApi.Models;
using ConsumerApi.Payload.Request;
using ConsumerApi.Payload.Response;
using ConsumerApi.Repository;
using Microsoft.Extensions.Logging;

namespace ConsumerApi.Services
{
    public class PolicyService : IPolicyService
    {
        private readonly IConsumerPolicyRepository _consumerPolicyRepository;
        private readonly ILogger<PolicyService> _logger;

        private readonly AppDbContext _context;
        public PolicyService(
            IConsumerPolicyRepository consumerPolicyRepository,
            ILogger<PolicyService> logger,
            AppDbContext context)
        {
            _consumerPolicyRepository = consumerPolicyRepository;
         
            _context = context;
        }
        public async Task<PolicyDetailsResponse> ViewPolicyAsync(long consumerId, string policyId)
        {
            
            var consumerPolicy = await _consumerPolicyRepository.FindByConsumerIdAsync(consumerId)
                ?? throw new Exception("Consumer policy not found.");
            
            return new PolicyDetailsResponse
            {
                ConsumerId = consumerId,
                PolicyId = consumerPolicy.PolicyId,
                PropertyType = consumerPolicy.PropertyType,
                ConsumerType = consumerPolicy.ConsumerType,
                AssuredSum = consumerPolicy.AssuredSum,
                Tenure = consumerPolicy.Tenure,
                BusinessValue = consumerPolicy.BusinessValue.GetValueOrDefault(),
                PropertyValue = consumerPolicy.PropertyValue.GetValueOrDefault(),
                BaseLocation = consumerPolicy.BaseLocation,
                Type = consumerPolicy.Type,
                BusinessId = consumerPolicy.BusinessId,
                PaymentDetails = consumerPolicy.PaymentDetails,
                AcceptanceStatus = consumerPolicy.AcceptanceStatus,
                PolicyStatus = consumerPolicy.PolicyStatus,
                EffectiveDate = consumerPolicy.EffectiveDate,
                CoveredSum = consumerPolicy.CoveredSum,
                Duration = consumerPolicy.Duration,
                AcceptedQuote = consumerPolicy.AcceptedQuote
            };
        }
        public async Task<MessageResponse> CreatePolicyAsync(CreatePolicyRequest createPolicyRequest)
        {
            var ExistingBusiness = _context.Bussinesses.Where(b => b.ConsumerId == createPolicyRequest.ConsumerId).FirstOrDefault();
            decimal businessAgeContribution = Convert.ToDecimal(ExistingBusiness.BusinessAge);
            string[] quote = createPolicyRequest.AcceptedQuotes.Split(" ");
            decimal coveredsum = decimal.Parse(quote[0] )/( businessAgeContribution * 0.9m) ;
            decimal assuredSum = decimal.Parse(quote[0]) / (businessAgeContribution* 0.5m);
            var quotes = _context.Quotes.Where(Q => Q.Quote == createPolicyRequest.AcceptedQuotes).FirstOrDefault();
            if (quote == null) {
                return new MessageResponse($"error ehile fetching quotes");
            }

            var bussinessvalue = quotes.BusinessValue;

            var propertyvalue = quotes.PropertyValue;
            var type = quotes.PropertyType;
            var consumerPolicy = new ConsumerPolicy
            {
                BusinessId = ExistingBusiness.Id,
                ConsumerId = createPolicyRequest.ConsumerId,
                AcceptanceStatus = "Initiated",
                AcceptedQuote = createPolicyRequest.AcceptedQuotes,
                CoveredSum = Convert.ToString(coveredsum),
                Duration = "5 Years",
                EffectiveDate = Convert.ToString(DateTime.Now),
                PolicyStatus = "initiated",
                PropertyType = quotes.PropertyType,
                ConsumerType = ExistingBusiness.BusinessType,
                AssuredSum = Convert.ToString(assuredSum),
                Tenure = "5 Years",
                PropertyValue = propertyvalue,
                BusinessValue = bussinessvalue,
                Type = type,
                BaseLocation= "---"
            };
            var savedPolicy = await _consumerPolicyRepository.SaveAsync(consumerPolicy);
            return new MessageResponse($"Policy has been created with Policy Consumer Id: {savedPolicy.PolicyId}. Thank You Very Much!!");
        }
        public async Task<MessageResponse> IssuePolicyAsync(IssuePolicyRequest issuePolicyRequest)
        {
            var now = DateTime.UtcNow;
            var dtf = "dd/MM/yyyy";

            var consumerPolicy = await _consumerPolicyRepository.FindByConsumerIdAndBusinessIdAsync(issuePolicyRequest.ConsumerId, issuePolicyRequest.BusinessId)
                ?? throw new Exception("Consumer policy not found.");
           
            consumerPolicy.PolicyId = issuePolicyRequest.PolicyId;
            consumerPolicy.PaymentDetails = issuePolicyRequest.PaymentDetails;
            consumerPolicy.AcceptanceStatus = issuePolicyRequest.AcceptanceStatus;
            consumerPolicy.PolicyStatus = "Issued";
           
            await _consumerPolicyRepository.SaveAsync(consumerPolicy);
            return new MessageResponse($"Policy has been issued to Policy Consumer Id: {consumerPolicy.PolicyId}. Thank You Very Much!!");
        }
    }
}
