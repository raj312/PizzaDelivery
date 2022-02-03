using System;
using System.Collections.Generic;

namespace PizzaDelivery.Models.Entities
{
    public partial class Product
    {
        public uint Id { get; set; }
        public string? Name { get; set; }
        public string? Desc { get; set; }
        public double? Total { get; set; }
    }
}
