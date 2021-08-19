using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerAPI.Entities
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [MaxLength(20)]
        [Required]
        public string Title { get; set; }

        [MaxLength(50)]
        [Required]
        public string Forename { get; set; }

        [MaxLength(50)]
        [Required]
        public string Surname { get; set; }

        [MaxLength(75)]
        [Required]
        public string EmailAddress { get; set; }

        [MaxLength(15)]
        [Required]
        public string MobileNo { get; set; }

        public List<Address> Addresses { get; set; }
    }
}
