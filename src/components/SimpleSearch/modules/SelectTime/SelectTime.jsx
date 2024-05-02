import { timeTypes } from "src/components/SimpleSearch/modules/EnterTimeQuery/timeTypes";
import { useSearchState } from "src/contexts/Search";
import Form from "src/components/interfaces/Form";

const SelectTime = ({ name, onChange }) => {
  const { whereClause } = useSearchState();

  return (
    <div className="simple-var-columns">
      <Form.Select
        name={name}
        value={whereClause[name] ?? ""}
        defaultValue="CHỌN THỜI GIAN"
        isSelected={!whereClause[name]}
        onChange={onChange}
      >
        {Object.keys(timeTypes).map((type) => (
          <option key={type} value={type}>
            {timeTypes[type].title}
          </option>
        ))}
      </Form.Select>
    </div>
  );
};

export default SelectTime;
