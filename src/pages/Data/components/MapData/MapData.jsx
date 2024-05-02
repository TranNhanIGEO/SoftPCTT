import "mapbox-gl/dist/mapbox-gl.css";
import "./MapData.css";
import Map from "react-map-gl";
import { useRef } from "react";
import config from "src/config";
import { MapTools, MapDraw } from "./modules";

const MapData = (props) => {
  const mapRef = useRef();
  const configData = config.data;

  const initMapView = {
    longitude: 106.6885,
    latitude: 10.7755,
    zoom: 9,
    minZoom: 1,
    maxZoom: 22,
    maxBounds: configData.fullZoneZoom,
  };

  const initMapStyle = {
    width: "100%",
    height: "100%",
  };

  return (
    <Map
      id="mapdata"
      ref={mapRef}
      mapboxAccessToken={process.env.REACT_APP_MAPBOX_TOKEN}
      mapStyle={process.env.REACT_APP_MAPBOX_STREETMAP}
      initialViewState={initMapView}
      style={initMapStyle}
      attributionControl={false}
      hash={false}
      language="vi"
      logoPosition="bottom-right"
    >
      <MapTools />
      <MapDraw map={mapRef} {...props} />
    </Map>
  );
};

export default MapData;
