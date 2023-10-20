using OilShop.Entities;
using OilShop.Models;
using System.Collections.Generic;

namespace OilShop.Services.Implement
{
    public interface IOrderService
    {
        List<OrderListViewModel> GetAllByUsId(long CustomerId);
        OrderViewModel GetById(long Id);
        List<OrderListViewModel> GetAllByStatus(string Status);
        List<OrderListViewModel> ConvertToOrderList(IEnumerable<Order> Orders);
        List<OrderDetailsList> GetDetails(long OrderId);
    }

}
