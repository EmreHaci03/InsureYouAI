using FluentValidation;
using InsureYouAi.Dtos.AboutItemDtos;

namespace InsureYouAi.ValidationRules.AboutItemValidator
{
    public class UpdateAboutItemValidator:AbstractValidator<UpdateAboutItemDto>
    {
        public UpdateAboutItemValidator()
        {
            RuleFor(x => x.AboutId)
           .NotEmpty().WithMessage("Hakkımda Seçimi Boş Bırakılamaz");


            RuleFor(x => x.ItemDetail)
            .NotEmpty().WithMessage("Öğe Detayı Boş Bırakılamaz")
            .MinimumLength(2).WithMessage("Öğe Detayı En Az 2 Karakter Olmalıdır")
            .MaximumLength(50).WithMessage("Öğe Detayı En Az 50 Karakter Olmalıdır");
        }
    }
}
