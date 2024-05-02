import "./ControlPanel.css";
import { useEffect } from "react";
import { useSelector, useDispatch } from "react-redux";
import { currentTabPanel, setViewRoleLists } from "src/stores/home";
import { getRoleLists } from "src/stores/global";
import { getViewLayers } from "src/utils/getViewLayers";
import { PanelContent } from "./interfaces";

const ControlPanel = () => {
  const dispatch = useDispatch();
  const tabPanel = useSelector(currentTabPanel);
  const [roleLists] = useSelector(getRoleLists);

  useEffect(() => {
    if (!roleLists?.length) return;
    const homeArgs = getViewLayers({
      pageType: process.env.REACT_APP_HOMEPAGE,
      roleLists: roleLists,
    });
    const homeRoleLists = homeArgs?.districts;
    dispatch(setViewRoleLists(homeRoleLists));
  }, [roleLists, dispatch]);

  return (
    <div className="controlpanel-container">
      <PanelContent currentTab={tabPanel} />
    </div>
  );
};

export default ControlPanel;
