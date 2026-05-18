# ERPFlow Italia Desktop Starter

This is the WPF desktop frontend version for ERPFlow Italia.

## Architecture

```text
WPF Desktop App
      ↓ HTTP API calls
ASP.NET Core Web API
      ↓
SQL Server
```

This desktop app is designed for an Italian warehouse / ERP-style company workflow.

## Modules included

- Dashboard
- Products
- Warehouses
- Stock
- Orders
- Customers
- Suppliers
- Invoices
- Reports

## Requirements

- Windows
- Visual Studio 2022
- .NET 8 SDK
- Backend API running from the ERPFlow Italia backend project

## How to run

1. First run your backend API:

```bash
cd backend
dotnet run
```

The backend should run on:

```text
https://localhost:7000
```

2. Then open this desktop app in Visual Studio:

```text
desktop/ERPFlowItalia.Desktop/ERPFlowItalia.Desktop.csproj
```

3. Press Start.

## Important

This starter desktop app uses the backend API URL:

```text
https://localhost:7000/api
```

You can change it in:

```text
Services/ApiClient.cs
```
