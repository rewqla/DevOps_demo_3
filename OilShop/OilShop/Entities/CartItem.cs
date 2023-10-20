using System.ComponentModel.DataAnnotations.Schema;

namespace OilShop.Entities
{
    public class CartItem
    {
        public long Id { get; set; }
        [ForeignKey("Cart")]
        public long CartId { get; set; }
        public Cart Cart { get; set; }
        [ForeignKey("Oil")]
        public long OilId { get; set; }
        public Oil Oil { get; set; }
    }
}