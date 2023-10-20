using OilShop.Entities;
using OilShop.Repo.Implement;

namespace OilShop.Repo.Interfaces
{
    public class OrderStatusRepo : GenericRepository<OrderStatus>, IOrderStatusRepo
    {
        public OrderStatusRepo(ApplicationDbContext context) : base(context)
        {

        }
    }
}

