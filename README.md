# .NET 10 Clean Architecture Blueprint

A professional, production-ready template designed for **.NET 10**. This repository focuses on modern architectural patterns, SOLID principles, and high-performance development to ensure scalability, maintainability, and high testability.

## 🏗️ Architectural Layers

- **Domain**: Core business logic, entities, and enums. Pure C# with no external dependencies.
- **Application**: Orchestrates business rules using **CQRS (MediatR)**, **FluentValidation**, and automated mapping.
- **Infrastructure**: Handles data persistence via **Entity Framework Core** and **SQL Server**, implementing the Unit of Work pattern.
- **Web API**: The interface layer. Built with **ASP.NET Core Controllers** for structured request handling, custom middlewares, and OpenAPI documentation.

## 🛠️ Technical Stack

- **Framework**: .NET 10+
- **ORM**: Entity Framework Core
- **Patterns**: DDD (Domain-Driven Design), CQRS, Repository Pattern.
- **Mapping**: **Mapperly** (Compile-time mapping for maximum performance).
- **Validation**: Automatic pipeline validation with **FluentValidation**.
- **API Style**: ASP.NET Core Web API (Controller-based)
- **Observability**: Structured logging with **Serilog** and request telemetry.
- **Database**: SQL Server (LocalDB for easy setup).
- **Testing**: Unit Tests using **xUnit**, **NSubstitute**, and **FluentAssertions**.
- **CI/CD**: Automated build and test pipeline via **GitHub Actions**.

## 🚀 Getting Started

1. **Prerequisites**: Ensure you have the .NET 10 SDK installed.
2. **Database**: Update the connection string in `src/Api/appsettings.json` if needed.
3. **Migrations**: Run `dotnet ef database update` from the root (requires `dotnet-ef` tool).
4. **Run**: F5 or `dotnet run --project src/Api`. Swagger will open automatically at the root URL.

---
*Developed by [José Carlos Román Rubio](https://www.linkedin.com/in/romanrubio/) 