import Form from "src/components/interfaces/Form";
import { useSearchState } from "src/contexts/Search";

const EnterRangeValue = ({ name, onChange }) => {
  const { whereClause, valueSuggests } = useSearchState();

  return (
    <div className="simple-value">
      <div className="simple-value-enter">
        <h3>Từ</h3>
        <Form.Suggestion
          id="fromValue"
          placeholder="Nhập giá trị"
          disabled={!valueSuggests[name]}
          value={whereClause["fromValue"] ?? ""}
          data={valueSuggests[name]}
          onChange={onChange}
        />
      </div>
      <div className="simple-value-enter">
        <h3>Đến</h3>
        <Form.Suggestion
          id="toValue"
          placeholder="Nhập giá trị"
          disabled={!valueSuggests[name]}
          value={whereClause["toValue"] ?? ""}
          data={valueSuggests[name]}
          onChange={onChange}
        />
      </div>
    </div>
  );
};

export default EnterRangeValue;
