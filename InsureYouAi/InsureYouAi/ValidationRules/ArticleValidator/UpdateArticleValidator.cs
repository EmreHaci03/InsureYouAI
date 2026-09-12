using FluentValidation;
using InsureYouAi.Dtos.ArticleDtos;

namespace InsureYouAi.ValidationRules.ArticleValidator
{
    public class UpdateArticleValidator : AbstractValidator<UpdateArticleDto>
    {
        public UpdateArticleValidator()
        {
            RuleFor(x => x.Title)
   
                .NotEmpty().WithMessage("Başlık boş olamaz.")
                .MinimumLength(2).WithMessage("Başlık adı en az 2 karakter olmalıdır.")
                .MaximumLength(50).WithMessage("Başlık adı en fazla 50 karakter olabilir.");


            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("İçerik boş olamaz.");

            RuleFor(x => x.CoverImageUrl)
          .NotEmpty().WithMessage("Resim boş Bırakılamaz.")
           .Must(url => url.EndsWith(".jpg") || url.EndsWith(".jpeg") || url.EndsWith(".png"))
          .WithMessage("Geçersiz resim formatı. (jpg, jpeg, png desteklenir)");


            RuleFor(x => x.MainCoverImageUrl)
          .NotEmpty().WithMessage("Arka Plan Resmi Boş Bırakılamaz.")
          .Must(url => url.EndsWith(".jpg") || (url.EndsWith(".jpeg") || url.EndsWith(".png")))
           .WithMessage("Geçersiz resim formatı. (jpg, jpeg, png desteklenir)");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Kategori Boş Bırakılamaz.");
        }
    }
}

