using MediatR;
using Microsoft.Extensions.Logging;
using System.Data.Common;
using TaskManagementSystem.Backend.Application.Abstractions;
using TaskManagementSystem.Backend.Application.Models;
using TaskManagementSystem.Backend.Application.Models.DTOs;
using TaskManagementSystem.Backend.Application.Queries.ProjectQueries;
using TaskManagementSystem.Backend.Domain.Entities;

namespace TaskManagementSystem.Backend.Application.Queries.Handlers.ProjectHandlers
{
    public class GetAllProjectsHandler : IRequestHandler<GetAllProjectsQuery, Result<IReadOnlyList<ProjectDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAllProjectsHandler> _logger;

        public GetAllProjectsHandler(IUnitOfWork unitOfWork, ILogger<GetAllProjectsHandler> logger)
        {
            this._unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<Result<IReadOnlyList<ProjectDto>>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var repo = _unitOfWork.Repository<Project>();
                var projects = await repo.GetAllAsync();

                IReadOnlyList<ProjectDto> projectDtos = projects.Select(p => new ProjectDto()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description
                }).ToList();
                return Result<IReadOnlyList<ProjectDto>>.Success(projectDtos);
            }
            catch (DbException ex)
            {
                this._logger.LogError(ex, "Something went wrong with the database.");
                return Result<IReadOnlyList<ProjectDto>>.InternalError<IReadOnlyList<ProjectDto>>();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Something went wrong.");
                return Result<IReadOnlyList<ProjectDto>>.InternalError<IReadOnlyList<ProjectDto>>();
            }
        }
    }
}
