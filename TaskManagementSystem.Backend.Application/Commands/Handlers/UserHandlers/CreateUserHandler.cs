using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Data.Common;
using TaskManagementSystem.Backend.Application.Abstractions;
using TaskManagementSystem.Backend.Application.Commands.UserCommands;
using TaskManagementSystem.Backend.Application.Models;
using TaskManagementSystem.Backend.Domain.Entities;

namespace TaskManagementSystem.Backend.Application.Commands.Handlers.UserHandlers
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateUserHandler> _logger;
        private readonly IValidator<CreateUserCommand> _validator;
        public CreateUserHandler(IUnitOfWork unitOfWork, ILogger<CreateUserHandler> logger, IValidator<CreateUserCommand> validator)
        {
            this._unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._validator = validator ?? throw new ArgumentNullException(nameof(validator));
        } 
        public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errorList = validationResult.Errors.Select(vf => vf.ErrorMessage).ToList();
                return Result.ValidationFailed(errorList);
            }

            try
            {
                await _unitOfWork.Repository<User>().AddAsync(User.CreateUser(request.FirstName, request.LastName, request.Email));
                await _unitOfWork.CommitAsync();

                return Result.Success();
            }
            catch(DbException ex)
            {
                _logger.LogError(ex, "Something went wrong while trying to update the database.");
                return Result.InternalError();
            }catch(Exception ex)
            {
                _logger.LogError(ex, "Something went wrong in the CreateUserHandler.cs.");
                return Result.InternalError();
            }
        }
    }
}
