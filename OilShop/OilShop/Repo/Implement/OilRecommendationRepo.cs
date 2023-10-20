using OilShop.Entities;
using OilShop.Repo.Implement;

namespace OilShop.Repo.Interfaces
{
    public class OilRecommendationRepo : GenericRepository<OilRecommendation>, IOilRecommendationRepo
    {
        public OilRecommendationRepo(ApplicationDbContext context) : base(context)
        {

        }
    }
}

