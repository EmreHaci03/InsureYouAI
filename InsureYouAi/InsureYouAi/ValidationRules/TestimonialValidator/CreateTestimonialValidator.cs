using FluentValidation;
using InsureYouAi.Dtos.TestimonialDtos;

namespace InsureYouAi.ValidationRules.TestimonialValidator
{
    public class CreateTestimonialValidator : AbstractValidator<CreateTestimonialDto>
    {
        public CreateTestimonialValidator()
        {
            RuleFor(x => x.Title)
           .NotEmpty().WithMessage("Başlık Boş Geçilemez")
           .MinimumLength(2).WithMessage("Başlık  En az 2 karakter olmalıdır.")
            .MaximumLength(50).WithMessage("Başlık En fazla 50 karakter olabilir.");

            RuleFor(x => x.ImageUrl)
         .NotEmpty().WithMessage("Resim Boş Geçilemez")
         .Must(url => !string.IsNullOrEmpty(url) && (url.EndsWith(".jpg") || url.EndsWith(".jpeg") || url.EndsWith(".png")))
          .WithMessage("Geçersiz resim formatı. (jpg, jpeg, png desteklenir)");

            RuleFor(x => x.CommentDetail)
         .NotEmpty().WithMessage("Yorum Boş Geçilemez")
         .MinimumLength(10).WithMessage("Yorum  En az 10 karakter olmalıdır.")
          .MaximumLength(200).WithMessage("Yorum En fazla 200 karakter olabilir.");
        }
    }
}
