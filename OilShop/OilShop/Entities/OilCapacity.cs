using System.Collections.Generic;

namespace OilShop.Entities
{
    public class OilCapacity
    {
        public long Id { get; set; }
        public int Capacity { get; set; }
        public IEnumerable<Oil> Oils { get; set; }
    }
}
