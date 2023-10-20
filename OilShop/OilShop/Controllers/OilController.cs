using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OilShop.Entities;
using OilShop.Services.Implement;

namespace OilShop.Controllers
{
    public class OilController : Controller
    {
        private readonly ILogger<OilController> _logger;
        private readonly IOilService _oilService;
        private readonly IWebHostEnvironment _env;
        private readonly ICartService _cartService;

        public OilController(ILogger<OilController> logger,
            IOilService oilService, IWebHostEnvironment env, ApplicationDbContext context, ICartService cartService)
        {
            _logger = logger;
            _oilService = oilService;
            _env = env;
            _cartService = cartService;
        }

        public IActionResult Index(string SearchData = "", int Page = 1)
        {
            var model = _oilService.GetOils(Page, SearchData);
            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int Id, string Email)
        {
            if (Email != null)
            {
                ViewBag.IsInCart = _cartService.IsInCartAsync(Id, Email).Result;
            }

            var model = _oilService.GetOilFullInfoById(Id);
            if (!model.PhotoBase64.Contains("https"))
            {
                model.PhotoBase64 = "/" + model.PhotoBase64;
            }
            return View(model);
        }
    }
}
