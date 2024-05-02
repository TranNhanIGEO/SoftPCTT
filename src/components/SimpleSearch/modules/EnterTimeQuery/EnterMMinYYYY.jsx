import { useCallback, useEffect } from "react";
import Form from "src/components/interfaces/Form";
import config from "src/config";
import {
  useSearchState,
  useSearchDispatch,
  setWhereClauses,
} from "src/contexts/Search";

const EnterDDinYYYY = () => {
  const dispatch = useSearchDispatch();
  const { whereClause } = useSearchState();
  const configTimes = config.times;

  useEffect(() => {
    const hasStartTime = whereClause["startTime"];
    const hasEndTime = whereClause["endTime"];
    const hasOnlyTime = whereClause["onlyTime"];
    if (!hasStartTime || !hasEndTime || !hasOnlyTime) return;
    const { startTime, endTime, onlyTime, ...whereClauses } = whereClause;
    dispatch(setWhereClauses(whereClauses));
  }, [whereClause, dispatch]);

  const handleSelectMonth = useCallback(
    (e) => {
      const { name, value } = e.target;
      const monthName = whereClause?.[name]?.includes(value);
      monthName
        ? dispatch(
            setWhereClauses({
              ...whereClause,
              [name]: whereClause?.[name].filter((val) => val !== value),
            })
          )
        : dispatch(
            setWhereClauses({
              ...whereClause,
              [name]: [...whereClause?.[name], value],
            })
          );
    },
    [whereClause, dispatch]
  );

  const handleEnterYear = useCallback(
    (e) => {
      const { name, value } = e.target;
      dispatch(setWhereClauses({ ...whereClause, [name]: value }));
    },
    [whereClause, dispatch]
  );

  return (
    <div className="simple-var-where">
      <div className="simple-time-month">
        <h3>Tháng</h3>
        <div className="simple-month">
          {configTimes.months.map((time) => (
            <Form.Check
              key={time.id}
              type="checkbox"
              name="aFewMonth"
              id={time.id}
              label={time.value}
              value={time.value}
              onChange={handleSelectMonth}
              checked={whereClause["aFewMonth"]?.includes(time.value)}
            />
          ))}
        </div>
      </div>
      <div className="simple-time-year">
        <h3>Năm</h3>
        <Form.Control
          type="number"
          name="aYear"
          id="year"
          value={whereClause["aYear"] ?? ""}
          onChange={handleEnterYear}
        />
      </div>
    </div>
  );
};

export default EnterDDinYYYY;
