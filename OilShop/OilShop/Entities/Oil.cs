using System.Collections.Generic;

namespace OilShop.Entities
{
    public class Oil
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public float Price { get; set; }
        public string Image { get; set; }
        public string Manafacturer { get; set; }
        public long OilManafacturerId { get; set; }
        public OilManafacturer OilManafacturer { get; set; }
        public long OilApplyingId { get; set; }
        public OilApplying OilApplying { get; set; }
        public long OilTypeId { get; set; }
        public OilType OilType { get; set; }
        public long OilCapacityId { get; set; }
        public OilCapacity OilCapacity { get; set; }
        public ICollection<RecommendationOil> RecommendationOils { get; set; }
        public ICollection<ToleranceOil> ToleranceOils { get; set; }
        public ICollection<SpecificationOil> SpecificationOils { get; set; }

    }
}
