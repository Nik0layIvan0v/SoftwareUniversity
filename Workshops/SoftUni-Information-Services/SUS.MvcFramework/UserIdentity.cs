using System.ComponentModel.DataAnnotations;

namespace SUS.MvcFramework
{
    public class UserIdentity
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(250)]
        public string LastName { get; set; }

        public string Description { get; set; }
    }
}