using FluentValidation;
using InternetShop.Data.Models;

namespace InternetShop.Logic.Validation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(product => product.Name)
                .MaximumLength(100)
                .MinimumLength(5);
            RuleFor(product => product.SupplierId)
                .NotEmpty();
            RuleFor(product => product.UnitInStock)
                .NotEmpty()
                .NotNull();
            RuleFor(product => product.Price)
                .NotNull();
        }
    }
}
