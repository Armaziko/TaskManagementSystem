using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Data.Common;
using TaskManagementSystem.Backend.Application.Abstractions;
using TaskManagementSystem.Backend.Application.Commands.ProjectCommands;
using TaskManagementSystem.Backend.Application.Models;
using TaskManagementSystem.Backend.Domain.Entities;

namespace TaskManagementSystem.Backend.Application.Commands.Handlers.ProjectHandlers
{
    public class DeleteProjectHandler : IRequestHandler<DeleteProjectCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteProjectHandler> _logger;
        private readonly IValidator<DeleteProjectCommand> _validator;
        public DeleteProjectHandler(IValidator<DeleteProjectCommand> validator, IUnitOfWork unitOfWork, ILogger<DeleteProjectHandler> logger)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<Result> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await this._validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errorList = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result.ValidationFailed(errorList);
            }
            try
            {
                var repo = this._unitOfWork.Repository<Project>();
                var project = await repo.GetByIdAsync(request.Id);
                if (project is null)
                {
                    return Result.NotFound();
                }

                repo.Remove(project);
                await this._unitOfWork.CommitAsync();
                return Result.Success();
            }
            catch (DbException ex)
            {
                this._logger.LogError(ex, "Something has gone wrong with the database.");
                return Result.InternalError();
            }
            catch (Exception ex) 
            {
                this._logger.LogError(ex, "Something has gone wrong in DeleteProjectHandler");
                return Result.InternalError();

            }
        }
    }
}
