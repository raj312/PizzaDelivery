﻿using System;
using System.Collections.Generic;

namespace PizzaDelivery.Models.Entities
{
    public partial class Order
    {
        public Order()
        {
            OrderProducts = new HashSet<OrderProduct>();
        }

        public uint Id { get; set; }
        public double Amount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public uint? StoreId { get; set; }

        public virtual Store? Store { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
