import "./TabControl.css";
import { useMap } from "react-map-gl";
import { useDispatch, useSelector } from "react-redux";
import clsx from "clsx";
import { removeAllThematicData } from "src/stores/data";
import {
  SwitchStyle,
  SwitchPanel,
  TogglePanel,
  SwitchTable,
} from "./interfaces";
import {
  toggleControlPanel,
  visibleCtrlPanel,
  toggleAttributeTable,
  visibleAttrTable,
  setTabPanel,
  currentTabPanel,
  setTabTable,
  currentTabTable,
  removeAllBasicData,
  removeAllLayers,
} from "src/stores/home";

const TabControl = () => {
  const streetStyle = process.env.REACT_APP_MAPBOX_STREETMAP;
  const satelliteStyle = process.env.REACT_APP_MAPBOX_SATELLITEMAP;
  const { mapbox } = useMap();
  const mapRef = mapbox?.getMap();
  const dispatch = useDispatch();
  const visiblePanel = useSelector(visibleCtrlPanel);
  const visibleTable = useSelector(visibleAttrTable);
  const tabPanel = useSelector(currentTabPanel);
  const tabTable = useSelector(currentTabTable);

  const handleChangeTabPanel = (e) => {
    const { value } = e.target.parentNode;
    dispatch(setTabPanel(value));
  };

  const handleChangeLayer = () => {
    const currentStyle = mapRef.getStyle("mapbox");
    const isStreetStyle = currentStyle.name === "Mapbox Streets";
    mapRef.setStyle(isStreetStyle ? satelliteStyle : streetStyle);
    dispatch(removeAllThematicData());
    dispatch(removeAllBasicData());
    dispatch(removeAllLayers());
    mapRef.getStyle().layers?.forEach((lyr) => {
      if (!lyr.source?.includes("pctt")) return;
      mapRef.removeLayer(lyr.id);
      mapRef.removeSource(lyr.source);
    });
  };

  const handleTogglePanel = () => {
    dispatch(toggleControlPanel());
  };

  const handleChangeTabTable = (e) => {
    const { value } = e.target.parentNode;
    dispatch(toggleAttributeTable(true));
    dispatch(setTabTable(value));
  };

  return (
    <div className={clsx("tabcontrols", { minimize: visibleTable })}>
      <div className="mapboxgl-ctrl-custom">
        <div className="mapboxgl-ctrl-top-left">
          <div className="mapboxgl-ctrl mapboxgl-ctrl-group">
            <SwitchPanel currentTab={tabPanel} onClick={handleChangeTabPanel} />
          </div>
          <div className="mapboxgl-ctrl mapboxgl-ctrl-group">
            <SwitchStyle onClick={handleChangeLayer} />
            <TogglePanel isVisible={visiblePanel} onClick={handleTogglePanel} />
          </div>
        </div>
        <div className="mapboxgl-ctrl-bottom-left">
          <div className="mapboxgl-ctrl mapboxgl-ctrl-group">
            <SwitchTable currentTab={tabTable} onClick={handleChangeTabTable} />
          </div>
        </div>
      </div>
    </div>
  );
};

export default TabControl;
