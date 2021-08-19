using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAPI.Entities
{
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        public string CustomerId { get; set; }

        [MaxLength(80)]
        [Required]
        public string AddressLine1 { get; set; }

        [MaxLength(80)]
        public string AddressLine2 { get; set; }

        [MaxLength(50)]
        [Required]
        public string Town { get; set; }

        [MaxLength(50)]
        public string County { get; set; }

        [MaxLength(10)]
        [Required]
        public string Postcode { get; set; }

        public string Country { get; set; }
    }
}
