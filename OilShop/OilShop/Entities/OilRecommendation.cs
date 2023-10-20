using System.Collections.Generic;

namespace OilShop.Entities
{
    public class OilRecommendation
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<RecommendationOil> RecommendationOils { get; set; }
    }
}
