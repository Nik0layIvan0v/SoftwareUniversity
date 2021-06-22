using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarShop.Data.Models
{
    public class Car
    {
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(20)]
        public string Model { get; set; } //– a string with min length 5 and max length 20 (required)

        public int Year { get; set; } //– a number (required)

        [Required]
        public string PictureUrl { get; set; } // – string (required)

        [Required]
        public string PlateNumber { get; set; } //– a string – Must be a valid Plate number (2 Capital English letters, followed by 4 digits, followed by 2 Capital English letters (required)

        [Required]
        public string OwnerId { get; set; }

        public User Owner { get; set; }

        public virtual ICollection<Issue> Issues { get; set; } = new HashSet<Issue>();

    }
}           