using FluentValidation;
using InsureYouAi.Dtos.ServiceDtos;

namespace InsureYouAi.ValidationRules.ServiceValidator
{
    public class CreateServiceValidator:AbstractValidator<CreateServiceDto>
    {
        public CreateServiceValidator()
        {
            RuleFor(x => x.Title)
               .NotEmpty().WithMessage("Başlık Boş Geçilemez")
               .MinimumLength(2).WithMessage("Başlık  En az 2 karakter olmalıdır.")
               .MaximumLength(50).WithMessage("Başlık En fazla 50 karakter olabilir.");

            RuleFor(x => x.Description)
              .NotEmpty().WithMessage("Açıklama Boş Geçilemez")
              .MinimumLength(2).WithMessage("Açıklama  En az 2 karakter olmalıdır.")
              .MaximumLength(100).WithMessage("Açıklama En fazla 100 karakter olabilir.");

            RuleFor(x => x.IconUrl)
                .NotEmpty().WithMessage("İkon Boş Geçilemez");

            RuleFor(x => x.ImageUrl)
         .NotEmpty()
         .WithMessage("Resim Boş Geçilemez.")
         .Must(url => !string.IsNullOrEmpty(url) && (url.EndsWith(".jpg") || url.EndsWith(".jpeg") || url.EndsWith(".png")))
           .WithMessage("Geçersiz resim formatı. (jpg, jpeg, png desteklenir)");

        }
    }
}
