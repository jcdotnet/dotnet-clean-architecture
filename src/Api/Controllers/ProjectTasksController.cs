using Application.Features.ProjectTasks.Commands.CreateProjectTask;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTasksController(ISender mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Guid>> Create(CreateProjectTaskCommand command)
        {
            var id = await mediator.Send(command);
            return Ok(id);
        }
    }
}