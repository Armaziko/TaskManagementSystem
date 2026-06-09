using MediatR;
using TaskManagementSystem.Backend.Application.Models;
using TaskManagementSystem.Backend.Domain.Entities;

namespace TaskManagementSystem.Backend.Application.Queries
{
    public class GetAllUsersQuery : IRequest<Result<IReadOnlyList<User>>>
    {
    }
}
