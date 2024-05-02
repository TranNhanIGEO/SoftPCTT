import { useCallback } from "react";
import { queryTypes } from "./interfaces/queryTypes";
import {
  setWhereClauses,
  useSearchState,
  useSearchDispatch,
} from "src/contexts/Search";
import Form from "src/components/interfaces/Form";

const EnterSecondQuery = ({ name, argument, onChange }) => {
  const { whereClause, valueSuggests } = useSearchState();
  const dispatch = useSearchDispatch();

  const thisQueryType = queryTypes[whereClause["queryType"] ?? "compareValue"];
  const QueryType = thisQueryType?.component;

  const handleQueryType = useCallback(
    (e) => {
      const { value } = e.target;
      const { secondWhere, fromValue, toValue, ...whereClauses } = whereClause;
      dispatch(setWhereClauses({ ...whereClauses, queryType: value }));
    },
    [whereClause, dispatch]
  );

  return (
    <div className="simple-var-where">
      <div className="simple-condition">
        {Object.keys(queryTypes).map((type) => (
          <Form.Check
            type="radio"
            key={type}
            id={type}
            label={queryTypes[type].text}
            value={type}
            checked={type === (whereClause["queryType"] ?? "compareValue")}
            onChange={handleQueryType}
            disabled={
              !valueSuggests[name]?.every((val) =>
                queryTypes[type].type.includes(typeof val["value"])
              )
            }
          />
        ))}
      </div>
      {thisQueryType && (
        <QueryType name={name} argument={argument} onChange={onChange} />
      )}
    </div>
  );
};

export default EnterSecondQuery;
