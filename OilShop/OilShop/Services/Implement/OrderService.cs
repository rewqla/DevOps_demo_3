using Microsoft.AspNetCore.Identity;
using OilShop.Entities;
using OilShop.Models;
using OilShop.Repo.Implement;
using OilShop.Services.Implement;
using System.Collections.Generic;
using System.Linq;

namespace OilShop.Services.Interfaces
{

    public class OrderService : IOrderService
    {
        private readonly IOrderStatusRepo _orderStatusRepo;
        private readonly IOrderRepo _orderRepo;
        private readonly IOrderDetailRepo _orderDetailRepo;
        private readonly IOilRepo _oilRepo;
        private readonly IOilCapacityRepo _oilCapacityRepo;
        private readonly UserManager<DbUser> _userManager;


        public OrderService(IOrderStatusRepo orderStatusRepo, IOrderRepo orderRepo, IOilRepo oilRepo, IOrderDetailRepo orderDetailRepo, UserManager<DbUser> userManager, IOilCapacityRepo oilCapacityRepo)
        {
            _orderStatusRepo = orderStatusRepo;
            _orderRepo = orderRepo;
            _oilRepo = oilRepo;
            _orderDetailRepo = orderDetailRepo;
            _userManager = userManager;
            _oilCapacityRepo = oilCapacityRepo;
        }
        public List<OrderDetailsList> GetDetails(long OrderId)
        {
            List<OrderDetailsList> detailsLists = new List<OrderDetailsList>();
            foreach (var item in _orderDetailRepo.GetAll().Where(x=>x.OrderId==OrderId))
            {
                detailsLists.Add(new OrderDetailsList()
                {
                    Price=item.Price,
                    Id=item.OilId,
                    Count=item.Amount,
                    CountInStock= _oilRepo.GetAll().FirstOrDefault(x => x.Id == item.OilId).Count,
                    OilName=_oilRepo.GetAll().FirstOrDefault(x=>x.Id==item.OilId).Name,
                    Image=_oilRepo.GetAll().FirstOrDefault(x=>x.Id==item.OilId).Image,
                    Capacity= _oilCapacityRepo.GetAll().FirstOrDefault(x => x.Id == _oilRepo.GetAll().FirstOrDefault(x => x.Id == item.OilId).OilCapacityId).Capacity
                });
            }
            return detailsLists;
        }
        public List<OrderListViewModel> ConvertToOrderList(IEnumerable<Order> Orders)
        {
            List<OrderListViewModel> converted = new List<OrderListViewModel>();
            foreach (var item in Orders)
            {
                converted.Add(new OrderListViewModel()
                {
                    Id = item.Id,
                    Date = item.OrderDate,
                    Phone = item.PhoneNumber,
                    Email = _userManager.FindByIdAsync(item.CustomerId.ToString()).Result.Email,
                    Status = _orderStatusRepo.GetAll().FirstOrDefault(x => x.Id == item.OrderStatusId).Name,
                    Details= GetDetails(item.Id)
                });
            }
            return converted;
        }

        public List<OrderListViewModel> GetAllByUsId(long CustomerId)
        {
            var orders = _orderRepo.GetAll().Where(x => x.CustomerId == CustomerId);
            return ConvertToOrderList(orders);
        }

        public List<OrderListViewModel> GetAllByStatus(string Status)
        {
            var orders = _orderRepo.GetAll().Where(x => x.OrderStatusId == _orderStatusRepo.GetAll().FirstOrDefault(x=>x.Name.Equals(Status)).Id);
            return ConvertToOrderList(orders);
        }

        public OrderViewModel GetById(long Id)
        {
            var order = _orderRepo.GetAll().FirstOrDefault(x => x.Id == Id);
            var details = _orderDetailRepo.GetAll().Where(x => x.OrderId == Id).ToArray();
            var model = new OrderViewModel()
            {
                Id = Id,
                PhoneNumber = order.PhoneNumber,
                RecieverName = order.Reciever,
                Date = order.OrderDate,
                Address = order.Address,
                DeclarationNumber = order.DeclarationNumber,
                Status = _orderStatusRepo.GetAll().FirstOrDefault(x => x.Id == order.OrderStatusId).Name,
                City = order.City,
                Email = _userManager.FindByIdAsync(order.CustomerId.ToString()).Result.Email,
                Items = new CartViewModel[details.Count()]
            };
            for (int i = 0; i < details.Count(); i++)
            {
                model.Items[i] = new CartViewModel();
                var tempoil = _oilRepo.GetAll().FirstOrDefault(x => x.Id == details[i].OilId);
                model.Items[i].Count = details[i].Amount;
                model.Items[i].MaxCount = tempoil.Count;
                model.Items[i].Id = details[i].OilId;
                model.Items[i].Capacity = _oilCapacityRepo.GetAll().FirstOrDefault(x => x.Id == tempoil.OilCapacityId).Capacity;
                if (!tempoil.Image.Contains("https"))
                {
                    model.Items[i].Image = "/" + tempoil.Image;
                }
                else
                {
                    model.Items[i].Image = tempoil.Image;
                }
                model.Items[i].OilName = tempoil.Name;
                model.Items[i].Price = details[i].Price;
            }
            return model;
        }
    }
}
