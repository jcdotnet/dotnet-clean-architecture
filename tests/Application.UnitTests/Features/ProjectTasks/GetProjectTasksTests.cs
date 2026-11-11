using Application.Common.Mappings;
using Application.Features.ProjectTasks.Queries.GetProjectTasks;
using Domain.Entities;
using Domain.Enums;
using FluentAssertions;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using NSubstitute;

namespace Application.UnitTests.Features.ProjectTasks;

public class GetProjectTasksTests
{
    private readonly ApplicationDbContext _context;
    private readonly GetProjectTasksQueryHandler _handler;

    public GetProjectTasksTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options, Substitute.For<AuditableEntityInterceptor>());
        _handler = new GetProjectTasksQueryHandler(_context, new ProjectTaskMapper());
    }

    [Fact]
    public async Task GetProjectTasksQueryHandler_WhenEmptyList_ShouldReturnSuccessWithEmptyList()
    {
        // Act
        var result = await _handler.Handle(new GetProjectTasksQuery(), CancellationToken.None);

        // Assert
        var tasks = (IEnumerable<ProjectTaskDto>)result.Value;

        tasks.Should().NotBeNull();
        tasks.Should().BeEmpty();
    }

    [Fact]
    public async Task GetProjectTasksQueryHandler_WhenTasksExist_ShouldReturnSuccessWithData()
    {
        // Arrange
        var task1 = (ProjectTask)ProjectTask.Create("Task1 Name", "Description", PriorityLevel.Low, null).Value!;
        var task2 = (ProjectTask)ProjectTask.Create("Task2 Name", "Description", PriorityLevel.High, null).Value!;

        _context.ProjectTasks.AddRange(task1, task2);
        await _context.SaveChangesAsync(CancellationToken.None);

        // Act
        var result = await _handler.Handle(new GetProjectTasksQuery(), CancellationToken.None);

        // Assert
        var tasks = (IEnumerable<ProjectTaskDto>)result.Value;

        tasks.Should().HaveCount(2);
        tasks.Should().Contain(t => t.Title == "Task1 Name");
    }
}
