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
    public class GetAllCommentHandler : IRequestHandler<GetAllCommentsQuery, Result<IReadOnlyList<CommentDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAllCommentHandler> _logger;

        public GetAllCommentHandler(IUnitOfWork unitOfWork, ILogger<GetAllCommentHandler> logger)
        {
            this._unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<Result<IReadOnlyList<CommentDto>>> Handle(GetAllCommentsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var repo = _unitOfWork.Repository<Comment>();
                var comments = await repo.GetAllAsync();

                IReadOnlyList<CommentDto> projectDtos = comments.Select(p => new CommentDto()
                {
                    Id = p.Id,
                    CommentMakerId = p.CommentMakerId,
                    Content = p.Content,
                    CreatedAt = p.CreatedAt,
                    TaskId = p.TaskId,
                }).ToList();
                return Result<IReadOnlyList<CommentDto>>.Success(projectDtos);
            }
            catch (DbException ex)
            {
                this._logger.LogError(ex, "Something went wrong with the database.");
                return Result<IReadOnlyList<CommentDto>>.InternalError<IReadOnlyList<CommentDto>>();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Something went wrong.");
                return Result<IReadOnlyList<CommentDto>>.InternalError<IReadOnlyList<CommentDto>>();
            }
        }
    }
}
