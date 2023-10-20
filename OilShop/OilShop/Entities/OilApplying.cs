using System.Collections.Generic;

namespace OilShop.Entities
{
    public class OilApplying
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Oil> Oils { get; set; }
    }
}
