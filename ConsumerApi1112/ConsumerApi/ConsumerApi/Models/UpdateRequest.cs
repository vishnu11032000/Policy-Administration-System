namespace ConsumerApi.Models
{
    public class UpdateRequest
    {
        public long ConsumerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Pan { get; set; }
        public DateTime Dob { get; set; }
        public string BusinessName { get; set; }
        public bool Validity { get; set; }
        public string AgentName { get; set; }
        public string AgentId { get; set; }
        public string BusinessType { get; set; }
        public int BusinessAge { get; set; }
        public int TotalEmployees { get; set; }
        public decimal CapitalInvested { get; set; }
        public decimal BusinessTurnover { get; set; }
    }
}
