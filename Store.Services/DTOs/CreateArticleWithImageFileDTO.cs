using Microsoft.AspNetCore.Http;
using MLGStore.Services.Validators;
using System.ComponentModel.DataAnnotations;

namespace MLGStore.Services.DTOs
{
    public class CreateArticleWithImageFileDTO
    {
        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Stock { get; set; }

        [MaxFileSize(10)]
        [FileType(groupFileType: GroupFileType.Image)]
        public IFormFile Image { get; set; }

        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "Value must be greather than 0.")]
        public long StoreId { get; set; }
    }
}
