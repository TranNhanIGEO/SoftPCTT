import { memo } from "react";
import { useSearchState } from "src/contexts/Search";

const SelectColumns = ({ onDBClick }) => {
  const { columnQuery } = useSearchState();

  return (
    <div className="advancedsearch-columns">
      <div className="advancedsearch-columns-options">
        {columnQuery?.map((form) => (
          <button
            key={form.id}
            type="button"
            name="where"
            value={form.id}
            onDoubleClick={onDBClick}
          >
            {form.id} --- {form.label}
          </button>
        ))}
      </div>
    </div>
  );
};

export default memo(SelectColumns);
