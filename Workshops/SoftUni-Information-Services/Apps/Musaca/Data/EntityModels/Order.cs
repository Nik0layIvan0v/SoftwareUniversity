using System;
using System.ComponentModel.DataAnnotations;
using Musaca.Data.Enums;

namespace Musaca.Data.EntityModels
{
    public class Order
    {
        public Order()
        {
           this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string Id { get; set; }

        public Status Status { get; set; }

        [Required]
        public string ProductId { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }

        [Required]
        public string CashierId { get; set; }

        public User Cashier { get; set; }
    }
}