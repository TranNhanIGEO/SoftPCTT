import "./Home.css";
import { useEffect } from "react";
import { useSelector } from "react-redux";
import { useDispatch } from "react-redux";
import { MapProvider } from "react-map-gl";
import clsx from "clsx";
import useAxiosJWT from "src/hooks/useAxiosJWT";
import useToken from "src/hooks/useToken";
import { TabControl, ControlPanel, AttributeTable, Mapbox } from "./components";
import { visibleAttrTable, visibleCtrlPanel } from "src/stores/home";
import { apiRoleLists, apiShowUserRoles } from "src/services/global";
import { currentPage, setPage } from "src/stores/global";

const Home = () => {
  const axiosJWT = useAxiosJWT();
  const dispatch = useDispatch();
  const currentUser = useToken();
  const memberid = currentUser?.["nameidentifier"];
  const visibilityPanel = useSelector(visibleCtrlPanel);
  const visibilityTable = useSelector(visibleAttrTable);
  const selectedPage = useSelector(currentPage);

  useEffect(() => {
    const page = process.env.REACT_APP_HOMEPAGE;
    apiRoleLists({ page, axiosJWT, dispatch });
    apiShowUserRoles({ memberid, axiosJWT, dispatch });
  }, [memberid, axiosJWT, dispatch]);

  useEffect(() => {
    if (selectedPage) return;
    dispatch(setPage(process.env.REACT_APP_HOMEPAGE));
  }, [selectedPage, dispatch]);

  return (
    <main className="home">
      <MapProvider>
        <div className={clsx("mapbox")}>
          <Mapbox />
        </div>
        <div className={clsx("controlpanel", { active: visibilityPanel })}>
          <ControlPanel />
          <TabControl />
        </div>
        <div className={clsx("attributetable", { active: visibilityTable })}>
          <AttributeTable />
        </div>
      </MapProvider>
    </main>
  );
};

export default Home;
