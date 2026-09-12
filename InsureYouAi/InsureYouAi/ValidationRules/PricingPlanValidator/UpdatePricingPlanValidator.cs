using FluentValidation;
using InsureYouAi.Dtos.PricingPlanDtos;
using InsureYouAi.Entities;

namespace InsureYouAi.ValidationRules.PricingPlanValidator
{
    public class UpdatePricingPlanValidator:AbstractValidator<UpdatePricingPlanDto>
    {
        public UpdatePricingPlanValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlık Boş Geçilemez")
                .MinimumLength(2).WithMessage("Başlık  En az 2 karakter olmalıdır.")
                .MaximumLength(50).WithMessage("Başlık En fazla 50 karakter olabilir.");

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Fiyat Boş Olamaz")
                .GreaterThan(0).WithMessage("Fiyat 0 Dan Büyük Olmalıdır.");

        }
    }
}
