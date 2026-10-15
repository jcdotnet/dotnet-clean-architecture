using Application.Features.ProjectTasks.Commands.CreateProjectTask;
using Application.Ports.Outbound;
using Domain.Entities;
using Domain.Enums;
using FluentAssertions;
using NSubstitute;

namespace Application.UnitTests.Features.ProjectTasks;
public class CreateProjectTaskTests
{

    private readonly IProjectTaskRepository _repository;
    private readonly CreateProjectTaskCommandHandler _handler;

    public CreateProjectTaskTests()
    {
        // Arrange: Mocking the repository
        _repository = Substitute.For<IProjectTaskRepository>();
        _handler = new CreateProjectTaskCommandHandler(_repository);
    }

    [Fact]
    public async Task CreateProjectTaskCommandHandler_InvalidNameRequest_ShouldReturnFailure()
    {
        // Arrange
        var command = new CreateProjectTaskCommand(
            "",
            "Validation Test",
            PriorityLevel.Medium,
            DateTime.UtcNow.AddDays(1));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Code.Should().Be("ProjectTask.TitleRequired");
        result.Error.ErrorType.Should().Be(ErrorType.Validation);
    }

    [Fact]
    public async Task CreateProjectTaskCommandHandler_ValidRequest_ShouldReturnGuid()
    {
        // Arrange
        var command = new CreateProjectTaskCommand(
            "Test Task",
            "Test Description",
            PriorityLevel.Medium,
            DateTime.UtcNow.AddDays(1));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeEmpty();

        // Verify that the repository receives the task to persist
        await _repository.Received(1).AddAsync(
            Arg.Is<ProjectTask>(task => task.Id == result.Value),
            Arg.Any<CancellationToken>()
        );
    }
}
