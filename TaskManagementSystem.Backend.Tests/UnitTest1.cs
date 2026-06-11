using FluentValidation;
using Moq;
using TaskManagementSystem.Backend.Application.Abstractions;
using TaskManagementSystem.Backend.Application.Commands.UserCommands;
using TaskManagementSystem.Backend.Application.Commands.Handlers.UserHandlers;
using TaskManagementSystem.Backend.Application.Queries.UserQueries;
using TaskManagementSystem.Backend.Application.Queries.Handlers.UserHandlers;
using TaskManagementSystem.Backend.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace TaskManagementSystem.Backend.Tests
{
    public class UserHandlersTests // NO AI WAS USED I SWEAR BATONO ARTUR!!
    {
        [Fact]
        public async Task CreateUserHandler_Success()
        {
            var mockUow = new Mock<IUnitOfWork>();
            var mockRepo = new Mock<IRepository<User>>();
            mockUow.Setup(u => u.Repository<User>()).Returns(mockRepo.Object);
            mockRepo.Setup(r => r.AddAsync(It.IsAny<User>())).Returns(Task.CompletedTask);
            mockUow.Setup(u => u.CommitAsync()).ReturnsAsync(1);

            var validator = new Mock<IValidator<CreateUserCommand>>();
            validator.Setup(v => v.ValidateAsync(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new FluentValidation.Results.ValidationResult());

            var logger = new Mock<ILogger<CreateUserHandler>>();

            var handler = new CreateUserHandler(mockUow.Object, logger.Object, validator.Object);

            var result = await handler.Handle(new CreateUserCommand { FirstName = "A", LastName = "B", Email = "a@b.com" }, CancellationToken.None);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task GetUserByIdHandler_NotFound()
        {
            var mockUow = new Mock<IUnitOfWork>();
            var mockRepo = new Mock<IRepository<User>>();
            mockUow.Setup(u => u.Repository<User>()).Returns(mockRepo.Object);
            mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((User?)null);

            var validator = new Mock<IValidator<GetUserByIdQuery>>();
            validator.Setup(v => v.ValidateAsync(It.IsAny<GetUserByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new FluentValidation.Results.ValidationResult());

            var logger = new Mock<ILogger<GetUserByIdHandler>>();

            var handler = new GetUserByIdHandler(mockUow.Object, logger.Object, validator.Object);

            var result = await handler.Handle(new GetUserByIdQuery { Id = Guid.NewGuid() }, CancellationToken.None);

            Assert.False(result.IsSuccess);
            Assert.Equal(Application.Models.OperationStatus.NOT_FOUND, result.OperationStatus);
        }

        [Fact]
        public async Task UpdateUserHandler_NotFound()
        {
            var mockUow = new Mock<IUnitOfWork>();
            var mockRepo = new Mock<IRepository<User>>();
            mockUow.Setup(u => u.Repository<User>()).Returns(mockRepo.Object);
            mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((User?)null);

            var validator = new Mock<IValidator<UpdateUserCommand>>();
            validator.Setup(v => v.ValidateAsync(It.IsAny<UpdateUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new FluentValidation.Results.ValidationResult());

            var logger = new Mock<ILogger<UpdateUserHandler>>();

            var handler = new UpdateUserHandler(mockUow.Object, logger.Object, validator.Object);

            var result = await handler.Handle(new UpdateUserCommand { Id = Guid.NewGuid() }, CancellationToken.None);

            Assert.False(result.IsSuccess);
            Assert.Equal(Application.Models.OperationStatus.NOT_FOUND, result.OperationStatus);
        }

        [Fact]
        public async Task DeleteUserHandler_Success()
        {
            var user = User.CreateUser("F", "L", "e@e.com");
            var mockUow = new Mock<IUnitOfWork>();
            var mockRepo = new Mock<IRepository<User>>();
            mockUow.Setup(u => u.Repository<User>()).Returns(mockRepo.Object);
            mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(user);
            mockRepo.Setup(r => r.Remove(It.IsAny<User>()));
            mockUow.Setup(u => u.CommitAsync()).ReturnsAsync(1);

            var validator = new Mock<IValidator<DeleteUserCommand>>();
            validator.Setup(v => v.ValidateAsync(It.IsAny<DeleteUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new FluentValidation.Results.ValidationResult());

            var logger = new Mock<ILogger<DeleteUserHandler>>();

            var handler = new DeleteUserHandler(mockUow.Object, logger.Object, validator.Object);

            var result = await handler.Handle(new DeleteUserCommand { Id = user.Id }, CancellationToken.None);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task GetAllUsersHandler_ReturnsList()
        {
            var users = new List<User>
            {
                User.CreateUser("A","B","a@b.com"),
                User.CreateUser("C","D","c@d.com")
            };

            var mockUow = new Mock<IUnitOfWork>();
            var mockRepo = new Mock<IRepository<User>>();
            mockUow.Setup(u => u.Repository<User>()).Returns(mockRepo.Object);
            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(users);

            var logger = new Mock<ILogger<GetAllUsersHandler>>();

            var handler = new GetAllUsersHandler(mockUow.Object, logger.Object);

            var result = await handler.Handle(new GetAllUsersQuery(), CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(2, result.Value!.Count);
        }

        [Fact]
        public async Task GetUserPageHandler_Pagination()
        {
            var users = Enumerable.Range(0, 10).Select(i => User.CreateUser($"F{i}", $"L{i}", $"{i}@test.com")).ToList();

            var mockUow = new Mock<IUnitOfWork>();
            var mockRepo = new Mock<IRepository<User>>();
            mockUow.Setup(u => u.Repository<User>()).Returns(mockRepo.Object);
            mockRepo.Setup(r => r.GetPage(1, 4, It.IsAny<System.Linq.Expressions.Expression<Func<User,bool>>>()))
                .ReturnsAsync(users.Take(4).ToList());
            mockRepo.Setup(r => r.GetTotalCount(It.IsAny<System.Linq.Expressions.Expression<Func<User,bool>>>()))
                .ReturnsAsync(10);

            var validator = new Mock<IValidator<GetUserPageQuery>>();
            validator.Setup(v => v.ValidateAsync(It.IsAny<GetUserPageQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new FluentValidation.Results.ValidationResult());

            var logger = new Mock<ILogger<GetUserPageHandler>>();

            var handler = new GetUserPageHandler(mockUow.Object, logger.Object, validator.Object);

            var result = await handler.Handle(new GetUserPageQuery { Page = 1, ItemLimit = 4 }, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(1, result.Value!.currentPage);
            Assert.Equal(10 / 4, result.Value.totalPages);
            Assert.Equal(4, result.Value.Users.Count);
        }
    }
}
