using OilShop.Entities;
using OilShop.Repo.Implement;

namespace OilShop.Repo.Interfaces
{
    public class OilToleranceRepo : GenericRepository<OilTolerance>, IOilToleranceRepo
    {
        public OilToleranceRepo(ApplicationDbContext context) : base(context)
        {

        }
    }
}

