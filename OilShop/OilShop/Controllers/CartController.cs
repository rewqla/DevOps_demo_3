using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OilShop.Entities;
using OilShop.Services.Implement;
using System.Threading.Tasks;

namespace OilShop.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly UserManager<DbUser> _userManager;
        private readonly SignInManager<DbUser> _signInManager;
        public CartController(ICartService cartService, UserManager<DbUser> userManager, SignInManager<DbUser> signInManager)
        {
            _cartService = cartService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Route("/shopcart")]
        public async Task<IActionResult> Index(string Email)
        {
            if (Email == null)
            {
                return RedirectToAction("login", "account");
            }
            else
            {
                var user = await _userManager.FindByEmailAsync(Email);
                await _signInManager.SignInAsync(user, isPersistent: false);

                var model = _cartService.CustomerCart(user.Id);
                return View(model);
            }
        }
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> AddToCart(int OilId, string Email)
        {
            if (Email == null)
            {
                return RedirectToAction("login", "account");
            }
            else
            {
                var user = await _userManager.FindByEmailAsync(Email);

                _cartService.AddItem(OilId, user.Id);
                return RedirectToAction("details", "oil",new { Id = OilId,Email=Email });
            }

        }
        [IgnoreAntiforgeryToken]
        public ActionResult DeleteFromCart(int OilId)
        {
            _cartService.DeleteItem(OilId, _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)).Result.Id);
            return RedirectToAction("index", "cart", new { Email = _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)).Result.Email });
        }
    }
}
