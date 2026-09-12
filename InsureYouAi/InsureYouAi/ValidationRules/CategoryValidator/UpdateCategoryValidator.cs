using FluentValidation;
using InsureYouAi.Dtos.CategoryDtos;

namespace InsureYouAi.ValidationRules.CategoryValidator
{
    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryDto>
    {

        public UpdateCategoryValidator()
        {
            RuleFor(x => x.CategoryId)
           .GreaterThan(0).WithMessage("Geçersiz kategori.");

            RuleFor(x => x.CategoryName)
                .NotEmpty().WithMessage("Kategori adı boş olamaz.")
                .MinimumLength(2).WithMessage("Kategori adı en az 2 karakter olmalıdır.")
                .MaximumLength(50).WithMessage("Kategori adı en fazla 50 karakter olabilir.");
        }
    }
}
