# Cafe and Employee Management System

This project provides a RESTful API to manage cafes and employees, including creating, updating, deleting, and retrieving both entities and their relationships.

---

## **Features**

### **Cafes**

- View a list of cafes with details (name, description, location, logo).
- Filter cafes by location.
- Add new cafes with validation (name, description, location, logo).
- Edit existing cafes with pre-filled data.
- Delete cafes with confirmation.

### **Employees**

- View employees filtered by cafe.
- Search employees by name.
- Add new employees with validation (name, email, phone, gender, assigned cafe).
- Edit existing employee records.
- Delete employees with confirmation.

---

## **Technologies Used**

### **Frontend**

- **React** (with functional components and hooks)
- **Material-UI** (for styling)
- **AgGrid** (for table management)
- **Axios** (for API communication)

### **Backend**

- **.NET 6** (ASP.NET Core)
- **Entity Framework Core** (for database interaction)
- **SQL Server** (Database)

---

## **Requirements**

### **Frontend**

- Node.js (v16 or later)
- npm or yarn

### **Backend**

- .NET 6 SDK
- SQL Server

---

## Setup Instructions

### 1. Clone the Repository

```bash
git clone <repository-url>
cd <repository-name>/Avepoint.Backend
```

### 2. Configure the Database

Update the `appsettings.json` file in the project with your database connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=<YOUR_SERVER>;Database=AvepointTest;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

### 3. Apply Database Migrations

Run the following `.sql` files to create the database schema:

```
<repository-name>/Avepoint.Backend/MigrationScript/\*.sql
```

---

## How to Compile and Run (Backend)

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

The API will be accessible at `http://localhost:5069` by default.

---

## How to Compile and Run (Frontend)

### 1. Navigate to the Frontend Directory

```bash
cd cd <repository-name>/Avepoint.Frontend
```

### 2: Install Dependencies

```bash
npm install
```

### 3: Configure Environment Variables

Configure the .env file if needed:

```env
VITE_API_URL=http://localhost:5069/api
```

### 4: Run the Frontend

```bash
npm run dev
```

The frontend will be running on http://localhost:5173.

---

## API Endpoints

### Cafe Management

- **Get a Cafe**: `GET /cafe`
- **Get a Cafe with location**: `GET /cafe?location=<value>`
- **Create a Cafe**: `POST /cafe`
- **Update a Cafe**: `PUT /cafe/{id}`
- **Delete a Cafe**: `DELETE /cafe/{id}` (Deletes associated employees)

### Employee Management

- **Get an Employee**: `GET /employee`
- **Create an Employee**: `POST /employee`
- **Update an Employee**: `PUT /employee/{id}`
- **Delete an Employee**: `DELETE /employee/{id}`

---

## Testing

You can use tools like Postman or curl to test the endpoints. Ensure the database is set up and the application is running before testing.
