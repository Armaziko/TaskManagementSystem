using MediatR;
using TaskManagementSystem.Backend.Application.Models;

namespace TaskManagementSystem.Backend.Application.Commands.UserCommands
{
    public class DeleteUserCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}
