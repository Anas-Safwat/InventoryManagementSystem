using FluentValidation;
using InventoryManagementSystem.DTOs.CategoryDTOs;
using InventoryManagementSystem.UnitOfWork;

namespace InventoryManagementSystem.Validators
{
    public class CategoryUpdateValidator : AbstractValidator<CategoryUpdateDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryUpdateValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Category name is required.")
                .MustAsync(async (dto, name, cancellationToken) =>
                    await BeAUniqueName(name, dto.Id, cancellationToken))
                .WithMessage("Category name already exists! Please choose another one.");
        }

        private async Task<bool> BeAUniqueName(string name, int id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(name)) return true;

            return await _unitOfWork.CategoryRepository.IsNameUniqueAsync(name, id);
        }
    }
}
