using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Data.Common;
using TaskManagementSystem.Backend.Application.Abstractions;
using TaskManagementSystem.Backend.Application.Commands.CommentCommands;
using TaskManagementSystem.Backend.Application.Models;
using TaskManagementSystem.Backend.Application.Models.DTOs;
using TaskManagementSystem.Backend.Domain.Entities;

namespace TaskManagementSystem.Backend.Application.Commands.Handlers.CommentHandlers
{
    public class UpdateCommentHandler : IRequestHandler<UpdateCommentCommand, Result<CommentDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateCommentHandler> _logger;
        private readonly IValidator<UpdateCommentCommand> _updateUserValidator;
        public UpdateCommentHandler(IUnitOfWork unitOfWork, ILogger<UpdateCommentHandler> logger, IValidator<UpdateCommentCommand> getProductsValidator)
        {
            this._unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._updateUserValidator = getProductsValidator ?? throw new ArgumentNullException(nameof(getProductsValidator));
        }
        public async Task<Result<CommentDto>> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _updateUserValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errorList = validationResult.Errors.Select(er => er.ErrorMessage).ToList();
                return Result<CommentDto>.ValidationFailed<CommentDto>(errorList);
            }

            try
            {
                var repo = _unitOfWork.Repository<Comment>();
                var comment = await repo.GetByIdAsync(request.Id);
                if (comment is null)
                {
                    return Result<CommentDto>.NotFound<CommentDto>();
                }
                if (!string.IsNullOrWhiteSpace(request.Content))
                {
                    comment.Content = request.Content;
                }

                repo.Update(comment);

                await _unitOfWork.CommitAsync();
                return Result<CommentDto>.Success(new CommentDto() 
                { 
                    Id = comment.Id,
                    CommentMakerId = comment.CommentMakerId,
                    CreatedAt = comment.CreatedAt,
                    Content = comment.Content,
                    TaskId = comment.TaskId });
            }
            catch (DbException ex)
            {
                this._logger.LogError(ex, "Something went wrong with the database.");
                return Result<CommentDto>.InternalError<CommentDto>();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Something went wrong.");
                return Result<CommentDto>.InternalError<CommentDto>();
            }
        }
    }
}
