# ERPFlow

Modern ERP platform for SMEs with desktop app, ASP.NET Core API, SQL Server, warehouse management, stock control, orders, invoices, reporting, and multi-client integration support.

## Overview

ERPFlow is a modular ERP platform designed for small and medium-sized businesses that need a practical system for managing inventory, warehouses, customers, suppliers, orders, invoices, and operational reporting.

This repository contains both the desktop client and the full starter platform structure for backend and frontend development.

## Repository Structure

```text
ERPFlow/
├── erpflow-italia-desktop-starter/
│   └── desktop/
│       └── ERPFlowItalia.Desktop/
└── erpflow-italia-starter/
    └── erpflow-italia-starter/
        ├── backend/
        ├── frontend/
        └── database/
            └── sql-scripts/
```

## Included Projects

### Desktop Client

Path:

```text
erpflow-italia-desktop-starter/
```

Includes:

- WPF desktop application
- Dashboard
- Products
- Warehouses
- Stock
- Orders
- Customers
- Suppliers
- Invoices
- Reports

### Platform Starter

Path:

```text
erpflow-italia-starter/erpflow-italia-starter/
```

Includes:

- ASP.NET Core Web API backend
- React frontend starter
- SQL Server database scripts
- Entity Framework Core setup
- Seed data script

## Architecture

```text
Desktop App
Web App
Seller App
E-commerce / POS Integration
        |
        v
ASP.NET Core API
        |
        v
SQL Server
```

The backend acts as the single source of truth for:

- business rules
- stock validation
- order lifecycle
- invoice generation
- reporting
- future integrations

## Core Modules

- Authentication
- Customers
- Suppliers
- Products
- Warehouses
- Stock
- Orders
- Invoices
- Reports

## Tech Stack

### Desktop

- WPF
- C#
- .NET 8

### Backend

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Swagger / OpenAPI

### Frontend

- React
- Vite

## Local Development

### 1. Run the Backend

```bash
cd erpflow-italia-starter/erpflow-italia-starter/backend
dotnet restore
dotnet ef database update
dotnet run
```

Backend runs on:

```text
https://localhost:7000
http://localhost:5000
```

### 2. Run the Desktop App

```bash
cd erpflow-italia-desktop-starter/desktop/ERPFlowItalia.Desktop
dotnet restore
dotnet run
```

### 3. Run the Frontend

```bash
cd erpflow-italia-starter/erpflow-italia-starter/frontend
npm install
npm run dev
```

Frontend runs on:

```text
http://localhost:5173
```

## Database

The backend uses SQL Server.

To create and update the database:

```bash
cd erpflow-italia-starter/erpflow-italia-starter/backend
dotnet ef migrations add InitialCreate
dotnet ef database update
```

To seed demo data, run:

```text
erpflow-italia-starter/erpflow-italia-starter/database/sql-scripts/seed-data.sql
```

## Current Workflow

1. Create or select a customer
2. Create an order
3. Validate stock availability
4. Confirm the order
5. Reduce stock
6. Generate an invoice
7. Update invoice payment status
8. Review results in dashboard and reports

## Roadmap

- JWT authentication and authorization
- Role-based permissions by module
- DTO-based API contracts
- Improved validation and error handling
- Audit logs for key operations
- Web dashboard expansion
- Seller app support
- E-commerce integration
- POS / cashier integration

## Status

This repository contains a working ERP starter platform with desktop and backend integration, designed to be extended into a broader multi-client system.
