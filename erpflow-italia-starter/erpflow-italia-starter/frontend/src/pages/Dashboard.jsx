import { useEffect, useState } from "react";
import { api } from "../services/api";

export default function Dashboard() {
  const [data, setData] = useState(null);

  useEffect(() => {
    api.get("/reports/dashboard")
      .then((res) => setData(res.data))
      .catch(() => setData(null));
  }, []);

  const cards = [
    ["Products", data?.totalProducts ?? 0],
    ["Customers", data?.totalCustomers ?? 0],
    ["Orders", data?.totalOrders ?? 0],
    ["Invoices", data?.totalInvoices ?? 0],
    ["Low Stock", data?.lowStock ?? 0],
    ["Sales Total €", data?.salesTotal ?? 0]
  ];

  return (
    <section>
      <h1>Dashboard</h1>
      <p>Italian ERP warehouse overview for managers.</p>

      <div className="card-grid">
        {cards.map(([title, value]) => (
          <div className="card" key={title}>
            <h3>{title}</h3>
            <strong>{value}</strong>
          </div>
        ))}
      </div>
    </section>
  );
}
