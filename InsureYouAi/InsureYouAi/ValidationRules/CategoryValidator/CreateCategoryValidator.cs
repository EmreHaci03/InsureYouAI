using FluentValidation;
using InsureYouAi.Dtos.CategoryDtos;

namespace InsureYouAi.ValidationRules.CategoryValidator
{
    public class CreateCategoryValidator:AbstractValidator<CreateCategoryDto>
    {

        public CreateCategoryValidator()
        {
            RuleFor(x => x.CategoryName)
                .NotEmpty().WithMessage("Kategori adı boş olamaz.")
                .MinimumLength(2).WithMessage("Kategori adı en az 2 karakter olmalıdır.")
                .MaximumLength(50).WithMessage("Kategori adı en fazla 50 karakter olabilir.");
        }
    }
}
