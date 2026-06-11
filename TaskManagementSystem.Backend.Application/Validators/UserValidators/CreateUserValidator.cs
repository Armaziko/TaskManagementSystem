using FluentValidation;
using TaskManagementSystem.Backend.Application.Commands.UserCommands;

namespace TaskManagementSystem.Backend.Application.Validators.UserValidators
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
        {
            RuleFor(u => u.FirstName).NotEmpty();
            RuleFor(u => u.LastName).NotEmpty();
            RuleFor(u => u.Email).EmailAddress();
        }
    }
}
