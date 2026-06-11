using MediatR;
using Microsoft.Extensions.Logging;
using TaskManagementSystem.Backend.Application.Abstractions;
using TaskManagementSystem.Backend.Application.Models;
using TaskManagementSystem.Backend.Application.Models.DTOs;
using TaskManagementSystem.Backend.Application.Queries.TaskItemQueries;
using TaskManagementSystem.Backend.Domain.Entities;

namespace TaskManagementSystem.Backend.Application.Queries.Handlers.TaskItemHandlers
{
    public class GetAllTaskItemsHandler : IRequestHandler<GetAllTaskItemsQuery, Result<IReadOnlyList<TaskItemDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAllTaskItemsHandler> _logger;
        public GetAllTaskItemsHandler(IUnitOfWork unitOfWork, ILogger<GetAllTaskItemsHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<Result<IReadOnlyList<TaskItemDto>>> Handle(GetAllTaskItemsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var repo = this._unitOfWork.Repository<TaskItem>();
                var items = await repo.GetAllAsync();

                IReadOnlyList<TaskItemDto> itemDtos = items.Select(i => new TaskItemDto() 
                {
                    Id = i.Id,
                    AssignedUserId = i.AssignedUserId,
                    CreatorUserId = i.CreatorUserId,
                    Description = i.Description,
                    Title = i.Title,
                }).ToList();

                return Result<IReadOnlyList<TaskItemDto>>.Success(itemDtos);
            }
            catch (Exception ex) 
            {
                this._logger.LogError(ex, "Something went wrong in get all task items handler.");
                return Result<IReadOnlyList<TaskItemDto>>.InternalError<IReadOnlyList<TaskItemDto>>();
            }
        }
    }
}
