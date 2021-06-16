using System;
using System.ComponentModel.DataAnnotations;
using SUS.MvcFramework;

namespace MyFirstMvcApp.Data.EntityModels
{
    public class User : IdentityUser<string>
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Role = IdentityRole.User;
        }

        [Required]
        public string Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Username { get; set; }

        [Required]
        [MaxLength(250)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string Description { get; set; }

        public IdentityRole Role { get; set; }
    }
}
