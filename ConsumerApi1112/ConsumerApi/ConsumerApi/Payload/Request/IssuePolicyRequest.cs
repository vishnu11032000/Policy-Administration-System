using System.ComponentModel.DataAnnotations;

namespace ConsumerApi.Payload.Request
{
    public class IssuePolicyRequest
    {
        [Required]
        [StringLength(50, ErrorMessage = "PolicyId cannot be longer than 50 characters.")]
        public string PolicyId { get; set; }

        [Required]
        public long ConsumerId { get; set; }

        [Required]
        public long BusinessId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "PaymentDetails cannot be longer than 100 characters.")]
        public string PaymentDetails { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "AcceptanceStatus cannot be longer than 50 characters.")]
        public string AcceptanceStatus { get; set; }

        public IssuePolicyRequest()
        {
        }

        public IssuePolicyRequest(string policyId, long consumerId, long businessId, string paymentDetails, string acceptanceStatus)
        {
            PolicyId = policyId;
            ConsumerId = consumerId;
            BusinessId = businessId;
            PaymentDetails = paymentDetails;
            AcceptanceStatus = acceptanceStatus;
        }
    }
}
