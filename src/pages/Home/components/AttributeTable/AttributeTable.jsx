import "./AttributeTable.css";
import { useSelector } from "react-redux";
import { currentTabTable } from "src/stores/home";
import { TableContent } from "./interfaces";
import { SearchProvider } from "src/contexts/Search";

const AttributeTable = () => {
  const tabTable = useSelector(currentTabTable);

  return (
    <div className="attributetable-container">
      <SearchProvider>
        <TableContent currentTab={tabTable} />
      </SearchProvider>
    </div>
  );
};

export default AttributeTable;
