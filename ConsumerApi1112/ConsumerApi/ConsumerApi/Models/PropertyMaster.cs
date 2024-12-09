using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConsumerApi.Models
{
    [Table("Property_Master")]
    public class PropertyMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [StringLength(255)]
        public string BuildingType { get; set; }

        [Required]
        public long BuildingAge { get; set; }
    }
}
