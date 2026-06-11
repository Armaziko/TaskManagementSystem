using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Data.Common;
using TaskManagementSystem.Backend.Application.Abstractions;
using TaskManagementSystem.Backend.Application.Models;
using TaskManagementSystem.Backend.Application.Models.DTOs;
using TaskManagementSystem.Backend.Application.Queries.ProjectQueries;
using TaskManagementSystem.Backend.Domain.Entities;

namespace TaskManagementSystem.Backend.Application.Queries.Handlers.PorjectHandlers
{
    public class GetProjectByIdHandler : IRequestHandler<GetProjectByIdQuery, Result<ProjectDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetProjectByIdHandler> _logger;
        private readonly IValidator<GetProjectByIdQuery> _validator;
        public GetProjectByIdHandler(IUnitOfWork unitOfWork, ILogger<GetProjectByIdHandler> logger, IValidator<GetProjectByIdQuery> validator)
        {
            this._unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }
        public async Task<Result<ProjectDto>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errorList = validationResult.Errors.Select(vf => vf.ErrorMessage).ToList();
                return Result<ProjectDto>.ValidationFailed<ProjectDto>(errorList);
            }
            try
            {
                var repo = _unitOfWork.Repository<Project>();
                var project = await repo.GetByIdAsync(request.Id);

                if (project is null)
                {
                    return Result<ProjectDto>.NotFound<ProjectDto>();
                }

                return Result<ProjectDto>.Success(new ProjectDto() { Name = project.Name, Description = project.Description, Id = project.Id });
            }
            catch (DbException ex)
            {
                this._logger.LogError(ex, "Something went wrong with the database.");
                return Result<ProjectDto>.InternalError<ProjectDto>();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Something went wrong.");
                return Result<ProjectDto>.InternalError<ProjectDto>();
            }
        }
    }
}
