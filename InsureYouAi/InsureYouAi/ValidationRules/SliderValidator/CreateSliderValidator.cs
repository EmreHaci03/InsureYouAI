using FluentValidation;
using InsureYouAi.Dtos.SliderDtos;

namespace InsureYouAi.ValidationRules.SliderValidator
{
    public class CreateSliderValidator:AbstractValidator<CreateSliderDto>
    {
        public CreateSliderValidator()
        {
            RuleFor(x => x.Title)
                       .NotEmpty().WithMessage("Başlık Boş Bırakılamaz")
                       .MinimumLength(2).WithMessage("Başlık En Az 2 Karakter Olmalıdır")
                       .MaximumLength(30).WithMessage("Başlık En Fazla 30 Karakter Olmalıdır");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama Boş Bırakılamaz")
                .MinimumLength(2).WithMessage("Açıklama En Az 2 Karakter Olmalıdır")
                .MaximumLength(100).WithMessage("Açıklama En Fazla 100 Karakter Olmalıdır");

            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("Resim Boş Bırakılamaz")
                .Must(url => !string.IsNullOrEmpty(url) &&
                  (url.EndsWith(".jpeg") ||
                   url.EndsWith(".jpg") ||
                   url.EndsWith(".png")))
                .WithMessage("Geçersiz resim formatı. (jpg, jpeg, png desteklenir)");
        }
    }
}
