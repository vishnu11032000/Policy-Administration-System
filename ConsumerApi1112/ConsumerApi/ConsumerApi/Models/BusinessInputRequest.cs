namespace ConsumerApi.Models
{
    public class BusinessInputRequest
    {
        public long BusinessId { get; set; }
        public long ConsumerId { get; set; }
        public int BuildingSqFt { get; set; }
        public string BuildingType { get; set; }
        public string BuildingStoreys { get; set; }
        public int BuildingAge { get; set; }
        public long CostOftheAsset { get; set; }
        public long SalvageValue { get; set; }
        public long UsefulLifeofAsset { get; set; }
    }
}
