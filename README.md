# .NET Clean Architecture Blueprint

This is my personal **blueprint** for building ASP.NET Core applications with Clean Architecture and .NET 10, used as my current standard since 2026.

### Architecture
* **Domain:** Core entities and logic with no external dependencies.
* **Application:** CQRS implementation using **MediatR**, **FluentValidation** and automated mapping (DTO/Entity).
* **Infrastructure:** Data persistence handled via EF Core (I used **SQL Server** fwith **LocalDB** for this template).
* **API:** ASP.NET Core Web API (Controller-based)

### Quality Assurance
* **Testing:** Unit Tests using **xUnit**, **NSubstitute** for isolation, and **FluentAssertions**.
* **CI/CD:** Automated build and test workflows via **GitHub Actions**.

Instead of a bloated template, I keep this one focused on the essentials to avoid unnecessary over-engineering.