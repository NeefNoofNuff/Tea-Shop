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
                .MinimumLength(1).NotNull();
            RuleFor(order => order.PhoneNumber)
                .NotNull();
            RuleFor(order => order.UnitsCount)
                .NotNull()
                .ExclusiveBetween(1, 10);
            RuleFor(order => order.ProductId)
                .NotNull();
            RuleFor(order => order.Product)
                .NotNull();
        }
    }
}
