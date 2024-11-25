
# Cafe and Employee Management System

This project provides a RESTful API to manage cafes and employees, including creating, updating, deleting, and retrieving both entities and their relationships.

---

## Prerequisites

Ensure you have the following installed on your system:

- .NET SDK (version 6.0 or later)
- SQL Server (for the database)
- Entity Framework Core tools (`dotnet-ef`)

---

## Setup Instructions

### 1. Clone the Repository
```bash
git clone <repository-url>
cd <repository-name>
```

### 2. Configure the Database
Update the `appsettings.json` file in the project with your database connection string:
```json
{
    "ConnectionStrings": {
        "DefaultConnection": "Server=<YOUR_SERVER>;Database=CafeDb;Trusted_Connection=True;MultipleActiveResultSets=true"
    }
}
```

### 3. Apply Database Migrations
Run the following commands to create the database schema:
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

## How to Compile and Run

### 1. Build the Project
Run the following command to compile the project:
```bash
dotnet build
```

### 2. Run the Application
Run the following command to start the application:
```bash
dotnet run
```

The API will be accessible at `http://localhost:5000` by default.

---

## API Endpoints

### Cafe Management
- **Create a Cafe**: `POST /cafe`
- **Update a Cafe**: `PUT /cafe/{id}`
- **Delete a Cafe**: `DELETE /cafe/{id}` (Deletes associated employees)

### Employee Management
- **Create an Employee**: `POST /employee`
- **Update an Employee**: `PUT /employee/{id}`
- **Delete an Employee**: `DELETE /employee/{id}`

---

## Testing
You can use tools like Postman or curl to test the endpoints. Ensure the database is set up and the application is running before testing.

---

## Contribution
Feel free to fork the repository and submit pull requests. Contributions are welcome!

---

## License
This project is licensed under the MIT License. See the LICENSE file for details.
