namespace OilShop.Entities
{
    public class SpecificationOil
    {
        public long OilId { get; set; }
        public long SpecificationId { get; set; }
        public Oil Oil { get; set; }
        public OilSpecification Specification { get; set; }
    }
}
