# 📦 OrdersApi

This is a .NET 9 Web API project running on OpenShift with PostgreSQL.  
It supports full CRUD operations: **Create, Read, Update, Delete**.

---

## 🚀 Features
- **Create Order** – Add a new order
- **Get Orders** – Retrieve all orders with their IDs
- **Update Order** – Modify an existing order by ID
- **Delete Order** – Remove an order by ID
- Clean API responses with DTOs (no raw strings exposed)

---

## 🛠 Prerequisites
- .NET 9 SDK
- PostgreSQL database
- OpenShift cluster
- `oc` CLI installed

---

## ▶️ Run Locally

```bash
# Restore dependencies
dotnet restore

# Build the project
dotnet build

# Run the API
dotnet run

# The API will be available at:
http://localhost:5000
https://localhost:7000

```
---
## ▶️ Deploying on OpenShift
### Deploying Application
```bash
#Create a new project:
oc new-project demo-test

# Create the application using the Red Hat UBI .NET 9 image:
oc new-app --docker-image=registry.access.redhat.com/ubi8/dotnet-90:latest~https://github.com/ng-rh/OrdersApi.git --name=orders-api

# Expose the service to create a route:
oc expose service/orders-api --path /api/orders

# Get the route URL:
oc get route orders-api -o jsonpath='{.spec.host}'

# Base URL for API: 
http://<your-app-route>/api/orders
```
### Create Database and update Environment variable:
```bash
# Create Postgresql database from openshift dashboard and give below values:
Name=postgresql
POSTGRES_PORT=5432POSTGRES_DB=ordersdb
POSTGRES_USER=demo
POSTGRES_PASSWORD=demo

# Set these values as environment variables for your PostgreSQL connection in application.:
oc set env deployment/orders-api \
  POSTGRES_HOST=postgresql \
  POSTGRES_PORT=5432 \
  POSTGRES_DB=ordersdb \
  POSTGRES_USER=demo \
  POSTGRES_PASSWORD=demo

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

### 4. ❌ Delete an order
```http
DELETE /api/orders/{id}
```

**Response:**
```json
{
  "id": 2,
  "name": "Updated Bread Order",
  "createdAt": "2025-08-22T14:06:12Z"
}
```

## 🧪 Example using curl
### 1. Create a new order (POST)
```http
curl -X POST \
  -H "Content-Type: application/json" \
  -d '{"name":"New Order"}' \
  http://<your-app-route>/api/orders
```
### 2. List all orders (GET)
```http
curl -X GET http://<your-app-route>/api/orders
```

### 3. Update an order (PUT)
```http
curl -X PUT \
  -H "Content-Type: application/json" \
  -d '{"name":"Updated Order"}' \
  http://<your-app-route>/api/orders/1
```
### 4. Delete an order (DELETE)
```http
curl -X DELETE \
  http://<your-app-route>/api/orders/1
Replace 1 with the actual order ID.
```
---

## 🐘 PostgreSQL Verification
```bash
# Connect to the database:
psql -h <POSTGRES_HOST> -U <POSTGRES_USER> -d <POSTGRES_DB>
example: psql -h localhost -p 5432 -U demo -d ordersdb

# List tables
\dt

# List all orders
SELECT * FROM "Orders";

# Insert an order manually
INSERT INTO "Orders" ("Name", "CreatedAt") VALUES ('Manual Order', NOW());

# Update an order manually
UPDATE "Orders" SET "Name" = 'Manual Update' WHERE "Id" = 1;

# Delete an order manually
DELETE FROM "Orders" WHERE "Id" = 1;
```