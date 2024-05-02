import { memo } from "react";
import Form from "src/components/interfaces/Form";
import config from "src/config";
import { useSearchState } from "src/contexts/Search";

const SelectColumn = ({ name, onChange }) => {
  const { columnQuery, whereClause } = useSearchState();
  const configTables = config.table;

  return (
    <div className="simple-var-columns">
      <Form.Select
        name={name}
        value={whereClause[name] ?? ""}
        defaultValue="CHỌN THAM SỐ"
        isSelected={!whereClause[name]}
        onChange={onChange}
      >
        {columnQuery
          ?.filter(
            (col) => !configTables.dateColumn.includes(col.id.toLowerCase())
          )
          ?.map((col) => (
            <option key={col.id} value={col.id}>
              {col.label}
            </option>
          ))}
      </Form.Select>
    </div>
  );
};

export default memo(SelectColumn);
