using FluentValidation;

namespace Application.Features.ProjectTasks.Commands.CreateProjectTask;
public class CreateProjectTaskCommandValidator : AbstractValidator<CreateProjectTaskCommand>
{
    // This validator handles input validation (UX/Request integrity).
    // Business rules remain enforced within the Domain entities.
    public CreateProjectTaskCommandValidator()
    {
        RuleFor(v => v.Title)
        .NotEmpty().WithMessage("Title is required.")
        .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

        RuleFor(v => v.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

        RuleFor(v => v.DueDate)
            .Must(date => !date.HasValue || date.Value > DateTime.UtcNow)
            .WithMessage("The due date must be in the future.");
    }
}
