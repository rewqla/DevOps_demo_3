using OilShop.Entities;
using OilShop.Repo.Implement;

namespace OilShop.Repo.Interfaces
{
    public class OilManafacturerRepo : GenericRepository<OilManafacturer>, IOilManafacturerRepo
    {
        public OilManafacturerRepo(ApplicationDbContext context) : base(context)
        {

        }
    }
}

