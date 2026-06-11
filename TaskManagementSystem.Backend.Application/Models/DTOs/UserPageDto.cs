namespace TaskManagementSystem.Backend.Application.Models.DTOs
{
    public class UserPageDto
    {
        public IReadOnlyList<UserDto> Users { get; set; } = new List<UserDto>();
        public int totalPages { get; set; }
        public int currentPage { get; set; }
    }
}
