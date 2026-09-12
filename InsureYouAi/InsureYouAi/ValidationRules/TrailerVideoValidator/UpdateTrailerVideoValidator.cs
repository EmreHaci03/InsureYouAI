using FluentValidation;
using InsureYouAi.Dtos.TrailerVideoDtos;

namespace InsureYouAi.ValidationRules.TrailerVideoValidator
{
    public class UpdateTrailerVideoValidator:AbstractValidator<UpdateTrailerVideoDto>
    {
        public UpdateTrailerVideoValidator()
        {
            RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Başlık Boş Bırakılamaz")
            .MinimumLength(2).WithMessage("Başlık En Az 2 Karakter Olmalıdır.")
             .MaximumLength(30).WithMessage("Başlık En Fazla 30 Karakter Olmalıdır.");


            RuleFor(x => x.CoverImageUrl)
            .NotEmpty().WithMessage("Arka Plan Resmi Boş Bırakılamaz")
            .Must(url=> !string.IsNullOrEmpty(url) &&(url.EndsWith(".jpg") || url.EndsWith(".jpeg") || url.EndsWith(".png")))
            .WithMessage("Geçersiz resim formatı. (jpg, jpeg, png desteklenir)");


            RuleFor(x => x.VideoUrl)
                .NotEmpty().WithMessage("Video Yolu Boş Bırakılamaz")
                .Must(url=> !string.IsNullOrEmpty(url) && (url.EndsWith(".jpg") || url.EndsWith(".jpeg") || url.EndsWith(".png")))
                .WithMessage("Geçersiz resim formatı. (jpg, jpeg, png desteklenir)");
        }
    }
}
