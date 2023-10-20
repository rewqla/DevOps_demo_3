using System.ComponentModel.DataAnnotations.Schema;

namespace OilShop.Entities
{
    public class OrderDetail
    {
        public long Id { get; set; }
        [ForeignKey("Order")]
        public long OrderId { get; set; }
        [ForeignKey("Oil")]
        public long OilId { get; set; }
        public int Amount { get; set; }
        public float Price { get; set; }
        public virtual Oil Oil { get; set; }
        public virtual Order Order { get; set; }
    }
}