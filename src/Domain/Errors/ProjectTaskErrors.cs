using Domain.Abstractions;

namespace Domain.Errors;

public static class ProjectTaskErrors
{
    public static Error NotFound(Guid id) =>
       Error.NotFound("ProjectTask.NotFound", $"Task {id} not found.");
}
