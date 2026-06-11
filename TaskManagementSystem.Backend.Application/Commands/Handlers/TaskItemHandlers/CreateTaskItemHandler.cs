using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using TaskManagementSystem.Backend.Application.Abstractions;
using TaskManagementSystem.Backend.Application.Commands.TaskItemCommands;
using TaskManagementSystem.Backend.Application.Models;
using TaskManagementSystem.Backend.Domain.Entities;

namespace TaskManagementSystem.Backend.Application.Commands.Handlers.TaskItemHandlers
{
    public class CreateTaskItemHandler : IRequestHandler<CreateTaskItemCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateTaskItemHandler> _logger;
        private readonly IValidator<CreateTaskItemCommand> _validator;
        public CreateTaskItemHandler(IUnitOfWork unitOfWork, ILogger<CreateTaskItemHandler> logger, IValidator<CreateTaskItemCommand> validator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }
        public async Task<Result> Handle(CreateTaskItemCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await this._validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errorList = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return Result.ValidationFailed(errorList);
            }
            try
            {
                var repoItem = this._unitOfWork.Repository<TaskItem>();
                var repoProject = this._unitOfWork.Repository<Project>();
                var project = await repoProject.GetByIdAsync(request.ProjectId);

                if (project is null)
                {
                    return Result.NotFound();
                }

                await repoItem.AddAsync(TaskItem.CreateNew(request.ProjectId, request.CreatorUserId, request.AssignedUserId, request.Title, request.Description));
                await this._unitOfWork.CommitAsync();
                return Result.Success();
            }
            catch (Exception ex) 
            {
                this._logger.LogError(ex, "Something went wrong with create task item handler.");
                return Result.InternalError();
            }
        }
    }
}
