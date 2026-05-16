# Contacts Manager

Professional, layered Contacts Manager demonstrating separation of concerns with distinct Data Access (DAL), Business Logic (BLL), and UI projects. Built on .NET Framework 4.8 using ADO.NET for straightforward, testable data access patterns.

## Highlights

- Layered architecture: ContactsManagerDAL, ContactsManagerBLL, ContactsManagerUI
- ADO.NET-based data access (simple and explicit SQL/DbConnection usage)
- Targets .NET Framework 4.8 — compatible with Visual Studio
- Clean structure for learning, extension, and CI/CD integration

## Table of Contents

- [Requirements](#requirements)
- [Getting Started](#getting-started)
- [Configuration](#configuration)
- [Project Structure](#project-structure)
- [Development Notes](#development-notes)
- [Contributing](#contributing)
- [License](#license)

## Requirements

- Visual Studio 2019/2022/2026 (or later) with .NET desktop development workload
- .NET Framework 4.8
- (Optional) SQL Server or a local database compatible with the DAL implementation

## Getting Started

1. Clone the repository:

   git clone <your-repo-url>

2. Open the solution in Visual Studio (ContactsManager.sln).

3. Restore NuGet packages (Visual Studio should prompt automatically; or: `Tools  NuGet Package Manager  Restore`).

4. Build the solution (Build  Build Solution).

5. Set `ContactsManagerUI` as the startup project and run.

## Configuration

The DAL reads connection information from configuration. Do NOT commit production credentials.

Example (app.config or user-provided configuration):

```
<connectionStrings>
  <add name="ContactsDb" connectionString="Server=.;Database=Contacts;Integrated Security=True;" />
</connectionStrings>
```

Recommendations:

- Use environment variables or user secrets for sensitive values.
- Update the connection string to point to your local or CI database before running.

## Project Structure

- ContactsManagerDAL  Data Access Layer (repositories, models)
- ContactsManagerBLL  Business Logic Layer (services, view models)
- ContactsManagerUI   Console/WinForms/WPF UI entry point (Program.cs)

## Development Notes

- Focus is on clarity and separation of concerns rather than advanced ORMs.
- Follow existing patterns when adding features: DAL exposes repository interfaces, BLL consumes repositories, UI uses BLL services.
- Keep database access code testable by injecting repository interfaces where appropriate.

## Contributing

Contributions are welcome. Please follow these steps:

1. Fork the repository.
2. Create a feature branch (git checkout -b feature/your-feature).
3. Commit your changes with clear messages.
4. Open a pull request describing the change.

