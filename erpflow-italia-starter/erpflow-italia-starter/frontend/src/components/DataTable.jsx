export default function DataTable({ columns, rows }) {
  return (
    <table className="data-table">
      <thead>
        <tr>
          {columns.map((col) => <th key={col}>{col}</th>)}
        </tr>
      </thead>
      <tbody>
        {rows.length === 0 && (
          <tr>
            <td colSpan={columns.length}>No data available</td>
          </tr>
        )}
        {rows.map((row, index) => (
          <tr key={index}>
            {columns.map((col) => <td key={col}>{row[col] ?? "-"}</td>)}
          </tr>
        ))}
      </tbody>
    </table>
  );
}
