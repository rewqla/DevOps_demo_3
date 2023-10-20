using System.Collections.Generic;

namespace OilShop.Models
{
    public class OrderViewModel
    {
        public long Id { get; set; }
        public string RecieverName { get; set; }
        public string Status { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string DeclarationNumber { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string CardsName { get; set; }
        public string Date { get; set; }
        public string CardsNumber { get; set; }
        public string CardsExpiredMonth { get; set; }
        public string CardsExpiredYear { get; set; }
        public string CVV { get; set; }
        public CartViewModel[] Items { get; set; }
    }

    public class OrderListViewModel
    {
        public long Id { get; set; }
        public string Status { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Date { get; set; }
        public List<OrderDetailsList> Details { get; set; }
    }

    public class OrderDetailsList
    {
        public string Image { get; set; }
        public long Id { get; set; }
        public string OilName { get; set; }
        public int Capacity { get; set; }
        public float Price { get; set; }
        public int Count { get; set; }
        public int CountInStock { get; set; }
    }
}
