# ERPFlow Italia

ERPFlow Italia is a portfolio-ready Italian ERP-style warehouse management system for SMEs.

## Stack

- Frontend: React.js
- Backend: ASP.NET Core Web API / C#
- Database: SQL Server
- ORM: Entity Framework Core
- Auth: JWT starter structure
- Cloud-ready: AWS S3, CloudFront, ECS Fargate, RDS SQL Server, ECR, CloudWatch

## Main modules

- Auth
- Products
- Warehouses
- Stock Movements
- Orders
- Customers
- Suppliers
- Invoices
- Reports

## Local development

### Backend

```bash
cd backend
dotnet restore
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
```

Backend runs on:

```text
https://localhost:7000
http://localhost:5000
```

### Frontend

```bash
cd frontend
npm install
npm run dev
```

Frontend runs on:

```text
http://localhost:5173
```

## Default workflow

1. Admin creates products and warehouses
2. Supplier sends goods
3. Warehouse worker registers incoming stock
4. Sales team creates customer order
5. System checks stock availability
6. Order is confirmed
7. Stock quantity is reduced
8. Invoice is generated
9. Manager views dashboard and reports
