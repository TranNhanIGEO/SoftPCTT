import { NavigationControl } from "react-map-gl";

const MapTools = () => {
  return (
    <NavigationControl
      position="top-right"
      visualizePitch={false}
      showCompass={false}
      showZoom={true}
    />
  );
};

export default MapTools;
