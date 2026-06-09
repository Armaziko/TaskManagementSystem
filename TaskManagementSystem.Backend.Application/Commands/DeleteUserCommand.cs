using MediatR;
using TaskManagementSystem.Backend.Application.Models;

namespace TaskManagementSystem.Backend.Application.Commands
{
    public class DeleteUserCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}
