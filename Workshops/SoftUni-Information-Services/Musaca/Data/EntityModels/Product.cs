using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Musaca.Data.EntityModels
{
    public class Product
    {
        public Product()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Orders = new HashSet<Order>();
        }

        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public byte Barcode { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}