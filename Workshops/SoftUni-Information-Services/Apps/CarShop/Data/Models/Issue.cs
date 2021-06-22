using System;
using System.ComponentModel.DataAnnotations;

namespace CarShop.Data.Models
{
    public class Issue
    {
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Description { get; set; } //– a string with min length 5 (required)

        public bool IsFixed { get; set; } //– a bool indicating if the issue has been fixed or not (required)

        [Required]
        public string CarId { get; set; }

        public Car Car { get; set; }
    }
}