import { useCallback, useEffect, useState } from "react";
import {
  useSearchState,
  useSearchDispatch,
  setWhereClauses,
} from "src/contexts/Search";
import Form from "src/components/interfaces/Form";
import { timeTypes } from "./timeTypes";
import { queryTypes } from "./interfaces/queryTypes";

const EnterTimeValue = () => {
  const dispatch = useSearchDispatch();
  const { whereClause } = useSearchState();
  const [currentStatus, setCurrentStatus] = useState("compareTime");
  const thisTimeType = timeTypes[whereClause["timeType"]];
  const thisQueryType = queryTypes[currentStatus];
  const QueryType = thisQueryType?.component;

  useEffect(() => {
    const hasMonth = whereClause["aFewMonth"];
    const hasYear = whereClause["aYear"];
    if (!hasMonth || !hasYear) return;
    const { aFewMonth, aYear, ...whereClauses } = whereClause;
    dispatch(setWhereClauses(whereClauses));
  }, [whereClause, dispatch]);

  const handleChangeStatus = useCallback(
    (e) => {
      const { value } = e.target;
      setCurrentStatus(value);
      const { startTime, endTime, onlyTime, ...whereClauses } = whereClause;
      dispatch(setWhereClauses(whereClauses));
    },
    [whereClause, dispatch]
  );

  const handleCompareTime = useCallback(
    (name, val) => {
      if (!val?.["$y"]) return;
      const onCompare = thisTimeType.compare.onCompare;
      const date = onCompare(val);
      dispatch(setWhereClauses({ ...whereClause, [name]: date }));
    },
    [thisTimeType, whereClause, dispatch]
  );

  const handleRangeTime = useCallback(
    (name, val) => {
      if (!val?.["$y"]) return;
      const onRange = thisTimeType.range.onRange;
      const { startDate, endDate } = onRange(val);
      const date = name === "startTime" ? startDate : endDate;
      dispatch(setWhereClauses({ ...whereClause, [name]: date }));
    },
    [thisTimeType, whereClause, dispatch]
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
            checked={currentStatus === type}
            onChange={handleChangeStatus}
          />
        ))}
      </div>
      <div className="simple-value">
        <QueryType
          timeType={thisTimeType}
          onCompare={handleCompareTime}
          onRange={handleRangeTime}
        />
      </div>
    </div>
  );
};

export default EnterTimeValue;
