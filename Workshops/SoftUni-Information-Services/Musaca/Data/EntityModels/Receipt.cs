using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Musaca.Data.EntityModels
{
    public class Receipt
    {
        public Receipt()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Orders = new HashSet<Order>();
        }

        [Required]
        public string Id { get; set; }

        public DateTime IssuedOn { get; set; }

        [Required]
        public string CashierId { get; set; }

        public User Cashier { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}