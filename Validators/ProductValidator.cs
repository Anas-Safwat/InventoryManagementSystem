using FluentValidation;
using InventoryManagementSystem.DTOs.ProductDTOs;
namespace InventoryManagementSystem.Validators
{
    public class ProductValidator : AbstractValidator<ProductAddDto>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Product name cannot be empty");

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("Product price cannot be negative");

            RuleFor(p => p.StockQuantity)
                .GreaterThanOrEqualTo(0).WithMessage("Product stock quantity cannot be negative");
                      
        }
    }
    
}
