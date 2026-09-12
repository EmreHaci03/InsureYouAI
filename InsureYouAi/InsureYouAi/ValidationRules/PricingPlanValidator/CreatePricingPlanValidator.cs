using FluentValidation;
using InsureYouAi.Dtos.PricingPlanDtos;

namespace InsureYouAi.ValidationRules.PricingPlanValidator
{
    public class CreatePricingPlanValidator:AbstractValidator<CreatePricingPlanDto>
    {
        public CreatePricingPlanValidator()
        {
            RuleFor(x => x.Title)
               .NotEmpty().WithMessage("Başlık Boş Geçilemez")
               .MinimumLength(2).WithMessage("Başlık  En az 2 karakter olmalıdır.")
               .MaximumLength(50).WithMessage("Başlık En fazla 50 karakter olabilir.");

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Fiyat Boş Geçilemez")
                .GreaterThan(0).WithMessage("Fiyat 0 Dan Büyük Olmalı");
        }
    }
}
