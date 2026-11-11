using Api.Extensions;
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
    public async Task<IActionResult> Create(CreateProjectTaskCommand command)
    {
        var result = await mediator.Send(command); // Result<Guid>

        return result.ToActionResult();
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await mediator.Send(new GetProjectTasksQuery()); // Result<IEnumerable<ProjectTaskDto>>

        return result.ToActionResult();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await mediator.Send(new GetProjectTaskByIdQuery(id)); // Result<ProjectTaskDto>

        return result.ToActionResult();
    }
}