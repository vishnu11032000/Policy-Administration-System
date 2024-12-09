using ConsumerApi.Payload.Response;
using ConsumerApi.Payload.Request;


namespace ConsumerApi.Services
{
    public interface IPolicyService
    {
        Task<PolicyDetailsResponse> ViewPolicyAsync(long consumerId, string policyId);
        Task<MessageResponse> CreatePolicyAsync(CreatePolicyRequest createPolicyRequest);
        Task<MessageResponse> IssuePolicyAsync(IssuePolicyRequest issuePolicyRequest);
    }
}
