using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Data.Common;
using TaskManagementSystem.Backend.Application.Abstractions;
using TaskManagementSystem.Backend.Application.Commands.TaskItemCommands;
using TaskManagementSystem.Backend.Application.Models;
using TaskManagementSystem.Backend.Application.Models.DTOs;
using TaskManagementSystem.Backend.Domain.Entities;

namespace TaskManagementSystem.Backend.Application.Commands.Handlers.TaskItemHandlers
{
    public class UpdateTaskItemHandler : IRequestHandler<UpdateTaskItemCommand, Result<TaskItemDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateTaskItemHandler> _logger;
        private readonly IValidator<UpdateTaskItemCommand> _updateUserValidator;
        public UpdateTaskItemHandler(IUnitOfWork unitOfWork, ILogger<UpdateTaskItemHandler> logger, IValidator<UpdateTaskItemCommand> getProductsValidator)
        {
            this._unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._updateUserValidator = getProductsValidator ?? throw new ArgumentNullException(nameof(getProductsValidator));
        }
        public async Task<Result<TaskItemDto>> Handle(UpdateTaskItemCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _updateUserValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errorList = validationResult.Errors.Select(er => er.ErrorMessage).ToList();
                return Result<TaskItemDto>.ValidationFailed<TaskItemDto>(errorList);
            }

            try
            {
                var repo = _unitOfWork.Repository<TaskItem>();
                var taskItem = await repo.GetByIdAsync(request.Id);
                if (taskItem is null)
                {
                    return Result<TaskItemDto>.NotFound<TaskItemDto>();
                }
                if (!string.IsNullOrWhiteSpace(request.Name))
                {
                    taskItem.Title = request.Name;
                }
                if (!string.IsNullOrWhiteSpace(request.Description))

                {
                    taskItem.Description = request.Description;
                }
                if (request.AssignedUserId is not null)

                {
                    taskItem.AssignedUserId = (Guid)request.AssignedUserId;
                }

                repo.Update(taskItem);

                await _unitOfWork.CommitAsync();
                return Result<TaskItemDto>.Success(new TaskItemDto()
                {
                    AssignedUserId = taskItem.AssignedUserId,
                    Description = taskItem.Description,
                    CreatorUserId = taskItem.CreatorUserId,
                    Id = taskItem.Id,
                    Title = taskItem.Title
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
