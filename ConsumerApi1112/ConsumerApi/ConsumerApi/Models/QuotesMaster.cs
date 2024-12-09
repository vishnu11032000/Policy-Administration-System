using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsumerApi.Models
{
    [Table("quotes")]  
    public class QuotesMaster
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public long BusinessValue { get; set; }

        [Required]
        public long PropertyValue { get; set; }

        [Required]
        [MaxLength(50)]
        public string PropertyType { get; set; }

        [Required]
        [MaxLength(50)]
        public string Quote { get; set; }


    }
}
