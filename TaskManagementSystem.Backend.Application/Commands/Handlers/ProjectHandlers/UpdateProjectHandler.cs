using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Data.Common;
using TaskManagementSystem.Backend.Application.Abstractions;
using TaskManagementSystem.Backend.Application.Commands.ProjectCommands;
using TaskManagementSystem.Backend.Application.Models;
using TaskManagementSystem.Backend.Application.Models.DTOs;
using TaskManagementSystem.Backend.Domain.Entities;

namespace TaskManagementSystem.Backend.Application.Commands.Handlers.ProjectHandlers
{
    internal class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, Result<ProjectDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateProjectHandler> _logger;
        private readonly IValidator<UpdateProjectCommand> _updateUserValidator;
        public UpdateProjectHandler(IUnitOfWork unitOfWork, ILogger<UpdateProjectHandler> logger, IValidator<UpdateProjectCommand> getProductsValidator)
        {
            this._unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._updateUserValidator = getProductsValidator ?? throw new ArgumentNullException(nameof(getProductsValidator));
        }
        public async Task<Result<ProjectDto>> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _updateUserValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errorList = validationResult.Errors.Select(er => er.ErrorMessage).ToList();
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
                if (!string.IsNullOrWhiteSpace(request.Name))

                {
                    project.Name = request.Name;
                }
                if (!string.IsNullOrWhiteSpace(request.Description))

                {
                    project.Description = request.Description;
                }

                repo.Update(project);

                await _unitOfWork.CommitAsync();
                return Result<ProjectDto>.Success(
                    new ProjectDto()
                    {
                        Id = project.Id,
                        Name = project.Name,
                        Description = project.Description 
                    });
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
