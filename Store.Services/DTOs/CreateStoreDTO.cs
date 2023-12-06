using System.ComponentModel.DataAnnotations;

namespace MLGStore.Services.DTOs
{
    public class CreateStoreDTO
    {
        [Required]
        [StringLength(50)]
        public string Branch { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
