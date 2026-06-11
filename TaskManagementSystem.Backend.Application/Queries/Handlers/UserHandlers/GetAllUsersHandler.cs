using MediatR;
using Microsoft.Extensions.Logging;
using System.Data.Common;
using TaskManagementSystem.Backend.Application.Abstractions;
using TaskManagementSystem.Backend.Application.Models;
using TaskManagementSystem.Backend.Application.Models.DTOs;
using TaskManagementSystem.Backend.Application.Queries.UserQueries;
using TaskManagementSystem.Backend.Domain.Entities;

namespace TaskManagementSystem.Backend.Application.Queries.Handlers.UserHandlers
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, Result<IReadOnlyList<UserDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAllUsersHandler> _logger;

        public GetAllUsersHandler(IUnitOfWork unitOfWork, ILogger<GetAllUsersHandler> logger)
        {
            this._unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<Result<IReadOnlyList<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var repo = _unitOfWork.Repository<User>();
                var users = await repo.GetAllAsync();

                // აქ დამჭირდა ტიპის განსაზღვრა იმიტორო მერე .Success-ში Implicit ად ვერ ქასთავდა რატომღაც და გადაქასთვას ბარემ აქვე გავნსაზღვრე.
                IReadOnlyList<UserDto> userDtos = users.Select(u => new UserDto() 
                { 
                    Id = u.Id, 
                    FirstName = u.FirstName, 
                    LastName = u.LastName,
                    Email = u.Email }).ToList();
                return Result<IReadOnlyList<UserDto>>.Success(userDtos);
            }
            catch (DbException ex)
            {
                this._logger.LogError(ex, "Something went wrong with the database.");
                return Result<IReadOnlyList<UserDto>>.InternalError<IReadOnlyList<UserDto>>();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Something went wrong.");
                return Result<IReadOnlyList<UserDto>>.InternalError<IReadOnlyList<UserDto>>();
            }
        }
    }
}
