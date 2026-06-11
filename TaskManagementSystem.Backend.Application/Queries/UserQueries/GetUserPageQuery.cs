using MediatR;
using TaskManagementSystem.Backend.Application.Models;
using TaskManagementSystem.Backend.Application.Models.DTOs;

namespace TaskManagementSystem.Backend.Application.Queries.UserQueries
{
    public class GetUserPageQuery : IRequest<Result<UserPageDto>>
    {
        public int Page { get; set; }
        public int? ItemLimit { get; set; } = 4;
        public bool? IsInProjects { get; set; }
        public bool? SortedByName { get; set; }
        public string? SearchName { get; set; }
    }
}
