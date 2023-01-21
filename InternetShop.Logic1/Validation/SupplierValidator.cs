using FluentValidation;
using InternetShop.Data.Models;

namespace InternetShop.Logic.Validation
{
    public class SupplierValidator : AbstractValidator<Supplier>
    {
        public SupplierValidator()
        {
            RuleFor(supplier => supplier.Products)
                .NotEmpty();
            RuleFor(supplier => supplier.LastName)
                .MinimumLength(1)
                .MaximumLength(30);
            RuleFor(supplier => supplier.FirstName)
                .MinimumLength(1)
                .MaximumLength(30);
            RuleFor(supplier => supplier.CompanyName)
                .MinimumLength(1)
                .MaximumLength(99);
        }
    }
}
