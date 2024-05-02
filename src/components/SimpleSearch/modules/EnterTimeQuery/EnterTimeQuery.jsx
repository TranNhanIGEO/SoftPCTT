import { timeTypes } from "./timeTypes";
import { useSearchState } from "src/contexts/Search";

const EnterTimeQuery = ({ name }) => {
  const { whereClause } = useSearchState();
  const thisTimeType = timeTypes[whereClause[name]];
  const TimeType = thisTimeType?.component;
  return thisTimeType && <TimeType />;
};

export default EnterTimeQuery;
