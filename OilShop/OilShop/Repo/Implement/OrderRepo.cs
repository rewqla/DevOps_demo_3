using OilShop.Entities;
using OilShop.Repo.Implement;

namespace OilShop.Repo.Interfaces
{
    public class OrderRepo : GenericRepository<Order>, IOrderRepo
    {
        public OrderRepo(ApplicationDbContext context) : base(context)
        {

        }
    }
}

