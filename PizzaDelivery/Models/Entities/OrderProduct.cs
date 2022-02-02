using System;
using System.Collections.Generic;

namespace PizzaDelivery.Models.Entities
{
    public partial class OrderProduct
    {
        public uint Id { get; set; }
        public uint OrderId { get; set; }
        public uint ProductId { get; set; }

        public virtual Order Order { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
