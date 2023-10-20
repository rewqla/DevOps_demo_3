using FluentValidation;
using OilShop.Models;
using System;

namespace OilShop
{
    public class CheckoutValidation : AbstractValidator<OrderViewModel>
    {
        public CheckoutValidation()
        {
            RuleFor(x => x.RecieverName).NotEmpty().WithMessage("Поле має бути заповненим");

            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Поле має бути заповненим")
            .Length(13).WithMessage("Номер телефону має складатись з 13 символів")
            .Matches(@"^\+?3?8?(0\d{9})$").WithMessage("Неправильний номер телефону");

            RuleFor(x => x.City).NotEmpty().WithMessage("Поле має бути заповненим");

            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Поле має бути заповненим");

            RuleFor(x => x.Address).NotEmpty().WithMessage("Поле має бути заповненим");

            RuleFor(x => x.CardsName).NotEmpty().WithMessage("Поле має бути заповненим");

            RuleFor(x => x.CardsNumber).NotEmpty().WithMessage("Поле має бути заповненим")
                .CreditCard().WithMessage("Неіснуючий номер банківської карти");

            RuleFor(x => x.CVV).NotEmpty().WithMessage("Поле має бути заповненим")
                .Matches("^[0-9]{3}$").WithMessage("Неправильний CVV");

            RuleFor(x => x.CardsExpiredYear).NotEmpty().WithMessage("Поле має бути заповненим")
                .InclusiveBetween(DateTime.Now.Year.ToString(), (DateTime.Now.Year + 4).ToString()).WithMessage("Термін дії повинен бути від 2021 до 2025");

            RuleFor(x => x.CardsExpiredMonth).NotEmpty().WithMessage("Поле має бути заповненим");
       }
    }
}
