using System;
using System.ComponentModel.DataAnnotations;

namespace MyFirstMvcApp.Data.EntityModels
{
    public class User
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        
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
