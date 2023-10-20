using System.Collections.Generic;

namespace OilShop.Entities
{
    public class OilSpecification
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<SpecificationOil> SpecificationOils { get; set; }

    }
}
