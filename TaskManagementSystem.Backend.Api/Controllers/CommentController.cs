using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Backend.Api.Extensions;
using TaskManagementSystem.Backend.Application.Commands.CommentCommands;
using TaskManagementSystem.Backend.Application.Queries.CommentQueries;

namespace TaskManagementSystem.Backend.Api.Controllers
{
    [ApiController]
    [Route("/comment")]
    public class CommentController : ControllerBase
    {
        private IMediator mediator { get; set; }
        public CommentController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCommentCommand command)
        {
            var result = await this.mediator.Send(command);

            return this.GetActionResult(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await this.mediator.Send(new GetAllCommentsQuery());

            return this.GetActionResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await this.mediator.Send(new GetCommentByIdQuery() { Id = id });

            return this.GetActionResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCommentCommand command)
        {
            var result = await this.mediator.Send(command);

            return this.GetActionResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await this.mediator.Send(new DeleteCommentCommand() { Id = id });

            return this.GetActionResult(result);
        }
    }
}
