using System.ComponentModel.DataAnnotations;

namespace MLGStore.Services.DTOs
{
    public class CreateCustomerDTO
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Surnames { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [StringLength(25)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
