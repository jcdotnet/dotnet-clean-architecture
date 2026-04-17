using Application.Features.ProjectTasks.Queries.GetProjectTasks;
using Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Application.Common.Mappings;

[Mapper]
public partial class ProjectTaskMapper
{
    [MapperIgnoreSource(nameof(ProjectTask.CreatedAt))]
    public partial ProjectTaskDto ProjectTaskToDto(ProjectTask entity);
}
