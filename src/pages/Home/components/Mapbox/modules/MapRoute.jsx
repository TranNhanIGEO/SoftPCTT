import { Fragment, useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Layer, Source, useMap } from "react-map-gl";
import mapboxgl from "mapbox-gl";
import { apiDirectionRoute } from "src/services/home";
import { symbolLyr, polylineLyr, pointLyr } from "src/utils/mapLayer";
import { pointSrc, polylineSrc, setImageSrc } from "src/utils/mapSource";
import { axiosMAP } from "src/tools/axiosAPI";
import { convertQuery } from "src/tools/convertQuery";
import { directionLayers, moveLayer, removeLayer } from "src/utils/toggleLayer";
import { icon } from "src/assets";
import {
  allLayers,
  currentRouteCoords,
  directionSystem,
  setDirectionSystem,
  setTabPanel,
} from "src/stores/home";
import useScreen from "src/hooks/useScreen";

const routeType = {
  polePoint: ({ name, data, image, map }) => {
    const pointGeojson = {
      name: `${name}-pctt`,
      data: data,
    };
    const pointImage = {
      name: `${name}-img`,
      image: image,
      map: map,
    };
    setImageSrc(pointImage);
    const pointLayer = {
      name: name,
      image: `${name}-img`,
      size: 1,
    };
    const pointMarker = {
      source: pointSrc(pointGeojson),
      layer: symbolLyr(pointLayer),
    };
    return pointMarker;
  },
  routeSystem: ({ name, data, image, map }) => {
    const routeGeojson = {
      name: `${name}-pctt`,
      data: data,
    };
    const routeLine = {
      name: `${name}-Đường`,
      color: "#008a15",
      width: 7,
      stroke: 0,
      dash: [],
      opacity: 0.75,
    };
    const routeImage = {
      name: `${name}-Điểm-img`,
      image: image,
      map: map,
    };
    setImageSrc(routeImage);
    const routePoint = {
      name: `${name}-Điểm`,
      image: `${name}-Điểm-img`,
      size: 1.25,
      placement: "line",
    };
    const routeMarker = {
      source: polylineSrc(routeGeojson),
      line: polylineLyr(routeLine),
      point: symbolLyr(routePoint),
    };
    return routeMarker;
  },
  currentPoint: ({ name, data }) => {
    const currentPointGeojson = {
      name: `${name}-pctt`,
      data: data,
    };
    const currentRoutePoint = {
      name: `${name}`,
      color: "#FFA500",
      radius: 5,
      stroke: 1,
      shadow: "#FFFFFF",
    };
    const currentMarker = {
      source: pointSrc(currentPointGeojson),
      layer: pointLyr(currentRoutePoint),
    };
    return currentMarker;
  },
};

const MapRoute = () => {
  const { mapbox } = useMap();
  const mapRef = mapbox?.getMap();
  const dispatch = useDispatch();
  const screenSize = useScreen();
  const allLayer = useSelector(allLayers);
  const directions = useSelector(directionSystem);
  const selectedCoord = useSelector(currentRouteCoords);
  const [startPoint, setStartPoint] = useState({});
  const [endPoint, setEndPoint] = useState({});
  const [routeSystem, setRouteSystem] = useState({});
  const [currentPoint, setCurrentPoint] = useState({});
  const hasDirectionLayer = allLayer?.includes("Hướng di chuyển sơ tán dân");
  const isDesktop = screenSize.isDesktop();

  useEffect(() => {
    if (!isDesktop) return;
    const handleLeftClick = (e) => {
      if (!hasDirectionLayer) return setStartPoint({});
      const hasLayer = mapRef.getLayer("Vị trí xung yếu");
      if (!hasLayer) return;
      const features = mapRef.queryRenderedFeatures(e.point, {
        layers: ["Vị trí xung yếu"],
      });
      if (!features.length) return;
      const coords = Object.keys(e.lngLat).map((coord) => e.lngLat[coord]);
      const thisType = routeType.polePoint;
      const startShape = JSON.stringify({
        type: "Point",
        coordinates: coords,
      });
      const startPoint = thisType({
        name: "Điểm bắt đầu",
        data: [{ shape: startShape }],
        image: icon.startIcon,
        map: mapRef,
      });
      setStartPoint({ ...startPoint, coords });
    };
    mapRef.on("click", handleLeftClick);
    return () => mapRef.off("click", handleLeftClick);
  }, [mapRef, isDesktop, allLayer, hasDirectionLayer]);

  useEffect(() => {
    if (!isDesktop) return;
    const handleRightClick = (e) => {
      if (!hasDirectionLayer) return setEndPoint({});
      const hasLayer = mapRef.getLayer("Vị trí an toàn");
      if (!hasLayer) return;
      const features = mapRef.queryRenderedFeatures(e.point, {
        layers: ["Vị trí an toàn"],
      });
      if (!features.length) return;
      const coords = Object.keys(e.lngLat).map((coord) => e.lngLat[coord]);
      const thisType = routeType.polePoint;
      const endShape = JSON.stringify({
        type: "Point",
        coordinates: coords,
      });
      const endPoint = thisType({
        name: "Điểm kết thúc",
        data: [{ shape: endShape }],
        image: icon.endIcon,
        map: mapRef,
      });
      setEndPoint({ ...endPoint, coords });
    };
    mapRef.on("contextmenu", handleRightClick);
    return () => mapRef.off("contextmenu", handleRightClick);
  }, [mapRef, isDesktop, allLayer, hasDirectionLayer]);

  useEffect(() => {
    const { coords: startCoords } = startPoint;
    const { coords: endCoords } = endPoint;
    if (!startCoords?.length || !endCoords?.length) return;
    const objParams = {
      type: "driving-traffic",
      geometries: "geojson",
      overview: "full",
      language: "vi",
      steps: true,
      alternatives: false,
      access_token: process.env.REACT_APP_MAPBOX_TOKEN,
    };
    const { type, ...sendParams } = objParams;
    const params = convertQuery(sendParams);
    apiDirectionRoute(type, startCoords, endCoords, params, axiosMAP, dispatch);
  }, [startPoint, endPoint, dispatch]);

  useEffect(() => {
    if (!directions?.routes) return;
    const [routes] = directions.routes;
    const geometry = routes.geometry;
    const routeShape = JSON.stringify(geometry);
    const lineCoords = geometry.coordinates;
    const bounds = new mapboxgl.LngLatBounds();
    lineCoords.forEach((coord) => bounds.extend(coord));
    mapRef.fitBounds(bounds, { padding: 100 });
    const thisType = routeType.routeSystem;
    const routeMarker = thisType({
      name: "Hệ thống dẫn đường",
      data: [{ shape: routeShape }],
      image: icon.arrowIcon,
      map: mapRef,
    });
    setRouteSystem(routeMarker);
    setCurrentPoint({});
    dispatch(setTabPanel("directionTab"));
    moveLayer({ layers: ["Điểm bắt đầu", "Điểm kết thúc"], map: mapRef });
  }, [mapRef, directions, dispatch]);

  useEffect(() => {
    if (!selectedCoord.length) return;
    const currentShape = JSON.stringify({
      type: "Point",
      coordinates: selectedCoord,
    });
    const thisType = routeType.currentPoint;
    const currentMarker = thisType({
      name: "Điểm dẫn đường hiện tại",
      data: [{ shape: currentShape }],
    });
    setCurrentPoint(currentMarker);
    mapRef.flyTo({ center: selectedCoord, zoom: 16 });
  }, [mapRef, selectedCoord]);

  useEffect(() => {
    if (hasDirectionLayer) return;
    removeLayer({ layers: directionLayers, map: mapRef });
    setRouteSystem({});
    setStartPoint({});
    setEndPoint({});
    setCurrentPoint({});
    dispatch(setDirectionSystem({}));
  }, [mapRef, allLayer, dispatch, hasDirectionLayer]);

  return (
    <Fragment>
      {startPoint?.source?.data && (
        <Source
          id={startPoint?.source?.name}
          type={startPoint?.source?.type}
          data={startPoint?.source?.data}
        >
          <Layer {...startPoint?.layer} />
        </Source>
      )}
      {endPoint?.source?.data && (
        <Source
          id={endPoint?.source?.name}
          type={endPoint?.source?.type}
          data={endPoint?.source?.data}
        >
          <Layer {...endPoint?.layer} />
        </Source>
      )}
      {routeSystem?.source?.data && (
        <Source
          id={routeSystem?.source?.name}
          type={routeSystem?.source?.type}
          data={routeSystem?.source?.data}
        >
          <Layer {...routeSystem?.line} />
          <Layer {...routeSystem?.point} />
        </Source>
      )}
      {currentPoint?.source?.data && (
        <Source
          id={currentPoint?.source?.name}
          type={currentPoint?.source?.type}
          data={currentPoint?.source?.data}
        >
          <Layer {...currentPoint?.layer} />
        </Source>
      )}
    </Fragment>
  );
};

export default MapRoute;
