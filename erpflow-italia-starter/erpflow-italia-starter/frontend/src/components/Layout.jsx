import { Outlet, NavLink } from "react-router-dom";

export default function Layout() {
  const links = [
    ["Dashboard", "/dashboard"],
    ["Products", "/products"],
    ["Warehouses", "/warehouses"],
    ["Stock", "/stock"],
    ["Orders", "/orders"],
    ["Customers", "/customers"],
    ["Suppliers", "/suppliers"],
    ["Invoices", "/invoices"],
    ["Reports", "/reports"]
  ];

  return (
    <div className="app-shell">
      <aside className="sidebar">
        <h2>ERPFlow Italia</h2>
        <p>Warehouse ERP</p>
        <nav>
          {links.map(([label, path]) => (
            <NavLink key={path} to={path}>{label}</NavLink>
          ))}
        </nav>
      </aside>

      <main className="main-content">
        <Outlet />
      </main>
    </div>
  );
}
