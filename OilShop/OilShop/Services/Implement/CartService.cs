using Microsoft.AspNetCore.Identity;
using OilShop.Entities;
using OilShop.Models;
using OilShop.Repo.Implement;
using OilShop.Services.Implement;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OilShop.Services.Interfaces
{

    public class CartService : ICartService
    {
        private readonly ICartRepo _cartRepo;
        private readonly IOilRepo _oilRepo;
        private readonly ICartItemRepo _cartItemRepo;
        private readonly IOilCapacityRepo _oilCapacityRepo;
        private readonly UserManager<DbUser> _userManager;

        public CartService(ICartRepo cartRepo, ICartItemRepo cartItemRepo, IOilRepo oilRepo, IOilCapacityRepo oilCapacityRepo, UserManager<DbUser> userManager)
        {
            _cartRepo = cartRepo;
            _cartItemRepo = cartItemRepo;
            _oilRepo = oilRepo;
            _oilCapacityRepo = oilCapacityRepo;
            _userManager = userManager;
        }

        public void AddItem(long OilId, long CustomerId)
        {
            var cart = _cartRepo.GetAll().FirstOrDefault(x => x.CustomerId == CustomerId);
            foreach (var item in _cartItemRepo.GetAll().Where(x => x.CartId == cart.Id))
            {
                if (item.OilId == OilId)
                {
                    return;
                }
            }
            _cartItemRepo.Add(new CartItem { CartId = cart.Id, OilId = OilId });
        }

        public void CreateCart(long CustomerId)
        {
            _cartRepo.Add(new Cart { CustomerId = CustomerId });
        }

        public ICollection<CartViewModel> CustomerCart(long CustomerId)
        {
            var cart = _cartRepo.GetAll().FirstOrDefault(x => x.CustomerId == CustomerId);
            var cartItems = _cartItemRepo.GetAll().Where(x => x.CartId == cart.Id);
            if (cartItems.Count() != 0)
            {
                ICollection<CartViewModel> cartViewModel = new List<CartViewModel>();

                foreach (var item in cartItems)
                {
                    var tempOil = _oilRepo.GetAll().FirstOrDefault(x => x.Id == item.OilId);
                    cartViewModel.Add(new CartViewModel
                    {
                        OilName = tempOil.Name,
                        Id = tempOil.Id,
                        Image = tempOil.Image,
                        Count = 1,
                        MaxCount=tempOil.Count,
                        Capacity = _oilCapacityRepo.GetAll().FirstOrDefault(x => x.Id == tempOil.OilCapacityId).Capacity,
                        Price = tempOil.Price
                    });
                }
                return cartViewModel;
            }
            return null;
        }

        public void DeleteItem(long OilId, long CustomerId)
        {
            var cart = _cartRepo.GetAll().FirstOrDefault(x => x.CustomerId == CustomerId);
            _cartItemRepo.Delete(_cartItemRepo.GetAll().FirstOrDefault(x => x.CartId == cart.Id && x.OilId == OilId));
        }

        public async Task<bool> IsInCartAsync(long OilId, string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);

            var cart = _cartRepo.GetAll().FirstOrDefault(x => x.CustomerId == user.Id);
            foreach (var item in _cartItemRepo.GetAll().Where(x => x.CartId == cart.Id))
            {
                if (item.OilId == OilId)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
