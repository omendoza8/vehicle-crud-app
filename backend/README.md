# CrudVehiclesApp.Api

A simple ASP.NET Core Web API for managing vehicles using Entity Framework Core and SQL Server.

## Features

- CRUD operations for vehicles (Create, Read, Update, Delete)
- RESTful API endpoints
- Entity Framework Core with SQL Server
- Swagger/OpenAPI documentation

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server instance

### Setup

1. **Clone the repository:**
2. **Configure the database connection:**
   - Update the `DefaultConnection` string in `appsettings.json` with your SQL Server details.

3. **Apply database migrations:**
4. **Run the application:**
5. **Access Swagger UI:**
   - Navigate to `https://localhost:<port>/swagger` in your browser.

## API Endpoints

- `GET /api/vehicles` - Get all vehicles
- `GET /api/vehicles/{id}` - Get a vehicle by ID
- `POST /api/vehicles` - Create a new vehicle
- `PUT /api/vehicles/{id}` - Update an existing vehicle
- `DELETE /api/vehicles/{id}` - Delete a vehicle

## Project Structure

- `Controllers/` - API controllers
- `Application/` - DTOs, interfaces, and services
- `Domain/` - Entity and repository interfaces
- `Infrastructure/` - EF Core DbContext and repository implementations
- `Migrations/` - EF Core migrations