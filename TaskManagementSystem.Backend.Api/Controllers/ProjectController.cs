using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Backend.Api.Extensions;
using TaskManagementSystem.Backend.Application.Commands.ProjectCommands;
using TaskManagementSystem.Backend.Application.Queries.ProjectQueries;

namespace TaskManagementSystem.Backend.Api.Controllers
{
    [ApiController]
    [Route("/project")]
    public class ProjectController : ControllerBase
    {
        private IMediator mediator { get; set; }
        public ProjectController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProjectCommand command)
        {
            var result = await this.mediator.Send(command);

            return this.GetActionResult(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await this.mediator.Send(new GetAllProjectsQuery());

            return this.GetActionResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await this.mediator.Send(new GetProjectByIdQuery() { Id = id });

            return this.GetActionResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProjectCommand command)
        {
            var result = await this.mediator.Send(command);

            return this.GetActionResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await this.mediator.Send(new DeleteProjectCommand() { Id = id });

            return this.GetActionResult(result);
        }
    }
}
