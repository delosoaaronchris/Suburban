using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Suburban.ViewModels
{
    public class ProductViewModel : EditImageViewModel
    {
        [Required]
        [Display(Name = "Brand: ")]
        public string Brand { get; set; }

        [Required]
        [Display(Name = "Name: ")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Price: ")]
        public int Price { get; set; }

        [Required]
        [Display(Name = "Quantity: ")]
        public int Quantity { get; set; }

        [Required]
        [Display(Name = "Description: ")]
        public string Description { get; set; }
    }
}

