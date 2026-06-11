using MediatR;
using TaskManagementSystem.Backend.Application.Models;
using TaskManagementSystem.Backend.Application.Models.DTOs;

namespace TaskManagementSystem.Backend.Application.Queries.UserQueries
{
    public class GetAllUsersQuery : IRequest<Result<IReadOnlyList<UserDto>>>
    {
    }
}
