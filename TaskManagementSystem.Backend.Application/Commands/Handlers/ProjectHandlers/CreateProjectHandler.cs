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
    public class CreateProjectHandler : IRequestHandler<CreateProjectCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateProjectHandler> _logger;
        private readonly IValidator<CreateProjectCommand> _validator;
        public CreateProjectHandler(IUnitOfWork unitOfWork, ILogger<CreateProjectHandler> logger, IValidator<CreateProjectCommand> validator)
        {
            this._unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }
        public async Task<Result> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errorList = validationResult.Errors.Select(vf => vf.ErrorMessage).ToList();
                return Result.ValidationFailed(errorList);
            }

            try
                {
                // რეალურ პროექტზე იუზერ-ის ენთითი ში უნდა იყოს AddProject და იქიდან ამატებდე
                // ასევე, რეალურ პროექტში მომხარებლის აიდის სესიის ტოკენიდან ამოვიღებდი და არა უშუალოდ რექუესთიდან, მაგრამ ასე დავტოვე სიმარტივისთვის.
                var user = await _unitOfWork.Repository<User>().GetByIdAsync(request.CreatorUserId);
                if (user is null)
                {
                    return Result.NotFound();
                }

                await _unitOfWork.Repository<Project>().AddAsync(Project.CreateProject(request.CreatorUserId, request.Name, request.Description));
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
