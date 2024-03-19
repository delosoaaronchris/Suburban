using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Suburban.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Brand: ")]
        public string Brand { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Name: ")]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Price: ")]
        public int Price { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Quantity: ")]
        public int Quantity { get; set; }

        [Required]
        [StringLength(500)]
        [Display(Name = "Description: ")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Product Image:")]
        public string ProductImage { get; set; }
    }
}
