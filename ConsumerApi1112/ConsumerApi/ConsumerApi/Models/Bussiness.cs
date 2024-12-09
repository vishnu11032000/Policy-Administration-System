using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsumerApi.Models
{
    [Table("Business")]
    public class Bussiness
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public long ConsumerId { get; set; }

        [Required]
        [StringLength(255)]
        public string BusinessName { get; set; }

        [Required]
        [StringLength(255)]
        public string BusinessType { get; set; }

        [Required]
        public int BusinessAge { get; set; }

        [Required]
        public int TotalEmployees { get; set; }
        
        [Required]  
        public int BusinessValue { get; set; }  

        [Required]
        public long CapitalInvested { get; set; }

        [Required]
        public long BusinessTurnover { get; set; }
    }
}
