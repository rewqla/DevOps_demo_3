using System.Collections.Generic;

namespace OilShop.Entities
{
    public class OilManafacturer
    {
        public long Id { get; set; }
        public string Country { get; set; }
        public IEnumerable<Oil> Oils { get; set; }
    }
}
