using OilShop.Entities;
using OilShop.Repo.Implement;

namespace OilShop.Repo.Interfaces
{
    public class OrderDetailRepo : GenericRepository<OrderDetail>, IOrderDetailRepo
    {
        public OrderDetailRepo(ApplicationDbContext context) : base(context)
        {

        }
    }
}

