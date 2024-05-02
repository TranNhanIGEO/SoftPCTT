import Form from "src/components/interfaces/Form";
import { useSearchState } from "src/contexts/Search";

const EnterCompareValue = ({ name, argument, onChange }) => {
  const { whereClause, valueSuggests } = useSearchState();

  return (
    <div className="simple-value">
      <div className="simple-value-enter">
        <h3>Giá trị</h3>
        <Form.Suggestion
          id={argument}
          placeholder="Nhập nội dung cần tìm kiếm"
          disabled={!valueSuggests[name]}
          value={whereClause[argument] ?? ""}
          data={valueSuggests[name]}
          onChange={onChange}
        />
      </div>
    </div>
  );
};

export default EnterCompareValue;
