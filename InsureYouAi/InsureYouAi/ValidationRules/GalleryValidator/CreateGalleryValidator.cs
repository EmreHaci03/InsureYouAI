using FluentValidation;
using InsureYouAi.Dtos.GalleryDtos;

namespace InsureYouAi.ValidationRules.GalleryValidator
{
    public class CreateGalleryValidator:AbstractValidator<CreateGalleryDto>
    {

        public CreateGalleryValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlık Alanı Boş Bırakılamaz")
                .MinimumLength(5).WithMessage("Başlık Alanı En Az 5 Karakter Olmalıdır")
                .MaximumLength(30).WithMessage("Başlık Alanı En Fazla 30 Karakter Olmalıdır");

            RuleFor(x => x.ImageUrl)
               .NotEmpty().WithMessage("Resim Yolu Boş Bırakılamaz")
               .Must(url => !string.IsNullOrEmpty(url) && (url.EndsWith(".jpg") || url.EndsWith(".jpeg") || url.EndsWith(".png")))
               .WithMessage("Geçersiz resim formatı. (jpg, jpeg, png desteklenir)");
        }
    }
}
