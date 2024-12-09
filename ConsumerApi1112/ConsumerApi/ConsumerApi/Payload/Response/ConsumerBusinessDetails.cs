using System.ComponentModel.DataAnnotations;

namespace ConsumerApi.Payload.Response
{
    public class ConsumerBusinessDetails
    {
        [Required]
        public long ConsumerId { get; set; }

        [Required]
        public long BusinessId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "FirstName cannot be longer than 50 characters.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "LastName cannot be longer than 50 characters.")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
        public string Email { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "PAN must be 10 characters.")]
        public string Pan { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "DOB must be in a valid format (e.g., YYYY-MM-DD).")]
        public string Dob { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "BusinessName cannot be longer than 100 characters.")]
        public string BusinessName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "BusinessType cannot be longer than 50 characters.")]
        public string BusinessType { get; set; }

        [Required]
        public long CapitalInvested { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Validity cannot be longer than 50 characters.")]
        public string Validity { get; set; }

        [Required]
        public long AgentId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "AgentName cannot be longer than 100 characters.")]
        public string AgentName { get; set; }

        [Required]
        public long BusinessTurnover { get; set; }

        [Required]
        public long BusinessAge { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "TotalEmployees must be a valid number.")]
        public string TotalEmployees { get; set; }

        public ConsumerBusinessDetails()
        {
        }
        public ConsumerBusinessDetails(
            long consumerId,
            long businessId,
            string firstName,
            string lastName,
            string email,
            string pan,
            string dob,
            string businessName,
            string businessType,
            long capitalInvested,
            string validity,
            long agentId,
            string agentName,
            long businessTurnover,
            long businessAge,
            string totalEmployees)
        {
            ConsumerId = consumerId;
            BusinessId = businessId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Pan = pan;
            Dob = dob;
            BusinessName = businessName;
            BusinessType = businessType;
            CapitalInvested = capitalInvested;
            Validity = validity;
            AgentId = agentId;
            AgentName = agentName;
            BusinessTurnover = businessTurnover;
            BusinessAge = businessAge;
            TotalEmployees = totalEmployees;
        }
    }
}
