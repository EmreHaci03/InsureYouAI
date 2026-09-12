using FluentValidation;
using InsureYouAi.Dtos.RegisterDtos;

namespace InsureYouAi.ValidationRules.AppUserValidator
{
    public class CreateAppUserValidator:AbstractValidator<RegisterDto>
    {
        public CreateAppUserValidator()
        {
            RuleFor(x => x.Name)
                       .NotEmpty()
                       .WithMessage("Ad boş bırakılamaz.")
                       .MinimumLength(2)
                       .WithMessage("Ad en az 2 karakter olmalıdır.");

            RuleFor(x => x.Surname)
                .NotEmpty()
                .WithMessage("Soyad boş bırakılamaz.")
                .MinimumLength(2)
                .WithMessage("Soyad en az 2 karakter olmalıdır.");

            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Kullanıcı adı boş bırakılamaz.")
                .MinimumLength(3)
                .WithMessage("Kullanıcı adı en az 3 karakter olmalıdır.")
                .MaximumLength(20)
                .WithMessage("Kullanıcı adı en fazla 20 karakter olabilir.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("E-posta boş bırakılamaz.")
                .EmailAddress()
                .WithMessage("Geçerli bir e-posta adresi giriniz.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Şifre boş bırakılamaz.");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password)
                .WithMessage("Şifreler eşleşmiyor.");
        }
    }
}
