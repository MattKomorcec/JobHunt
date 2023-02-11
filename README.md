# Jobhunt

Jobhunt is a web application that helps users keep track of their job applications. It allows users to store and organize information about jobs they have applied to, including the company name, job title, application date, location, salary, benefits, and any notes they want to add.

## Features

- Create, edit, and delete job applications
- View a list of all job applications with sorting and filtering options
- Search for job applications by keywords
- View job application statistics, such as the number of successful applications
- User authentication and authorization

## Technologies

- C# .NET 7
- ASP.NET Core MVC
- Entity Framework Core
- PostgreSQL
- HTML, CSS, JavaScript
- Identity

## Setup

1. Clone the repository to your local machine.
2. Open the solution in Visual Studio or your preferred IDE.
3. Create a new database called `jobhunt` in your PostgreSQL instance
3. Set up the database by running the following commands in the Package Manager Console:

```sql
Update-Database           -- in Visual Studio
dotnet ef database update -- in any other terminal

```

4. Run the project and navigate to https://localhost:5001 in your browser.
```
dotnet run
```
