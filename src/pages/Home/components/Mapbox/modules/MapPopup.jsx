import { Fragment, useEffect, useState } from "react";
import { useDispatch } from "react-redux";
import { Popup, useMap } from "react-map-gl";
import config from "src/config";
import { setCurrentLayer, setCurrentObjCoords } from "src/stores/home";

const MapPopup = () => {
  const { mapbox } = useMap();
  const mapRef = mapbox?.getMap();
  const dispatch = useDispatch();
  const [popupInfo, setPopupInfos] = useState({});
  const [isShowPopup, setIsShowPopup] = useState(false);
  const configPopup = config.popup;
  const [layers] = useState(Object.keys(configPopup).map((lyr) => [lyr]));

  useEffect(() => {
    const handleMouseMove = (e) => {
      layers.forEach((lyr) => {
        const [l] = lyr;
        const hasLayer = mapRef.getLayer(l);
        if (!hasLayer) return;
        const features = mapRef.queryRenderedFeatures(e.point, { layers: lyr });
        if (!features?.length) return;
        const [feature] = features;
        const { layer, geometry, properties } = feature;
        const id = layer.id;
        setIsShowPopup(true);
        setPopupInfos({ id, geometry, properties });
      });
    };
    mapRef.on("mousemove", handleMouseMove);
    return () => mapRef.off("mousemove", handleMouseMove);
  }, [mapRef, layers]);

  useEffect(() => {
    const handleMouseClick = (e) => {
      layers.forEach((lyr) => {
        const [l] = lyr;
        const hasLayer = mapRef.getLayer(l);
        if (!hasLayer) return;
        const features = mapRef.queryRenderedFeatures(e.point, { layers: lyr });
        if (!features?.length) return;
        const [feature] = features;
        const { layer, properties } = feature;
        const id = layer.id;
        dispatch(setCurrentLayer(id))
        dispatch(setCurrentObjCoords(properties));
      });
    };
    mapRef.on("dblclick", handleMouseClick);
    return () => mapRef.off("dblclick", handleMouseClick);
  }, [mapRef, layers, dispatch]);

  return (
    isShowPopup &&
    Object.keys(popupInfo).length && (
      <Popup
        longitude={
          popupInfo?.geometry?.coordinates?.[0]?.[0]?.[0]?.[0] ??
          popupInfo?.geometry?.coordinates?.[0]?.[0]?.[0] ??
          popupInfo?.geometry?.coordinates?.[0]?.[0] ??
          popupInfo?.geometry?.coordinates?.[0]
        }
        latitude={
          popupInfo?.geometry?.coordinates?.[0]?.[0]?.[0]?.[1] ??
          popupInfo?.geometry?.coordinates?.[0]?.[0]?.[1] ??
          popupInfo?.geometry?.coordinates?.[0]?.[1] ??
          popupInfo?.geometry?.coordinates?.[1]
        }
        className="mapbox-popup"
        maxWidth="350px"
        anchor="bottom"
        closeOnMove={true}
        closeOnClick={true}
        closeButton={false}
        focusAfterOpen={false}
        onClose={() => setIsShowPopup(false)}
      >
        <Information id={popupInfo.id} properties={popupInfo.properties} />
      </Popup>
    )
  );
};

export default MapPopup;

const Information = ({ id, properties }) => {
  const [infomations, setInfomations] = useState([]);
  const configPopup = config.popup;

  useEffect(() => {
    const findedInfos = configPopup[id];
    const thisInfos = findedInfos?.infos;
    setInfomations(thisInfos);
  }, [configPopup, id]);

  return (
    <Fragment>
      <h3>{id}</h3>
      {infomations?.map((info) => (
        <span key={info}>{properties[info]}</span>
      ))}
    </Fragment>
  );
};
