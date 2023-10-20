using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OilShop.Entities;
using OilShop.Models;
using OilShop.Repo.Implement;
using OilShop.Services.Implement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OilShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOilService _oilService;
        private readonly IOrderRepo _orderRepo;
        private readonly IOrderStatusRepo _orderStatusRepo;
        private readonly IOilRepo _oilRepo;
        private readonly ICartRepo _cartRepo;
        private readonly ICartItemRepo _cartItemRepo;
        private readonly UserManager<DbUser> _userManager;


        public OrderController(IOilService oilService, IOrderRepo orderRepo, UserManager<DbUser> userManager, ICartRepo cartRepo, ICartItemRepo cartItemRepo, IOilRepo oilRepo, IOrderStatusRepo orderStatusRepo)
        {
            _oilService = oilService;
            _orderRepo = orderRepo;
            _userManager = userManager;
            _cartRepo = cartRepo;
            _cartItemRepo = cartItemRepo;
            _oilRepo = oilRepo;
            _orderStatusRepo = orderStatusRepo;
        }

        [HttpGet]
        [Route("/checkout")]
        public IActionResult Checkout(CartViewModel[] models)
        {
            if (models.Length != 0)
            {
                int size = 0, counter = 0;

                foreach (var item in models)
                {
                    if(item.MaxCount<item.Count|| item.Count > 20)
                    {
                        return StatusCode(500);
                    }
                    else if(item.Count > 0)
                    {
                        size += 1;
                    }
                }
                if (size > 0)
                {

                    foreach (var item in _oilService.GetAllOils())
                    {
                        for (int i = 0; i < models.Length; i++)
                        {
                            if (models[i].Count > 0)
                            {
                                if (item.Id == models[i].Id)
                                {
                                    models[i].OilName = item.Name;
                                    models[i].Price = item.Price;
                                    models[i].Capacity = item.Capacity;
                                }
                            }
                        }
                    }

                    OrderViewModel CartItems = new OrderViewModel();
                    CartItems.Items = new CartViewModel[size];
                    for (int i = 0; i < models.Length; i++)
                    {
                        if (models[i].Count > 0)
                        {
                            CartItems.Items[counter] = models[i];
                            counter += 1;
                        }
                    }

                    return View(CartItems);
                }
                else
                {
                    return StatusCode(500);
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("/checkout")]
        public IActionResult Checkout(OrderViewModel model)
        {
            if (ModelState.IsValid)
            {

                foreach (var item in model.Items)
                {
                    if (item.MaxCount < item.Count || item.Count > 20)
                    {
                        return StatusCode(500);
                    }
                }

                if (model.Items.Length > 0)
                {
                    var Order = new Order
                    {
                        Address = model.Address,
                        City = model.City,
                        CustomerId = _userManager.FindByEmailAsync(model.Email).Result.Id,
                        Reciever = model.RecieverName,
                        PhoneNumber = model.PhoneNumber,
                        OrderStatusId = _orderStatusRepo.GetAll().FirstOrDefault(x=>x.Name.Equals("В обробці")).Id,
                        OrderDate = DateTime.UtcNow.ToString("dd-MM-yyyy"),
                        OrderLines = new List<OrderDetail>()
                    };
                    foreach (var item in model.Items)
                    {
                        Order.OrderLines.Add(new OrderDetail
                        {
                            Amount = item.Count,
                            Price = item.Price,
                            OilId = item.Id,
                            OrderId = Order.Id
                        });
                        var tempoil = _oilRepo.GetAll().FirstOrDefault(x => x.Id == item.Id);
                        tempoil.Count = tempoil.Count- item.Count;
                        _oilRepo.Update(tempoil);
                    }

                    _orderRepo.Add(Order);
                    bool InCart = true;
                    foreach (var item in model.Items)
                    {
                        if (_cartItemRepo.GetAll().FirstOrDefault(x => x.OilId == item.Id
                            && x.CartId == _cartRepo.GetAll().FirstOrDefault(x => x.CustomerId == _userManager.FindByEmailAsync(model.Email).Result.Id).Id) == null)
                        {
                            InCart = false;
                        }
                    }
                    if (InCart == true)
                    {
                        foreach (var item in model.Items)
                        {
                            _cartItemRepo.Delete(_cartItemRepo.GetAll().FirstOrDefault(x => x.OilId == item.Id
                            && x.CartId == _cartRepo.GetAll().FirstOrDefault(x => x.CustomerId == _userManager.FindByEmailAsync(model.Email).Result.Id).Id));
                        }
                    }
                    return RedirectToAction("orders", "account");
                }
                else
                {
                    return StatusCode(500);
                }

            }

            if (model.CardsExpiredMonth != null)
            {
                try
                {
                    if (Convert.ToInt32(model.CardsExpiredMonth) > 12 || Convert.ToInt32(model.CardsExpiredMonth) < 0)
                    {
                        ModelState.AddModelError("CardsExpiredMonth", "Число повинне бути в межах від 0 до 12");
                    }
                }
                catch {           ModelState.AddModelError("CardsExpiredMonth", "Число повинне бути цілим");}
                if (new string[] { ".", "," }.Any(s => model.CardsExpiredMonth.Contains(s)))
                {
                    ModelState.AddModelError("CardsExpiredMonth", "Число повинне бути цілим");
                }
            }
            if (model.CardsExpiredYear != null)
            {
                if (new string[] { ".", "," }.Any(s => model.CardsExpiredYear.Contains(s)))
                {
                    ModelState.AddModelError("CardsExpiredYear", "Число повинне бути цілим");
                }
            }
            return View(model);
        }

        public IActionResult RepeatOrder(OrderViewModel model)
        {
            TempData["Again"] = "true";
            return View("checkout", model);
        }
    }
}

