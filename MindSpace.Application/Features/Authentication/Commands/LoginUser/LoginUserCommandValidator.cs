using FluentValidation;

namespace MindSpace.Application.Features.Authentication.Commands.LoginUser
{
    internal class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(rq => rq.Email)
            .EmailAddress()
            .WithMessage("Invalid Email");
            RuleFor(rq => rq.Password)
                .Matches(@"(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]{6,}")
                .WithMessage("Invalid Password must be at least 6 characters, at least 1 special character (@#$%^&*) and at least 1 number");
        }
    }
}