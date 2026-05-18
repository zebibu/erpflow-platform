import { useEffect, useState } from "react";
import { api } from "../services/api";
import DataTable from "../components/DataTable";

export default function Warehouses() {
  const [warehouses, setWarehouses] = useState([]);

  useEffect(() => {
    api.get("/warehouses")
      .then((res) => setWarehouses(res.data))
      .catch(() => setWarehouses([]));
  }, []);

  const rows = warehouses.map((w) => ({
    Name: w.name,
    City: w.city,
    Province: w.province,
    Address: w.address
  }));

  return (
    <section>
      <h1>Warehouses</h1>
      <p>Manage Magazzino Centrale and secondary storage locations.</p>
      <DataTable columns={["Name", "City", "Province", "Address"]} rows={rows} />
    </section>
  );
}
