using ETrade.Application.Features.UserImages.DTOs;
using FluentValidation;

namespace ETrade.Application.Features.UserImages.Validations;

public class UserImageAddDtoValidator:AbstractValidator<UserImageAddDto>
{
    public UserImageAddDtoValidator()
    {
        RuleFor(userImage => userImage.ImageTitle)
            .NotNull()
            .WithMessage("Lütden resim başlığını boş geçmeyiniz....")
            .NotEmpty()
            .WithMessage("Lütden resim başlığını boş geçmeyiniz....")
            .MaximumLength(100)
            .WithMessage("En fazla 100 karakter girebilirsiniz...");

        RuleFor(userImage => userImage.ImageAltText)
            .MaximumLength(250)
            .WithMessage("En fazla 250 karakter girebilirsiniz...");
        
        RuleFor(userImage => userImage.Note)
            .MaximumLength(500)
            .WithMessage("En fazla 500 karakter girebilirsiniz...");

        RuleFor(userImage => userImage.ImageFile)
            .NotNull()
            .WithMessage("Lütden resimi boş geçmeyiniz....")
            .NotEmpty()
            .WithMessage("Lütden resimi boş geçmeyiniz....");
    }
}