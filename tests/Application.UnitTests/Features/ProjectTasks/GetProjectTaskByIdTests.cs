using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Features.ProjectTasks.Queries.GetProjectTaskById;
using Application.Features.ProjectTasks.Queries.GetProjectTasks;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Enums;
using FluentAssertions;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using NSubstitute;

namespace Application.UnitTests.Features.ProjectTasks;

public class GetProjectTaskByIdTests
{
    private readonly IApplicationDbContext _context;
    private readonly GetProjectTaskByIdQueryHandler _handler;

    public GetProjectTaskByIdTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options, Substitute.For<AuditableEntityInterceptor>());
        _handler = new GetProjectTaskByIdQueryHandler(_context, new ProjectTaskMapper());
    }

    [Fact]
    public async Task GetProjectTaskByIdQueryHandler_InvalidTaskId_ShouldReturnFailure()
    {
        // Arrange
        // Act
        var result = await _handler.Handle(new GetProjectTaskByIdQuery(Guid.NewGuid()), CancellationToken.None);

        // Assert
        var error = result.Value.Should().BeOfType<Error>().Which;

        error.ErrorType.Should().Be(ErrorType.NotFound);
    }

    [Fact]
    public async Task GetProjectTaskByIdQueryHandler_ValidRequest_ShouldReturnSuccess()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        var task = (ProjectTask)ProjectTask.Create("Task Name", "Description", PriorityLevel.Medium, null).Value!;

        typeof(ProjectTask).GetProperty(nameof(ProjectTask.Id))!.SetValue(task, taskId);

        _context.ProjectTasks.Add(task);
        await _context.SaveChangesAsync(CancellationToken.None);

        var query = new GetProjectTaskByIdQuery(taskId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        var dto = result.Value.Should().BeOfType<ProjectTaskDto>().Which;

        dto.Id.Should().Be(taskId);
    }

}
