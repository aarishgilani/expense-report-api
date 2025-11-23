## Expense Report API

This is a basic ASP.NET Core MVC application for managing expense reports. It includes:

- Controllers for handling requests
- Models for representing data
- Views for displaying pages
- Entity Framework Core for data access

### Getting Started

1. Clone the repository.
2. Restore dependencies and build the project:
	```sh
	dotnet restore
	dotnet build
	```
3. Run the application:
	```sh
	dotnet run
	```
4. Access the Swagger UI at `https://localhost:5113/swagger` (or the port shown in your terminal) to explore and test the API endpoints.

### Project Structure

- `Controllers/` - MVC controllers
- `Models/` - Data models
- `Views/` - Razor views
- `Data/` - Database context and seeder
- `wwwroot/` - Static files (CSS, JS, etc.)

---
Feel free to modify and extend the project as needed.
