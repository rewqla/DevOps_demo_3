using OilShop.Entities;
using OilShop.Repo.Implement;

namespace OilShop.Repo.Interfaces
{
    public class OilApplyinRepo : GenericRepository<OilApplying>, IOilApplyingRepo
    {
        public OilApplyinRepo(ApplicationDbContext context) : base(context)
        {

        }
    }
}

