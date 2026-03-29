using FluentValidation;
using InventoryManagementSystem.DTOs.SupplierDTOs;

namespace InventoryManagementSystem.Validators
{
    public class SupplierValidator : AbstractValidator<SupplierAddDto>
    {
        public SupplierValidator()
        {
            RuleFor(s => s.Name)
                .NotEmpty().WithMessage("Supplier name cannot be empty");

            RuleFor(s => s.ContactEmail)
                .NotEmpty().WithMessage("Supplier must have email")
                .EmailAddress().WithMessage("Incorrect email foramt");

           
             
        }
    }
}
