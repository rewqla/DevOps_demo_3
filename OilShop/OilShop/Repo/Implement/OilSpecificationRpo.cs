using OilShop.Entities;
using OilShop.Repo.Implement;

namespace OilShop.Repo.Interfaces
{
    public class OilSpecificationRepo : GenericRepository<OilSpecification>, IOilSpecificationRepo
    {
        public OilSpecificationRepo(ApplicationDbContext context) : base(context)
        {

        }
    }
}

