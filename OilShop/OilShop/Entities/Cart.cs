using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OilShop.Entities
{
    public class Cart
    {
        [Key]
        public long Id { get; set; }
        [ForeignKey("Customer")]
        public long CustomerId { get; set; }
        public DbUser Customer { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
    }
}
