using MediatR;
using TaskManagementSystem.Backend.Application.Models;

namespace TaskManagementSystem.Backend.Application.Commands.UserCommands
{
    public class CreateUserCommand : IRequest<Result>
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
    }
}
