using MediatR;
using TaskManagementSystem.Backend.Application.Models;
using TaskManagementSystem.Backend.Application.Models.DTOs;

namespace TaskManagementSystem.Backend.Api.Controllers
{
    public class GetUserById : IRequest<Result<UserDto>>
    {
        public Guid Id { get; set; }
    }
}