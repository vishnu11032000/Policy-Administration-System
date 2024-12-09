using System.ComponentModel.DataAnnotations;

namespace ConsumerApi.Payload.Response
{
    public class PolicyDetailsResponse
    {
        [Required]
        public long ConsumerId { get; set; }

        [Required]
        [StringLength(100)]
        public string PolicyId { get; set; }

        [Required]
        [StringLength(100)]
        public string PropertyType { get; set; }

        [Required]
        [StringLength(100)]
        public string ConsumerType { get; set; }

        [Required]
        [StringLength(100)]
        public string AssuredSum { get; set; }

        [Required]
        [StringLength(50)]
        public string Tenure { get; set; }

        [Required]
        public long BusinessValue { get; set; }

        [Required]
        public long PropertyValue { get; set; }

        [Required]
        [StringLength(100)]
        public string BaseLocation { get; set; }

        [Required]
        [StringLength(50)]
        public string Type { get; set; }

        [Required]
        public long BusinessId { get; set; }

        [Required]
        [StringLength(100)]
        public string PaymentDetails { get; set; }

        [Required]
        [StringLength(50)]
        public string AcceptanceStatus { get; set; }

        [Required]
        [StringLength(50)]
        public string PolicyStatus { get; set; }

        [Required]
        [StringLength(50)]
        public string EffectiveDate { get; set; }

        [Required]
        [StringLength(100)]
        public string CoveredSum { get; set; }

        [Required]
        [StringLength(50)]
        public string Duration { get; set; }

        [Required]
        [StringLength(100)]
        public string AcceptedQuote { get; set; }
    }
}
