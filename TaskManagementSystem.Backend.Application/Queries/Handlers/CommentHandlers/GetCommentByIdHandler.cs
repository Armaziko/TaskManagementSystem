using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Data.Common;
using TaskManagementSystem.Backend.Application.Abstractions;
using TaskManagementSystem.Backend.Application.Models;
using TaskManagementSystem.Backend.Application.Models.DTOs;
using TaskManagementSystem.Backend.Application.Queries.CommentQueries;
using TaskManagementSystem.Backend.Domain.Entities;

namespace TaskManagementSystem.Backend.Application.Queries.Handlers.CommentHandlers
{
    public class GetCommentByIdHandler : IRequestHandler<GetCommentByIdQuery, Result<CommentDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetCommentByIdHandler> _logger;
        private readonly IValidator<GetCommentByIdQuery> _validator;
        public GetCommentByIdHandler(IUnitOfWork unitOfWork, ILogger<GetCommentByIdHandler> logger, IValidator<GetCommentByIdQuery> validator)
        {
            this._unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }
        public async Task<Result<CommentDto>> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errorList = validationResult.Errors.Select(vf => vf.ErrorMessage).ToList();
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

                return Result<CommentDto>.Success(new CommentDto() 
                {
                    Id = comment.Id,
                    CommentMakerId = comment.CommentMakerId,
                    Content = comment.Content,
                    CreatedAt = comment.CreatedAt,
                    TaskId = comment.TaskId,
                });
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
