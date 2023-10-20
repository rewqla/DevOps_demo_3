using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OilShop.Entities
{
    public class Order
    {
        public long Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Reciever { get; set; }
        public string Address { get; set; }
        public string OrderDate { get; set; }
        public long OrderStatusId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string City { get; set; }
        public string DeclarationNumber { get; set; }

        [ForeignKey("Customer)")]
        public long CustomerId { get; set; }
        public DbUser Customer { get; set; }
        public virtual ICollection<OrderDetail> OrderLines { get; set; }
    }
}
