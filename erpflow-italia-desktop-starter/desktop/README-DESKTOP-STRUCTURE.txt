Recommended final solution structure:

company-warehouse-system/
│
├── frontend/                 React web app
├── backend/                  ASP.NET Core Web API
├── desktop/
│   └── ERPFlowItalia.Desktop/ WPF desktop app
└── database/                 SQL scripts

The WPF app does not connect directly to SQL Server.
It connects to the backend API.

This is better because:
- security is better
- business logic stays in backend
- web and desktop apps use the same API
- future mobile app can also use the same API
