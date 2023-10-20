using OilShop.Entities;
using OilShop.Repo.Implement;

namespace OilShop.Repo.Interfaces
{
    public class CartRepo : GenericRepository<Cart>, ICartRepo
    {
        public CartRepo(ApplicationDbContext context) : base(context)
        {

        }
    }
}

