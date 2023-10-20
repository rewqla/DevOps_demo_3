using OilShop.Entities;
using OilShop.Repo.Implement;

namespace OilShop.Repo.Interfaces
{
    public class CartItemRepo : GenericRepository<CartItem>, ICartItemRepo
    {
        public CartItemRepo(ApplicationDbContext context) : base(context)
        {

        }
    }
}

