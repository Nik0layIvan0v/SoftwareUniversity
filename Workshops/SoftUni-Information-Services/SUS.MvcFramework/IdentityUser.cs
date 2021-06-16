using System.ComponentModel.DataAnnotations;

namespace SUS.MvcFramework
{
    public class IdentityUser <T>
    {
        [Required]
        public T Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Username { get; set; }

        [Required]
        [MaxLength(250)]
        public string Email { get; set; }

        public string Description { get; set; }
    }
}