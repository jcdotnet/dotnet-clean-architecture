using Application.Features.ProjectTasks.Commands.CreateProjectTask;
using Application.Features.ProjectTasks.Queries.GetProjectTaskById;
using Application.Features.ProjectTasks.Queries.GetProjectTasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectTaskDto>>> Get()
    {
        return Ok(await mediator.Send(new GetProjectTasksQuery()));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProjectTaskDto>> GetById(Guid id)
    {
        return Ok(await mediator.Send(new GetProjectTaskByIdQuery(id)));
    }
}