using Application.Common.Mappings;
using Application.Features.ProjectTasks.Queries.GetProjectTasks;
using Application.Ports.Outbound;
using Domain.Entities;
using Domain.Enums;
using FluentAssertions;
using NSubstitute;

namespace Application.UnitTests.Features.ProjectTasks;

public class GetProjectTasksTests
{
    private readonly IProjectTaskRepository _repository;
    private readonly GetProjectTasksQueryHandler _handler;

    public GetProjectTasksTests()
    {
        _repository = Substitute.For<IProjectTaskRepository>();
        _handler = new GetProjectTasksQueryHandler(_repository, new ProjectTaskMapper());
    }

    [Fact]
    public async Task GetProjectTasksQueryHandler_WhenEmptyList_ShouldReturnSuccessWithEmptyList()
    {
        // Act
        var result = await _handler.Handle(new GetProjectTasksQuery(), CancellationToken.None);

        // Assert
        result.Value.Should().BeEmpty();
        result.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task GetProjectTasksQueryHandler_WhenTasksExist_ShouldReturnSuccessWithData()
    {
        // Arrange
        var task1 = ProjectTask.Create("Task 1 Name", "Task 1 Description", PriorityLevel.Low, null).Value!;
        var task2 = ProjectTask.Create("Task 2 Name", "Task 2 Description", PriorityLevel.High, null).Value!;

        _repository.GetAsync(Arg.Any<CancellationToken>()).Returns([task1, task2]);

        // Act
        var result = await _handler.Handle(new GetProjectTasksQuery(), CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().HaveCount(2);
        result.Value.Should().Contain(t => t.Title == "Task 1 Name");
    }
}
