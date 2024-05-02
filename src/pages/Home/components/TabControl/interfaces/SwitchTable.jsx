import { Fragment } from "react";
import clsx from "clsx";
import useScreen from "src/hooks/useScreen";
import { tabTables } from "src/pages/Home/components/AttributeTable/modules/tabTables";

const SwitchTable = ({ currentTab, onClick }) => {
  const screenSize = useScreen();

  return (
    <Fragment>
      {tabTables
        .filter((tab) => tab.condition?.some((cdt) => screenSize[cdt]()))
        .map((tab) => (
          <button
            key={tab.value}
            className={clsx("tabcontrol", {
              active: currentTab === tab.value,
            })}
            value={tab.value}
            onClick={onClick}
          >
            {tab.children}
          </button>
        ))}
    </Fragment>
  );
};

export default SwitchTable;
