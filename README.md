# CareerCloud Project

CareerCloud is a layered architecture .NET solution designed for seamless and efficient data management. It integrates multiple technologies such as ADO.NET, Entity Framework, and gRPC to provide a comprehensive solution for business logic, data access, and communication.

## Project Overview

The solution is organized into **7 layers**, each with a specific role to ensure separation of concerns and maintainability:

1. **CareerCloud.ADODataLayer**
2. **CareerCloud.BusinessLogicLayer**
3. **CareerCloud.DataAccessLayer**
4. **CareerCloud.EFDataAccessLayer**
5. **CareerCloud.gRPC**
6. **CareerCloud.Pocos**
7. **CareerCloud.WebAPI**

---

## Layer Descriptions

### 1. **CareerCloud.ADODataLayer**
- **Purpose**: Handles all database interactions using ADO.NET.
- **Technology**: ADO.NET.

---

### 2. **CareerCloud.BusinessLogicLayer**
- **Purpose**: Implements business logic to validate and process data before interacting with the data access layers.
- **Key Features**:
  - Acts as a bridge between the presentation layer (WebAPI) and the data layers.
- **Technology**: Standard C# classes for logic implementation.

---

### 3. **CareerCloud.DataAccessLayer**
- **Purpose**: Provides interfaces and base classes for data access.
- **Key Features**:
  - Defines contracts for data access methods.
  - Acts as an abstraction layer between the business logic and data storage technologies (e.g., ADO.NET, EF).
- **Technology**: C# interfaces and classes.

---

### 4. **CareerCloud.EFDataAccessLayer**
- **Purpose**: Uses Entity Framework Core to perform database operations with a higher level of abstraction.
- **Key Features**:
  - Simplifies data access with LINQ queries.
- **Technology**: Entity Framework Core.

---

### 5. **CareerCloud.gRPC**
- **Purpose**: Implements gRPC services for efficient communication between distributed systems.
- **Key Features**:
  - Provides RPC methods to interact with CareerCloud's business logic.
  - Ensures fast, low-latency communication using Protocol Buffers.
- **Technology**: gRPC with Protocol Buffers.

---

### 6. **CareerCloud.Pocos**
- **Purpose**: Contains Plain Old CLR Objects (POCOs) representing database entities.
- **Key Features**:
  - Models every table in the database as a C# class.
  - Serves as a shared model for consistency across all layers.
- **Technology**: C# POCO classes.

---

### 7. **CareerCloud.WebAPI**
- **Purpose**: Exposes RESTful endpoints for the CareerCloud application.
- **Key Features**:
  - Integrates with the business logic layer to handle API requests.
  - Provides CRUD operations for various entities.
- **Technology**: ASP.NET Web API.

