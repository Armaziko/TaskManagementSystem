using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using TaskManagementSystem.Backend.Application.Abstractions;
using TaskManagementSystem.Backend.Application.Models;
using TaskManagementSystem.Backend.Application.Models.DTOs;
using TaskManagementSystem.Backend.Application.Queries.UserQueries;
using TaskManagementSystem.Backend.Domain.Entities;

namespace TaskManagementSystem.Backend.Application.Queries.Handlers.UserHandlers
{
    public class GetUserPageHandler : IRequestHandler<GetUserPageQuery, Result<UserPageDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetUserPageHandler> _logger;
        private readonly IValidator<GetUserPageQuery> _validator;
        public GetUserPageHandler(IUnitOfWork unitOfWork, ILogger<GetUserPageHandler> logger, IValidator<GetUserPageQuery> validator)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this._validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }
        public async Task<Result<UserPageDto>> Handle(GetUserPageQuery request, CancellationToken cancellationToken)
        {
            var validationResult = await this._validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errorList = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<UserPageDto>.ValidationFailed<UserPageDto>(errorList);
            }


            try
            {
                var repo = _unitOfWork.Repository<User>();

                Expression<Func<User, bool>> filter = user =>
                (string.IsNullOrEmpty(request.SearchName) || user.FirstName.Contains(request.SearchName) || user.LastName.Contains(request.SearchName)) &&
                (!request.IsInProjects.HasValue || (request.IsInProjects.Value ? user.Projects.Any() : !user.Projects.Any()));

                var users = await repo.GetPage(request.Page, (int)request.ItemLimit!, filter);
                var userDtos = users.Select(u => new UserDto() 
                {
                    Id = u.Id,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                }).ToList();

                var totalItems = (int)Math.Ceiling((await repo.GetTotalCount(filter) / (double)request.ItemLimit));

                return Result<UserPageDto>.Success(new UserPageDto() { Users = userDtos, currentPage = request.Page, totalPages = totalItems });
            }catch(Exception ex)
            {
                this._logger.LogError(ex, "Something went wrong.");
                return Result<UserPageDto>.InternalError<UserPageDto>();
            }
        }
    }
}
