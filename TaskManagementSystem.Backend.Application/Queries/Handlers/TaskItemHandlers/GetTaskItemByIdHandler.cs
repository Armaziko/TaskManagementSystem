using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Data.Common;
using TaskManagementSystem.Backend.Application.Abstractions;
using TaskManagementSystem.Backend.Application.Models;
using TaskManagementSystem.Backend.Application.Models.DTOs;
using TaskManagementSystem.Backend.Application.Queries.TaskItemQueries;
using TaskManagementSystem.Backend.Domain.Entities;

namespace TaskManagementSystem.Backend.Application.Queries.Handlers.TaskItemHandlers
{
    public class GetTaskItemByIdHandler : IRequestHandler<GetTaskItemByIdQuery, Result<TaskItemDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetTaskItemByIdHandler> _logger;
        private readonly IValidator<GetTaskItemByIdQuery> _validator;
        public GetTaskItemByIdHandler(IUnitOfWork unitOfWork, ILogger<GetTaskItemByIdHandler> logger, IValidator<GetTaskItemByIdQuery> validator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }
        public async Task<Result<TaskItemDto>> Handle(GetTaskItemByIdQuery request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid) 
            {
                var errorList = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return Result<TaskItemDto>.ValidationFailed<TaskItemDto>(errorList);
            }
            try
            {
                var repo = _unitOfWork.Repository<TaskItem>();
                var item = await repo.GetByIdAsync(request.Id);

                if (item is null)
                {
                    return Result<TaskItemDto>.NotFound<TaskItemDto>();
                }

                return Result<TaskItemDto>.Success(new TaskItemDto() 
                {
                    Id = item.Id,
                    AssignedUserId = item.AssignedUserId,
                    CreatorUserId = item.CreatorUserId,
                    Description = item.Description,
                    Title = item.Title,
                });
            }
            catch (DbException ex)
            {
                this._logger.LogError(ex, "Something went wrong with the database.");
                return Result<TaskItemDto>.InternalError<TaskItemDto>();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Something went wrong.");
                return Result<TaskItemDto>.InternalError<TaskItemDto>();
            }
        }
    }
}
