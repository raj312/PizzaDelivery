using System;
using System.Collections.Generic;

namespace PizzaDelivery.Models.Entities
{
    public partial class Store
    {
        public Store()
        {
            Orders = new HashSet<Order>();
        }

        public uint Id { get; set; }
        public string? StoreAddress { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
