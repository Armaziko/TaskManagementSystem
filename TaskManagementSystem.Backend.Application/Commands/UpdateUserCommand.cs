using MediatR;
using TaskManagementSystem.Backend.Application.Models;
using TaskManagementSystem.Backend.Application.Models.DTOs;

namespace TaskManagementSystem.Backend.Application.Commands
{
    public class UpdateUserCommand : IRequest<Result<UserDto>>
    {
        public Guid Id { get; set; } // ეს სესიის ტოკენიდან ან რამე მსგავსიდან უნდა იქნეს ამოღებული ზოგადად, მაგრამ დავტოვე ესე მაინც
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
    }
}
