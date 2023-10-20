namespace OilShop.Entities
{
    public class ToleranceOil
    {
        public long OilId { get; set; }
        public long ToleranceId { get; set; }
        public Oil Oil { get; set; }
        public OilTolerance Tolerance { get; set; }
    }
}
