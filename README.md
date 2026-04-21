# .NET Clean Architecture Blueprint

This is my personal **blueprint** for building ASP.NET Core applications with Clean Architecture and .NET 10, used as my current standard since 2026.

### Architecture
* **Domain:** Rich entities with encapsulated business logic and no external dependencies.
* **Application:** CQRS implementation using **MediatR**, **FluentValidation** and source-generated mapping with **Mapperly**.
* **Infrastructure:** Data persistence handled via EF Core (I used **SQL Server** with **LocalDB** for this template) with **automated auditing** for creation dates using Interceptors..
* **API:** ASP.NET Core Web API (Controller-based) with global exception handling for **ProblemDetails** errors.

### Quality Assurance
* **Testing:** Unit Tests using **xUnit**, **NSubstitute** for isolation, and **FluentAssertions**.
* **CI/CD:** Automated build and test workflows via **GitHub Actions**.

Instead of a bloated template, I keep this one focused on the essentials to avoid unnecessary over-engineering, such as redundant repository layers.