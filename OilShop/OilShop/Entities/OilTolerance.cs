using System.Collections.Generic;

namespace OilShop.Entities
{
    public class OilTolerance
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ToleranceOil> ToleranceOils { get; set; }
    }
}
