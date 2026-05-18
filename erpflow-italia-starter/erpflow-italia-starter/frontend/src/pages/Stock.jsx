import { useEffect, useState } from "react";
import { api } from "../services/api";
import DataTable from "../components/DataTable";

export default function Stock() {
  const [stock, setStock] = useState([]);

  useEffect(() => {
    api.get("/stock")
      .then((res) => setStock(res.data))
      .catch(() => setStock([]));
  }, []);

  const rows = stock.map((s) => ({
    Product: s.product,
    SKU: s.sku,
    Warehouse: s.warehouse,
    Quantity: s.quantity,
    Status: s.isLowStock ? "Low stock" : "OK"
  }));

  return (
    <section>
      <h1>Stock</h1>
      <p>Track current stock and low-stock products.</p>
      <DataTable columns={["Product", "SKU", "Warehouse", "Quantity", "Status"]} rows={rows} />
    </section>
  );
}
