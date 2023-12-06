using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MLGStore.Services.DTOs
{
    public class CreateArticleWithImageUrlDTO
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

        public string Image { get; set; }

        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "Value must be greather than 0.")]
        public long StoreId { get; set; }
    }
}
