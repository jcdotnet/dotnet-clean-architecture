using Application.Common.Interfaces;
using Application.Features.ProjectTasks.Commands.CreateProjectTask;
using Domain.Enums;
using FluentAssertions;
using NSubstitute;

namespace Application.UnitTests.Features.ProjectTasks;
public class CreateProjectTaskTests
{

    private readonly IApplicationDbContext _context;
    private readonly CreateProjectTaskCommandHandler _handler;

    public CreateProjectTaskTests()
    {
        // Arrange: Mocking the database context
        _context = Substitute.For<IApplicationDbContext>();
        _handler = new CreateProjectTaskCommandHandler(_context);
    }

    [Fact]
    public async Task Handle_ValidRequest_ShouldReturnGuid()
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
        result.Should().NotBeEmpty();

        // Verify that the changes were actually committed to the database
        await _context.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }
}
