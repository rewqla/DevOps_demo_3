using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OilShop.Repo.Implement;
using OilShop.Services.Implement;
using System.Linq;

namespace OilShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IOrderStatusRepo _orderStatusRepo;
        private readonly IOrderRepo _orderRepo;

        public OrdersController(IOrderService orderService, IOrderStatusRepo orderStatusRepo, IOrderRepo orderRepo)
        {
            _orderService = orderService;
            _orderStatusRepo = orderStatusRepo;
            _orderRepo = orderRepo;
        }

        public IActionResult Index(string Status)
        {
            if (Status != null)
            {
                var model = _orderService.GetAllByStatus(Status);

                ViewBag.Status = Status;

                return View(model);
            }
            return StatusCode(500);
        }

        [HttpGet]
        public IActionResult Info(long Id)
        {
            var model = _orderService.GetById(Id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Info(long OrderId, string DeclarationNumber)
        {
            if (_orderRepo.GetAll().FirstOrDefault(x => x.DeclarationNumber == DeclarationNumber) == null)
            {
                var order = _orderRepo.FindById(OrderId);
                order.DeclarationNumber = DeclarationNumber;
                order.OrderStatusId = _orderStatusRepo.GetAll().FirstOrDefault(x => x.Name == "Відправлено").Id;
                _orderRepo.Update(order);

                TempData["message"] = "Замовлення №" + OrderId + " успішно змінине";
                return Redirect("/admin/orders?Status=Відправлено");
            }
            var model = _orderService.GetById(OrderId);

            TempData["message"] = "Декларація №" + DeclarationNumber + " же використовється!";
            return View(model);
        }

    }
}
