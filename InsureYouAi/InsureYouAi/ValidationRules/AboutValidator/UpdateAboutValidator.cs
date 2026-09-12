using FluentValidation;
using InsureYouAi.Dtos.AboutDtos;

namespace InsureYouAi.ValidationRules.AboutValidator
{
    public class UpdateAboutValidator:AbstractValidator<UpdateAboutDto>
    {
        public UpdateAboutValidator()
        {
            RuleFor(x => x.Title)
           .NotEmpty().WithMessage("Başlık boş olamaz.")
           .MinimumLength(2).WithMessage("Başlık adı en az 2 karakter olmalıdır.")
           .MaximumLength(50).WithMessage("Başlık adı en fazla 50 karakter olabilir.");

            RuleFor(x => x.Description)
          .NotEmpty().WithMessage("Açıklama boş olamaz.")
          .MinimumLength(2).WithMessage("Açıklama adı en az 2 karakter olmalıdır.")
          .MaximumLength(50).WithMessage("Açıklama adı en fazla 50 karakter olabilir.");


            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("Resim boş Geçilemez.")
                .Must(url => !string.IsNullOrEmpty(url) &&
                (url.EndsWith(".jpg") ||
                url.EndsWith(".jpeg") ||
                url.EndsWith(".png")))
                .WithMessage("Geçersiz resim formatı. (jpg, jpeg, png desteklenir)");
        }
    }
}
