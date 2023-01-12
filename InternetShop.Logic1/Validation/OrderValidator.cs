using FluentValidation;
using InternetShop.Data.Models;

namespace InternetShop.Logic.Validation
{
    public class OrderValidator : AbstractValidator<Order> 
    {
        public OrderValidator()
        {
            RuleFor(order => order.FirstName)
                .MaximumLength(40)
                .MinimumLength(1)
                .NotNull();
            RuleFor(order => order.LastName)
                .MaximumLength(50)
                .MinimumLength(1)
                .NotNull();
            RuleFor(order => order.Price)
                .NotNull();
            RuleFor(order => order.PhoneNumber)
                .NotNull();
        }
    }
}
