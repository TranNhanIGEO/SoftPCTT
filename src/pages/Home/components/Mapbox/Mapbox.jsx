import "./Mapbox.css";
import "mapbox-gl/dist/mapbox-gl.css";
import Map from "react-map-gl";
import { useCallback } from "react";
import { useDispatch } from "react-redux";
import useAxiosJWT from "src/hooks/useAxiosJWT";
import config from "src/config";
import { toggleLoading } from "src/stores/global";
import { apiDistrictData, apiIslandData } from "src/services/home";
import {
  MapTools,
  ZoomBound,
  SourceLayer,
  MapPopup,
  MapRoute,
  MapVisualization,
} from "./modules";

const Mapbox = () => {
  const dispatch = useDispatch();
  const axiosJWT = useAxiosJWT();
  const configData = config.data;

  const initMapView = {
    longitude: 106.6885,
    latitude: 10.7755,
    zoom: 9,
    minZoom: 1,
    maxZoom: 18,
    maxBounds: configData.fullZoneZoom,
  };

  const initMapStyle = {
    width: "100%",
    height: "100%",
  };

  const handleLoadMap = useCallback(() => {
    apiDistrictData({ axiosJWT, dispatch });
    apiIslandData({ axiosJWT, dispatch });
    dispatch(toggleLoading(false));
  }, [axiosJWT, dispatch]);

  return (
    <Map
      id="mapbox"
      mapboxAccessToken={process.env.REACT_APP_MAPBOX_TOKEN}
      mapStyle={process.env.REACT_APP_MAPBOX_STREETMAP}
      initialViewState={initMapView}
      style={initMapStyle}
      attributionControl={false}
      hash={false}
      language="vi"
      logoPosition="bottom-right"
      onLoad={handleLoadMap}
    >
      <MapTools />
      <SourceLayer />
      <ZoomBound />
      <MapRoute />
      <MapPopup />
      <MapVisualization />
    </Map>
  );
};

export default Mapbox;
