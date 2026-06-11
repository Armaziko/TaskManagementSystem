using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Data.Common;
using TaskManagementSystem.Backend.Application.Abstractions;
using TaskManagementSystem.Backend.Application.Commands.TaskItemCommands;
using TaskManagementSystem.Backend.Application.Models;
using TaskManagementSystem.Backend.Domain.Entities;

namespace TaskManagementSystem.Backend.Application.Commands.Handlers.TaskItemHandlers
{
    public class DeleteTaskItemHandler : IRequestHandler<DeleteTaskItemCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteTaskItemHandler> _logger;
        private readonly IValidator<DeleteTaskItemCommand> _validator;

        public DeleteTaskItemHandler(IUnitOfWork unitOfWork, ILogger<DeleteTaskItemHandler> logger, IValidator<DeleteTaskItemCommand> validator)
        {
            this._unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }
        public async Task<Result> Handle(DeleteTaskItemCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errorList = validationResult.Errors.Select(vf => vf.ErrorMessage).ToList();
                return Result.ValidationFailed(errorList);
            }

            try
            {
                var repo = _unitOfWork.Repository<TaskItem>();
                var taskItem = await repo.GetByIdAsync(request.Id);
                if (taskItem is null)
                {
                    return Result.NotFound();
                }

                repo.Remove(taskItem);

                await _unitOfWork.CommitAsync();
                return Result.Success();
            }
            catch (DbException ex)
            {
                this._logger.LogError(ex, "Something went wrong with the database.");
                return Result.InternalError();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Something went wrong.");
                return Result.InternalError();
            }
        }
    }
}
