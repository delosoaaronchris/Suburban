using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Suburban.ViewModels
{
    public class UploadImageViewModel
    {
        [Required]
        [Display(Name = "Image")]
        public IFormFile ProductPicture { get; set; }
    }
}