Task Management System (ASP.NET Core Web API)
This repository implements a Task Management System built with ASP.NET Core Web API and EF Core.
The system supports managing Users, Projects, Tasks (TaskItem), and Comments with repository/unit-of-work patterns, FluentValidation,
global exception handling middleware and OpenAPI (Swagger).
Migrations for SQL Server are included.

Key features
•	Domain entities: User, Project, TaskItem, Comment
•	Full CRUD handlers implemented in the Application layer (MediatR handlers)
•	FluentValidation validators for commands and queries
•	Repository<T> and UnitOfWork abstractions
•	Pagination and filtering support in the repository and in user paging handler
•	Global exception handling middleware
•	Swagger/OpenAPI documentation
•	EF Core migrations included (Infrastructure/Migrations)
•	Unit test project with xUnit and Moq (partial coverage for User handlers)
Prerequisites
•	.NET 10 SDK
•	SQL Server (local or remote) or SQL Server Express
•	dotnet-ef (for applying migrations) — optional but recommended for local DB creation
•	Visual Studio 2022/2026 or VS Code
Repository layout (important folders)
•	TaskManagementSystem.Backend.Api — API project (Program.cs, Controllers)
•	TaskManagementSystem.Backend.Application — Application layer (Handlers, Commands, Queries, Validators)
•	TaskManagementSystem.Backend.Domain — Domain models and enums
•	TaskManagementSystem.Backend.Infrastructure — EF Core DbContext, migrations, repository implementation
•	TaskManagementSystem.Backend.Tests — Unit tests (xUnit + Moq). Note: currently contains user-handler tests; expand to cover all services.
Getting started (local)
1.	Restore and build
•	dotnet restore
•	dotnet build
2.	Apply database migrations (creates schema on configured SQL Server)
•	From repository root:
•	dotnet tool install --global dotnet-ef (if not installed)
•	dotnet ef database update --project TaskManagementSystem.Backend.Infrastructure --startup-project TaskManagementSystem.Backend.Api
•	Alternatively apply SQL from the Migrations folder manually.
3.	Run the API
•	dotnet run --project TaskManagementSystem.Backend.Api
•	Open the Swagger/OpenAPI UI:
•	In development the API config exposes OpenAPI. The project maps an OpenAPI json at /openapi/v1.json and registers Swagger UI in development.
•	Typically browse to: https://localhost:{port}/swagger or the root path shown in the console.
Testing
•	Unit tests
•	dotnet test TaskManagementSystem.Backend.Tests
•	Current test coverage
•	User handlers have unit tests (success, not found, pagination). Tests for Project, TaskItem and Comment handlers and for validation failure and exception paths are still required to meet full assignment test requirements.
How to run and view Swagger locally
1.	Build and run:
•	dotnet run --project TaskManagementSystem.Backend.Api
2.	Open browser to:
•	https://localhost:{port}/ (Swagger UI)
•	https://localhost:{port}/openapi/v1.json (raw OpenAPI JSON)
