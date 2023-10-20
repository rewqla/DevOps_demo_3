using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OilShop.Entities;
using OilShop.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OilShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<DbUser> _userManager;
        private readonly RoleManager<DbRole> _roleManager;

        public UsersController(UserManager<DbUser> userManager, RoleManager<DbRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Registration()
        {
            ViewBag.Roles = _roleManager.Roles.Select(x => x.Name).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(RegistrationViewModel model, string rolename)
        {
            if (ModelState.IsValid)
            {
                bool isEmailExist = await _userManager.FindByEmailAsync(model.Email) != null;
                bool isPhoneExist = _userManager.Users.FirstOrDefault(x => x.PhoneNumber.Equals(model.PhoneNumber)) != null;

                if (isEmailExist)
                {
                    ModelState.AddModelError("Email", "Дана пошта вже використовується");
                    ViewBag.Roles = _roleManager.Roles.Select(x => x.Name).ToList();
                    return View(model);
                }
                if (isPhoneExist)
                {
                    ModelState.AddModelError("PhoneNumber", "Даний номер телефону вже використовується");
                    ViewBag.Roles = _roleManager.Roles.Select(x => x.Name).ToList();
                    return View(model);
                }

                var user = new DbUser
                {
                    Email = model.Email,
                    UserName = model.Email,
                    FullName = model.FullName,
                    PhoneNumber = model.PhoneNumber,
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if (rolename == "User")
                    {
                        await _userManager.AddToRoleAsync(user, "User");
                    }
                    if (rolename == "Admin")
                    {
                        await _userManager.AddToRoleAsync(user, "Admin");
                    }
                    return RedirectToAction("index", "home");
                }
                return StatusCode(500);
            }
            ViewBag.Roles = _roleManager.Roles.Select(x => x.Name).ToList();
            return View(model);
        }
    }
}
