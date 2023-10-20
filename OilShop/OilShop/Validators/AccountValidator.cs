using FluentValidation;
using OilShop.Models;

namespace OilShop
{
    public class LoginValidation : AbstractValidator<LoginViewModel>
    {
        public LoginValidation()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Поле має бути заповненим")
                .EmailAddress().WithMessage("Неправильна пошта");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Поле має бути заповненим")
                .MinimumLength(8).WithMessage("Пароль не може бути меншим за 8 символів")
                //.Matches("^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$").WithMessage("Пароль повинен містити як мінімум 1 цифру, 1 велику літеру і 1 малу літеру")
                .MaximumLength(30).WithMessage("Пароль не може бути більшим за 30 символів");
        }
    }

    public class RegistrationValidation : AbstractValidator<RegistrationViewModel>
    {
        public RegistrationValidation()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Поле має бути заповненим")
                .EmailAddress().WithMessage("Неправильна пошта");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Поле має бути заповненим")
                .MinimumLength(8).WithMessage("Пароль не може бути меншим за 8 символів")
                .MaximumLength(30).WithMessage("Пароль не може бути більшим за 30 символів");
                //.Matches("^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$").WithMessage("Пароль повинен містити як мінімум 1 цифру, 1 велику літеру і 1 малу літеру");
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Поле має бути заповненим");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Поле має бути заповненим")
            .Length(13).WithMessage("Номер телефону має складатись з 13 символів")
            .Matches(@"^\+?3?8?(0\d{9})$").WithMessage("Неправильний номер телефону");
                   RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Поле має бути заповненим")
                .Equal(x => x.Password).WithMessage("Паролі мають співпадати");
        }
    }
}
