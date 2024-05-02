import { memo } from "react";
import config from "src/config";
import { useSearchState } from "src/contexts/Search";

const SelectQuery = ({ onDBClick }) => {
  const { valueSuggests } = useSearchState();
  const configOperators = config.operators;

  return (
    <div className="advancedsearch-query">
      <div className="advancedsearch-operators">
        {configOperators.map((cols) => (
          <div key={cols.col} className="advancedsearch-operators-cols">
            {cols.operators.map((char) => (
              <button
                key={char.id}
                type="button"
                name="where"
                value={char.operator}
                onDoubleClick={onDBClick}
              >
                {char.operator}
              </button>
            ))}
          </div>
        ))}
      </div>
      <div className="advancedsearch-suggestions">
        <div className="advancedsearch-suggestions-options">
          {valueSuggests["where"]?.map((val) => (
            <button
              key={val["value"]}
              type="button"
              name="where"
              value={`'${val["value"]}'`}
              onDoubleClick={onDBClick}
            >
              {val["name"]}
            </button>
          ))}
        </div>
      </div>
    </div>
  );
};

export default memo(SelectQuery);
