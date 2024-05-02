import { Fragment } from "react";
import { tabPanels } from "src/pages/Home/components/ControlPanel/modules/tabPanels";

const PanelContent = ({ currentTab }) => {
  return (
    <Fragment>
      {tabPanels
        .filter((tab) => tab.value === currentTab)
        .map((tab) => (
          <div key={tab.value} className="controlpanel-content">
            {tab.content}
          </div>
        ))}
    </Fragment>
  );
};

export default PanelContent;
