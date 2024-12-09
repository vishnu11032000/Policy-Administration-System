using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsumerApi.Models
{
    [Table("Property")]
    public class Property
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public long BusinessId { get; set; }

        [Required]
        public long ConsumerId { get; set; }

        [Required]
        [StringLength(255)]
        public int BuildingSqFt { get; set; }

        [Required]
        [StringLength(255)]
        public string BuildingType { get; set; }

        [Required]
        [StringLength(255)]
        public string BuildingStoreys { get; set; }

        [Required]
        public long BuildingAge { get; set; }

        [Required]
        public long PropertyValue { get; set; }

        [Required]
        public long CostOfTheAsset { get; set; }

        [Required]
        public long SalvageValue { get; set; }

        [Required]
        public long UsefulLifeOfAsset { get; set; }
    }
}
