using OilShop.Entities;
using OilShop.Repo.Implement;

namespace OilShop.Repo.Interfaces
{
    public class OilCapacityRepo : GenericRepository<OilCapacity>, IOilCapacityRepo
    {
        public OilCapacityRepo(ApplicationDbContext context) : base(context)
        {

        }
    }
}

