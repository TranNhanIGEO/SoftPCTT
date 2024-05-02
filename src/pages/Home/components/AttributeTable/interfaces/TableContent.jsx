import { Fragment } from "react";
import { tabTables } from "src/pages/Home/components/AttributeTable/modules/tabTables";

const TableContent = ({ currentTab }) => {
  return (
    <Fragment>
      {tabTables
        .filter((tab) => tab.value === currentTab)
        .map((tab) => (
          <div key={tab.value} className="attributetable-content">
            {tab.content}
          </div>
        ))}
    </Fragment>
  );
};

export default TableContent;
