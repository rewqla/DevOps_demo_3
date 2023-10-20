namespace OilShop.Entities
{
    public class RecommendationOil
    {
        public long OilId { get; set; }
        public long RecommendationId { get; set; }
        public Oil Oil { get; set; }
        public OilRecommendation Recommendation { get; set; }
    }
}
