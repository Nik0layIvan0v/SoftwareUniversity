using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarShop.Data.Models
{
    public class User
    {
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(20)]
        public string Username { get; set; } //– a string with min length 4 and max length 20 (required)

        [Required]
        public string Email { get; set; } //- a string (required)

        [Required]
        public string Password { get; set; } //– a string with min length 5 and max length 20  - hashed in the database (required)

        public bool IsMechanic { get; set; } //– a bool indicating if the user is a mechanic or a client

        public virtual ICollection<Car> Cars { get; set; } = new HashSet<Car>();

    }
}