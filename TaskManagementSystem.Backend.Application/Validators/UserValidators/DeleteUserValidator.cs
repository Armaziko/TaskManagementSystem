using FluentValidation;
using TaskManagementSystem.Backend.Application.Commands.UserCommands;

namespace TaskManagementSystem.Backend.Application.Validators.UserValidators
{
    public class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserValidator()
        {
            RuleFor(u => u.Id).NotEmpty();
        }
    }
}
