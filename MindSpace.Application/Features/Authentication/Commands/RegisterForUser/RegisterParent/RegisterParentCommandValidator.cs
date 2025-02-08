using FluentValidation;

namespace MindSpace.Application.Features.Authentication.Commands.RegisterForUser.RegisterParent
{
    internal class RegisterParentCommandValidator : AbstractValidator<RegisterParentCommand>
    {
        public RegisterParentCommandValidator()
        {
            RuleFor(rq => rq.Email)
                .EmailAddress()
                .WithMessage("Invalid Email");
            RuleFor(rq => rq.Password)
                .Matches(@"(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]{6,}")
                .WithMessage("Invalid Password must be at least 6 characters, at least 1 special character (@#$%^&*) and at least 1 number");
            RuleFor(rq => rq.PhoneNumber)
                .Matches("([0-9])+")
                .MinimumLength(10)
                .MaximumLength(11)
                .WithMessage("Invalid phone number");
            RuleFor(rq => rq.Birthdate)
                .Must(birthdate => DateTime.TryParse(birthdate, out DateTime parsedDate) && parsedDate <= DateTime.Now.AddYears(-16))
                .WithMessage("Invalid Birthdate. User must be at least 16 years old.");
        }
    }
}