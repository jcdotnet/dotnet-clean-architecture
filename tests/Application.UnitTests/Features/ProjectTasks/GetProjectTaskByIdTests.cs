using Application.Common.Mappings;
using Application.Features.ProjectTasks.Queries.GetProjectTaskById;
using Application.Ports.Outbound;
using Domain.Entities;
using Domain.Enums;
using FluentAssertions;
using NSubstitute;

namespace Application.UnitTests.Features.ProjectTasks;

public class GetProjectTaskByIdTests
{
    private readonly IProjectTaskRepository _repository;
    private readonly GetProjectTaskByIdQueryHandler _handler;

    public GetProjectTaskByIdTests()
    {
        _repository = Substitute.For<IProjectTaskRepository>();
        _handler = new GetProjectTaskByIdQueryHandler(_repository, new ProjectTaskMapper());
    }

    [Fact]
    public async Task GetProjectTaskByIdQueryHandler_InvalidTaskId_ShouldReturnFailure()
    {
        // Arrange
        // Act
        var result = await _handler.Handle(new GetProjectTaskByIdQuery(Guid.NewGuid()), CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.ErrorType.Should().Be(ErrorType.NotFound);
    }

    [Fact]
    public async Task GetProjectTaskByIdQueryHandler_ValidRequest_ShouldReturnSuccess()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        var task = ProjectTask.Create("Task Name", "Task Description", PriorityLevel.Medium, null).Value!;

        typeof(ProjectTask).GetProperty(nameof(ProjectTask.Id))!.SetValue(task, taskId);

        _repository.GetByIdAsync(taskId, Arg.Any<CancellationToken>()).Returns(task);

        var query = new GetProjectTaskByIdQuery(taskId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value!.Id.Should().Be(taskId);
    }

}
