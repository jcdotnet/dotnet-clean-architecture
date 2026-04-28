# .NET Clean Architecture Blueprint

This is my personal **blueprint** for building applications with **ASP.NET Core and .NET 10**. It represents the structure I go for when a project's complexity justifies it, and I'm sharing it as a reference for anyone who wants a solid starting point for their project.

### Architecture
* **Domain:** I use the **Result Pattern** for error handling, reserving exceptions only for truly unexpected system failures.
* **Application:** CQRS implementation using **MediatR**, **FluentValidation**, and **Mapperly** for mapping.
* **Infrastructure:** EF Core with **SQL Server (LocalDB)**. I use **Interceptors** for automated auditing (created/modified dates), keeping the Handlers focused purely on logic.
* **API:** ASP.NET Core Web API using custom extensions that map `Result` objects to HTTP responses (**200 OK**, **400 Bad Request**, **404 Not Found**).

Note: I intentionally avoided redundant layers like the Repository Pattern on top of EF Core to avoid over-engineering and keep the codebase lean.

### Quality Assurance
* **Testing:** Unit Tests with xUnit and FluentAssertions, using NSubstitute for mocking and InMemoryDatabase for realistic query testing.
* **CI/CD:** Basic GitHub Actions workflow to ensure every push builds and passes all tests.