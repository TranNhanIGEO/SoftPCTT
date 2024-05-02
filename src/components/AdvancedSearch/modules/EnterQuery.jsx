import { memo } from "react";
import { useSearchState } from "src/contexts/Search";
import Form from "src/components/interfaces/Form";
import { convertLanguages } from "src/tools/convertLanguages";

const EnterQuery = ({ layer, onChange }) => {
  const { whereClause } = useSearchState();

  return (
    <div className="advancedsearch-input">
      <Form.Group controlID="where">
        <Form.Label>SELECT * FROM {convertLanguages(layer)} WHERE:</Form.Label>
        <Form.Control
          asTextArea
          id="where"
          name="where"
          spellCheck={false}
          value={whereClause["where"]?.replaceAll("a.", "") ?? ""}
          onChange={onChange}
        />
      </Form.Group>
    </div>
  );
};

export default memo(EnterQuery);
