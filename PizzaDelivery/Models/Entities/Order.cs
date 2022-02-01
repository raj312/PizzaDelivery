using System;
using System.Collections.Generic;

namespace PizzaDelivery.Models.Entities
{
    public partial class Order
    {
        public uint Id { get; set; }
        public double Amount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public uint? StoreId { get; set; }
    }
}
