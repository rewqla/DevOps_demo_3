using System.Collections.Generic;

namespace OilShop.Entities
{
    public class OrderStatus
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
