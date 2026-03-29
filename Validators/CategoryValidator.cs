using FluentValidation;
using InventoryManagementSystem.DTOs.CategoryDTOs;
using InventoryManagementSystem.UnitOfWork;

namespace InventoryManagementSystem.Validators
{
    public class CategoryValidator : AbstractValidator<CategoryAddDto>
    {

        private readonly IUnitOfWork _unitOfWork;
        public CategoryValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Category name cannot be empty")
                .MustAsync(BeAUniqueName).WithMessage("Category name is already exist");
                
        }

        private async Task<bool> BeAUniqueName(string name, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(name)) return true;
            return await _unitOfWork.CategoryRepository.IsNameUniqueAsync(name);
        }
    }
}
