using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OilShop.Entities;
using OilShop.Helpers;
using OilShop.Models;
using OilShop.Repo.Implement;
using OilShop.Services.Implement;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace OilShop.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<DbUser> _userManager;
        private readonly SignInManager<DbUser> _signInManager;
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        private readonly IWebHostEnvironment _env;

        public AccountController(UserManager<DbUser> userManager,
            SignInManager<DbUser> signInManager,
            IWebHostEnvironment env, ICartService cartService, IOrderService orderService, IOrderDetailRepo orderDetailRepo)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _env = env;
            _cartService = cartService;
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            var currentUser = await _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User));
            if (currentUser == null)
            {
                return View();
            }
            return StatusCode(403);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("index", "home");
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "Невірний логін або пароль");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "Такої пошти не існує");
                }
            }
            return View(model);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        [HttpGet]
        public async Task<IActionResult> Registration()
        {
            var currentUser = await _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User));
            if (currentUser == null)
            {
                return View();
            }
            return StatusCode(403);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Registration(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool isEmailExist = await _userManager.FindByEmailAsync(model.Email) != null;
                bool isPhoneExist = _userManager.Users.FirstOrDefault(x => x.PhoneNumber.Equals(model.PhoneNumber)) != null;
                if (isEmailExist)
                {
                    ModelState.AddModelError("Email", "Дана пошта вже використовується");
                    return View(model);
                }
                if (isPhoneExist)
                {
                    ModelState.AddModelError("PhoneNumber", "Даний номер телефону вже використовується");
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
                    await _userManager.AddToRoleAsync(user, "User");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _cartService.CreateCart(Convert.ToInt64(user.Id));

                    return RedirectToAction("index", "home");
                }
                return StatusCode(500);
            }
            return View(model);
        }


        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> ChangeUserInfo(PersonalInfoViewModel model)
        {
            var user = await _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User));
            user.FullName = model.FullName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            IdentityResult result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Redirect("/account/personal-info");
            }
            return StatusCode(500);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]

        public async Task<IActionResult> RequestPasswordReset(PersonalInfoViewModel model)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var result = await _userManager.ChangePasswordAsync(user, model.oldPassword, model.newPassword);
            if (!result.Succeeded)
            {
                TempData["message"] = "Щось пішло не так. Можливо ваш поточний пароль був введений неправильно";
            }
            else
            {
                TempData["message"] = "Пароль успішно змінений";
            }
            return Redirect("/account/personal-info");
        }

        [HttpGet]
        public async Task<ActionResult> ForgotPassword()
        {
            var currentUser = await _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User));
            if (currentUser == null)
            {
                return View();
            }
            return StatusCode(403);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]

        public async Task<ActionResult> ForgotPassword(string Email)
        {
            if (Email != null)
            {
                var getUser = await _userManager.FindByEmailAsync(Email);
                if (getUser != null)
                {
                    string newPassword = new PasswordHelper().GenerateRandomPassword();

                    var token = await _userManager.GeneratePasswordResetTokenAsync(getUser);

                    var result = await _userManager.ResetPasswordAsync(getUser, token, newPassword);
                    if (result.Succeeded)
                    {
                        var subject = "Новий пароль";
                        var body = "Привіт " + getUser.FullName + ", <br/> Oсь твій новий пароль від акауунту " + newPassword +
                            "<br/>Змінити його можна в настройках твого акаунту<br/>" +

                             "Якщо ти не просив запит на новий пароль, то ігноруй цей лист або повідом нас<br/><br/> Дякую тобі";

                        SendEmail(getUser.Email, body, subject);

                        ViewBag.Message = "Новий пароль висланий на вашу пошту";
                    }
                    else
                    {
                        return StatusCode(500);
                    }
                }
                else
                {
                    ViewBag.Message = "Користувача з такою поштою не існує";
                    return View();
                }
            }
            else
            {
                ViewBag.Message = "Поле має бути заповненим";
                return View();
            }

            return View();
        }

        private void SendEmail(string emailAddress, string body, string subject)
        {
            using (MailMessage mm = new MailMessage("sde6319@gmail.com", emailAddress))
            {
                mm.Subject = subject;
                mm.Body = body;

                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("sde6319@gmail.com", "Rewqla13?");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Orders()
        {
            var currentUser = await _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User));

            if (currentUser != null)
            {
                var model = _orderService.GetAllByUsId(currentUser.Id);
                return View(model);
            }
            return RedirectToAction("login", "account");
        }

        [HttpGet]
        [Route("account/personal-info")]
        public async Task<IActionResult> PersonalInfo()
        {
            var currentUser = await _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User));
            if (currentUser != null)
            {
                var user = new PersonalInfoViewModel
                {
                    FullName = currentUser.FullName,
                    Email = currentUser.Email,
                    PhoneNumber = currentUser.PhoneNumber
                };

                return View(user);
            }
            else
            {
                return RedirectToAction("login", "account");
            }
        }

        [HttpGet]
        [Route("orders/{Id}")]
        public IActionResult OrderInfo(long Id)
        {
            var model = _orderService.GetById(Id);
            if (model.Email == _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User)).Result.Email)
            {
                return View(model);
            }
            return StatusCode(403);
        }
    }
}
