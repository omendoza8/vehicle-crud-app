# Vehicle CRUD Application

This project is a full-stack web application that allows users to create, read, update, and delete vehicle records. It is built using the latest technologies:

- **Backend:** .NET 8 (Web API)
- **Frontend:** Angular 19 (with standalone components and Angular Material)
- **Database:** SQL Server

## Project Structure

```
vehicle-crud-app/
├── backend/   (ASP.NET Core Web API using .NET 8)
└── frontend/  (Angular 19 app with standalone components)
```

## Main Features

- Full CRUD operations for vehicles
- Clean architecture with separation of concerns
- Repository and Unit of Work patterns
- UI components with Angular Material
- Snackbar notifications and form validation

## Backend Setup (ASP.NET Core)

**Prerequisites:**
- .NET 8 SDK installed
- SQL Server installed and configured (local or remote)

**Steps to run:**
1. Navigate to the backend folder:
    ```
    cd backend
    ```
2. Restore dependencies:
    ```
    dotnet restore
    ```
3. Configure the connection string in `appsettings.json` to point to your SQL Server instance.
4. Apply migrations to create the database:
    ```
    dotnet ef database update
    ```
5. Run the API:
    ```
    dotnet run
    ```
    The API will be available at: https://localhost:7003 or http://localhost:5050

## Frontend Setup (Angular 19)

**Prerequisites:**
- Node.js v18 or higher
- Angular CLI installed

**Steps to run:**
1. Navigate to the frontend folder:
    ```
    cd frontend
    ```
2. Install dependencies:
    ```
    npm install
    ```
3. Start the development server:
    ```
    ng serve
    ```
    Access the app at: http://localhost:4200

> **Note:** Make sure the backend is running for API calls to work.


# API Endpoint Configuration in the Frontend

In the Angular project, the base URL for the API is configured in the following file:

`frontend/vehicles-app/src/environments/environment.ts`

## Technologies Used

- **Frontend:** Angular 19, Angular Material
- **Backend:** ASP.NET Core (.NET 8), Entity Framework Core
- **Database:** SQL Server
- **Architecture:** REST API, Clean Architecture

## Git Ignore

```
node_modules/
dist/
bin/
obj/
*.mdf
*.ldf
.env
.vscode/
.DS_Store
```

## Author

Ovier Farid Mendoza Cruz