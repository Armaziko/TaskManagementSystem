using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Data.Common;
using TaskManagementSystem.Backend.Application.Abstractions;
using TaskManagementSystem.Backend.Application.Commands.CommentCommands;
using TaskManagementSystem.Backend.Application.Models;
using TaskManagementSystem.Backend.Domain.Entities;

namespace TaskManagementSystem.Backend.Application.Commands.Handlers.CommentHandlers
{
    public class CreateCommentHandler : IRequestHandler<CreateCommentCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateCommentHandler> _logger;
        private readonly IValidator<CreateCommentCommand> _validator;
        public CreateCommentHandler(IUnitOfWork unitOfWork, ILogger<CreateCommentHandler> logger, IValidator<CreateCommentCommand> validator)
        {
            this._unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }
        public async Task<Result> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errorList = validationResult.Errors.Select(vf => vf.ErrorMessage).ToList();
                return Result.ValidationFailed(errorList);
            }

            try
            {
                var taskItem = await _unitOfWork.Repository<TaskItem>().GetByIdAsync(request.TaskId);
                if (taskItem is null)
                {
                    return Result.NotFound();

                }

                await _unitOfWork.Repository<Comment>().AddAsync(Comment.CreateProject(request.Content, request.TaskId, request.CommentMakerId));
                await _unitOfWork.CommitAsync();

                return Result.Success();
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, "Something went wrong while trying to update the database.");
                return Result.InternalError();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong in the CreateUserHandler.cs.");
                return Result.InternalError();
            }
        }
    }
}
