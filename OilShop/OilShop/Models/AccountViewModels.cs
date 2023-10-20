namespace OilShop.Models
{
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegistrationViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class PersonalInfoViewModel
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
        public string confirmPassword { get; set; }
    }

    public class CartViewModel
    {
        public long Id { get; set; }
        public string OilName { get; set; }
        public int Capacity { get; set; }
        public int Count { get; set; }
        public int MaxCount { get; set; }
        public string Image { get; set; }
        public float Price { get; set; }
    }
}
