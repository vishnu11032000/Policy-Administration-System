using System.ComponentModel.DataAnnotations;

namespace ConsumerApi.Payload.Request
{
    public class CreatePolicyRequest
    {
        [Required]
        public long ConsumerId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "AcceptedQuotes cannot be longer than 100 characters.")]
        public string AcceptedQuotes { get; set; }

        public CreatePolicyRequest()
        {
        }
        public CreatePolicyRequest(long consumerId, string acceptedQuotes)
        {
            ConsumerId = consumerId;
            AcceptedQuotes = acceptedQuotes;
        }
    }
}
