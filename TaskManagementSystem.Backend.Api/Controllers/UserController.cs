using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Backend.Api.Extensions;
using TaskManagementSystem.Backend.Application.Commands;
using TaskManagementSystem.Backend.Application.Queries;

namespace TaskManagementSystem.Backend.Api.Controllers
{
    [ApiController]
    [Route("/User")]
    public class UserController : ControllerBase
    {
        private IMediator mediator { get; set; }
        public UserController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            var result = await this.mediator.Send(command);

            return this.GetActionResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await this.mediator.Send(new GetAllUsersQuery()); // სუფთა CQRS-ს რომ მივყვეთ საჭიროა, რომ ხელით შევქმნათ აქ, რადგანაც არაფრის გადმოცემაა საჭირო

            return this.GetActionResult(result);
        }

        [HttpGet("/{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await this.mediator.Send(new GetUserById() { Id = id });

            return this.GetActionResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]UpdateUserCommand command)
        {
            var result = await this.mediator.Send(command);

            return this.GetActionResult(result);
        }

        [HttpDelete("/{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await this.mediator.Send(new DeleteUserCommand() { Id = id});

            return this.GetActionResult(result);
        }
    }
}
