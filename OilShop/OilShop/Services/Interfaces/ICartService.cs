using OilShop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OilShop.Services.Implement
{
    public interface ICartService
    {
        void AddItem(long OilId, long CustomerId);
        void DeleteItem(long OilId, long CustomerId);
        void CreateCart(long CustomerId);
        Task<bool> IsInCartAsync(long OilId, string Email);
        ICollection<CartViewModel> CustomerCart(long CustomerIdl);
    }

}
