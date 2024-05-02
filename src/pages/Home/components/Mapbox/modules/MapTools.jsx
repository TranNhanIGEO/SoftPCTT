import { Fragment } from "react";
import { GeolocateControl, NavigationControl } from "react-map-gl";

const MapTools = () => {
  return (
    <Fragment>
      <NavigationControl
        position="top-right"
        visualizePitch={true}
        showCompass={true}
        showZoom={true}
      />
      <GeolocateControl
        position="top-right"
        showAccuracyCircle={true}
        showUserHeading={true}
        showUserLocation={true}
        trackUserLocation={true}
      />
    </Fragment>
  );
};

export default MapTools;
