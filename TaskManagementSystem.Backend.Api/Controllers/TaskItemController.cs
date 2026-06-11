using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Backend.Api.Extensions;
using TaskManagementSystem.Backend.Application.Commands.TaskItemCommands;
using TaskManagementSystem.Backend.Application.Queries.TaskItemQueries;

namespace TaskManagementSystem.Backend.Api.Controllers
{
    [ApiController]
    [Route("/taskitem")]
    public class TaskItemController : ControllerBase
    {
        private IMediator mediator { get; set; }
        public TaskItemController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskItemCommand command)
        {
            var result = await this.mediator.Send(command);

            return this.GetActionResult(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await this.mediator.Send(new GetAllTaskItemsQuery());

            return this.GetActionResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await this.mediator.Send(new GetTaskItemByIdQuery() { Id = id });

            return this.GetActionResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateTaskItemCommand command)
        {
            var result = await this.mediator.Send(command);

            return this.GetActionResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await this.mediator.Send(new DeleteTaskItemCommand() { Id = id });

            return this.GetActionResult(result);
        }
    }
}
