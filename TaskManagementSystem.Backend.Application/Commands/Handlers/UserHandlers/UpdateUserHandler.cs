using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Data.Common;
using TaskManagementSystem.Backend.Application.Abstractions;
using TaskManagementSystem.Backend.Application.Commands.UserCommands;
using TaskManagementSystem.Backend.Application.Models;
using TaskManagementSystem.Backend.Application.Models.DTOs;
using TaskManagementSystem.Backend.Domain.Entities;

namespace TaskManagementSystem.Backend.Application.Commands.Handlers.UserHandlers
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Result<UserDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateUserHandler> _logger;
        private readonly IValidator<UpdateUserCommand> _updateUserValidator;
        public UpdateUserHandler(IUnitOfWork unitOfWork, ILogger<UpdateUserHandler> logger, IValidator<UpdateUserCommand> getProductsValidator)
        {
            this._unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._updateUserValidator = getProductsValidator ?? throw new ArgumentNullException(nameof(getProductsValidator));
        }
        public async Task<Result<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _updateUserValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errorList = validationResult.Errors.Select(er => er.ErrorMessage).ToList();
                return Result<UserDto>.ValidationFailed<UserDto>(errorList);
            }

            try
            {
                var repo = _unitOfWork.Repository<User>();
                var user = await repo.GetByIdAsync(request.Id);
                if (user is null)
                {
                    return Result<UserDto>.NotFound<UserDto>();
                }
                if (!string.IsNullOrWhiteSpace(request.FirstName))
                {
                    user.FirstName = request.FirstName;
                }
                if (!string.IsNullOrWhiteSpace(request.LastName))

                {
                    user.LastName = request.LastName;
                }
                if (!string.IsNullOrWhiteSpace(request.Email))

                {
                    user.Email = request.Email;
                }

                repo.Update(user);

                await _unitOfWork.CommitAsync();
                return Result<UserDto>.Success(
                    new UserDto()
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email
                    });
            }
            catch (DbException ex)
            {
                this._logger.LogError(ex, "Something went wrong with the database.");
                return Result<UserDto>.InternalError<UserDto>();
            } catch (Exception ex) 
            {
                this._logger.LogError(ex, "Something went wrong.");
                return Result<UserDto>.InternalError<UserDto>();
            }
        }
    }
}
