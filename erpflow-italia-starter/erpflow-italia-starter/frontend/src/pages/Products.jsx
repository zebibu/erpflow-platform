import { useEffect, useState } from "react";
import { api } from "../services/api";
import DataTable from "../components/DataTable";

export default function Products() {
  const [products, setProducts] = useState([]);

  useEffect(() => {
    api.get("/products")
      .then((res) => setProducts(res.data))
      .catch(() => setProducts([]));
  }, []);

  const rows = products.map((p) => ({
    SKU: p.sku,
    Name: p.name,
    Category: p.category,
    "Unit Price": `€${p.unitPrice}`,
    "VAT %": p.vatRate,
    "Min Stock": p.minimumStockLevel
  }));

  return (
    <section>
      <h1>Products</h1>
      <p>Manage company products, SKUs, prices, VAT, and minimum stock level.</p>
      <DataTable columns={["SKU", "Name", "Category", "Unit Price", "VAT %", "Min Stock"]} rows={rows} />
    </section>
  );
}
