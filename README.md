# 📦 OrdersApi

A simple **.NET 9 Web API** to manage orders.  
Supports creating, reading, updating, and deleting orders with clean response DTOs.

---

## 🚀 Features
- **Create Order** – Add a new order
- **Get Orders** – Retrieve all orders with their IDs
- **Update Order** – Modify an existing order by ID
- **Delete Order** – Remove an order by ID
- Clean API responses with DTOs (no raw strings exposed)

---

## 🛠 Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0) installed
- Any REST client (e.g., [Postman](https://www.postman.com/) or `curl`)

---

## ▶️ Run Locally

```bash
# Restore dependencies
dotnet restore

# Build the project
dotnet build

# Run the API
dotnet run --project OrdersApi
```

The API will be available at:

```
http://localhost:5000
https://localhost:7000
```

---

## 📖 API Endpoints

### 1. 📋 Get all orders
```http
GET /api/orders
```

**Response:**
```json
[
  {
    "id": 1,
    "name": "Coffee Order",
    "createdAt": "2025-08-22T14:05:32Z"
  }
]
```

---

### 2. ✏️ Create a new order
```http
POST /api/orders
Content-Type: application/json

{
  "name": "Bread Order"
}
```

**Response:**
```json
{
  "id": 2,
  "name": "Bread Order",
  "createdAt": "2025-08-22T14:06:12Z"
}
```

---

### 3. ✏️ Update an order
```http
PUT /api/orders/{id}
Content-Type: application/json

{
  "name": "Updated Bread Order"
}
```

**Response:**
```json
{
  "id": 2,
  "name": "Updated Bread Order",
  "createdAt": "2025-08-22T14:06:12Z"
}
```

---

### 4. ❌ Delete an order
```http
DELETE /api/orders/{id}
```

**Response:**
```json
{
  "message": "Order deleted successfully"
}
```

---

## 🧪 Example using curl

```bash
# Create an order
curl -X POST http://localhost:5000/api/orders \
  -H "Content-Type: application/json" \
  -d '{"name":"Coffee Order"}'

# Get all orders
curl http://localhost:5000/api/orders
```

---

## 📂 Project Structure

```
OrdersApi/
├── Controllers/
│   └── OrdersController.cs   # API logic
├── Models/
│   └── Order.cs              # Order entity
│   └── OrderResponse.cs      # DTO for responses
├── Program.cs                 # Application entrypoint
└── README.md                  # Documentation
```

