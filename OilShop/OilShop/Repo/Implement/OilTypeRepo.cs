using OilShop.Entities;
using OilShop.Repo.Implement;

namespace OilShop.Repo.Interfaces
{
    public class OilTypeRepo : GenericRepository<OilType>, IOilTypeRepo
    {
        public OilTypeRepo(ApplicationDbContext context) : base(context)
        {

        }
    }
}

