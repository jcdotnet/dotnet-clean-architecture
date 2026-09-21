# .NET Clean Architecture Blueprint

This is my personal blueprint for building applications with **ASP.NET Core and .NET 10**. 

It represents the structure I go for when a project's complexity justifies it.

### Architecture
* **Domain:** I use the Result Pattern for error handling, reserving exceptions only for truly unexpected system failures.
* **Application:** CQRS with MediatR to organize Commands and Queries, FluentValidation for input validation and Mapperly for mapping.
* **Infrastructure:** EF Core with SQL Server (LocalDB). I use EF Core Interceptors for automated auditing (created/modified dates), keeping this concern outside the application handlers. Note: I intentionally avoid adding a Repository layer on top of EF Core. For this project, that additional abstraction would not provide enough value to justify the extra complexity.
* **API:** ASP.NET Core Web API with extension methods that map Result objects to HTTP responses (200 OK, 400 Bad Request, 404 Not Found).

### Quality Assurance
* **Testing:** Unit Tests with xUnit and FluentAssertions, using NSubstitute for mocking and InMemoryDatabase for realistic query testing.
* **CI/CD:** Basic GitHub Actions workflow to ensure every push builds and passes all tests.