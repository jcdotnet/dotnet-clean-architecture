# .NET Clean Architecture Blueprint

This is my personal project structure for building applications with **ASP.NET Core and .NET 10**.

It's the approach I use when a project's complexity or requirements justify it.

### Architecture
* **Domain:** Result Pattern for error handling, reserving exceptions only for unexpected failures.
* **Application:** CQRS with MediatR, FluentValidation for request validation, a pipeline behavior to run validators and Mapperly for mapping.
* **Infrastructure:** EF Core with SQL Server (LocalDB). I use EF Core Interceptors for automated auditing (created/modified dates), keeping this logic outside the application handlers. Note: I intentionally avoid adding a Repository layer on top of EF Core. For this project, that additional abstraction would not provide enough value to justify the extra complexity.
* **API:** ASP.NET Core Web API with extension methods that map Result objects to HTTP responses (200 OK, 400 Bad Request, 404 Not Found).

### Hexagonal Architecture

This branch explores the transition to Hexagonal Architecture. 

Since the project already followed many of its principles, I decided to keep the existing structure and isolate EF Core behind an outbound port in the Application layer, with its implementation in the Infrastructure adapters folder.

No other changes were needed.

### DDD & Validation

I chose to use a simple DDD pattern to protect the `ProjectTask` entity, with a private constructor and a `Create()` factory method that returns the created entity on success or an error if the title is missing.

This is just an example of how domain validation can be implemented. The validation rules should be adapted to the needs of each project.

I use FluentValidation to validate requests in the Application layer, while domain validation protects the entity's rules. Some validations may exist in both places, depending on the requirements.

Not every project needs a rich domain model or the same level of DDD. Feel free to use DDD in yours when it makes sense, or keep things simple if your project doesn't need it.

### Quality Assurance
* **Testing:** Unit tests with xUnit and FluentAssertions, using NSubstitute for mocking and EF Core InMemory for query testing.
* **CI/CD:** Basic GitHub Actions workflow to ensure every push builds and passes all tests.