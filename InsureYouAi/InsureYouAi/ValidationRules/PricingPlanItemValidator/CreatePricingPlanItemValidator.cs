using FluentValidation;
using InsureYouAi.Dtos.PricingPlanItemDtos;

namespace InsureYouAi.ValidationRules.PricingPlanItemValidator
{
    public class CreatePricingPlanItemValidator:AbstractValidator<CreatePricingPlanItemDto>
    {
        public CreatePricingPlanItemValidator()
        {
            RuleFor(x => x.PricingPlanId)
           .NotEmpty().WithMessage("Ödeme Planı Boş Bırakılamaz");

            RuleFor(x => x.PlanItem)
                .NotEmpty().WithMessage("Ödeme Detayı Boş Bırakılamaz.");
        }
    }
}
