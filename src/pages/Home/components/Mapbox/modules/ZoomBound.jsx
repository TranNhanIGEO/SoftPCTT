import { useSelector } from "react-redux";
import { Layer, Source, useMap } from "react-map-gl";
import mapboxgl from "mapbox-gl";
import { currentObjCoords, districtGeometry } from "src/stores/home";
import { Fragment, useEffect, useState } from "react";
import { pointSrc, polylineSrc } from "src/utils/mapSource";
import { pointLyr, polylineLyr } from "src/utils/mapLayer";
import { moveLayer } from "src/utils/toggleLayer";
import config from "src/config";

const zoomType = {
  Point: ({ coordinates, data, map, onPoint, onLine }) => {
    const [lon, lat] = coordinates;
    if (lon === -Infinity && lat === -Infinity) return;
    const pointGeojson = {
      name: "Điểm được chọn-pctt",
      data: data,
    };
    const pointLayer = {
      name: "Điểm được chọn",
      color: "#01abff",
      radius: 7.5,
      stroke: 3.5,
      shadow: "#99ddff",
    };
    const pointMarker = {
      source: pointSrc(pointGeojson),
      layer: pointLyr(pointLayer),
    };
    onLine({});
    onPoint(pointMarker);
    map?.flyTo({ center: coordinates, zoom: 12 });
    moveLayer({ layers: ["Điểm được chọn"], map });
  },
  LineString: ({ coordinates, data, map, onPoint, onLine }) => {
    const lineGeojson = {
      name: "Đường được chọn-pctt",
      data: data,
    };
    const lineLayer = {
      name: "Đường được chọn",
      color: "#0083c3",
      width: 3.5,
      stroke: 1.5,
      dash: [],
      opacity: 1,
    };
    const lineMarker = {
      source: polylineSrc(lineGeojson),
      layer: polylineLyr(lineLayer),
    };
    const lineBounds = new mapboxgl.LngLatBounds();
    coordinates.forEach((coord) => lineBounds.extend(coord));
    onPoint({});
    onLine(lineMarker);
    map?.fitBounds(lineBounds, {
      maxZoom: 12,
      padding: { bottom: 400, top: 100, left: 100 },
    });
    moveLayer({ layers: ["Đường được chọn"], map });
  },
  MultiLineString: ({ coordinates, data, map, onPoint, onLine }) => {
    const lineGeojson = {
      name: "Đường được chọn-pctt",
      data: data,
    };
    const lineLayer = {
      name: "Đường được chọn",
      color: "#0083c3",
      width: 3.5,
      stroke: 1.5,
      dash: [],
      opacity: 1,
    };
    const lineMarker = {
      source: polylineSrc(lineGeojson),
      layer: polylineLyr(lineLayer),
    };
    const lineBounds = new mapboxgl.LngLatBounds();
    coordinates.forEach((coords) =>
      coords.forEach((coord) => lineBounds.extend(coord))
    );
    onPoint({});
    onLine(lineMarker);
    map?.fitBounds(lineBounds, {
      maxZoom: 12,
      padding: { bottom: 400, top: 100, left: 100 },
    });
    moveLayer({ layers: ["Đường được chọn"], map });
  },
};

const ZoomBound = () => {
  const { mapbox } = useMap();
  const mapRef = mapbox?.getMap();
  const districtGeom = useSelector(districtGeometry);
  const selectedObjCoords = useSelector(currentObjCoords);
  const [pointMarker, setPointMarker] = useState({});
  const [lineMarker, setLineMarker] = useState({});
  const configData = config.data;

  useEffect(() => {
    const bounds = new mapboxgl.LngLatBounds();
    if (!districtGeom) {
      const cityBounds = configData.cityZoom;
      const hasLayer = mapRef?.getLayer("district");
      if (!hasLayer) return;
      mapRef?.fitBounds(cityBounds, { padding: 20 });
      return;
    }
    const geom = JSON.parse(districtGeom);
    const [coords] = geom?.coordinates;
    const [districtCoords] = coords;
    districtCoords.forEach((coord) => bounds.extend(coord));
    mapRef?.fitBounds(bounds, { maxZoom: 14, padding: 50 });
  }, [mapRef, configData, districtGeom]);

  useEffect(() => {
    if (!selectedObjCoords) return;
    const shape = selectedObjCoords?.shape;
    const line = selectedObjCoords?.line;
    const geometry = shape ? JSON.parse(shape) : JSON.parse(line);
    const { type, coordinates } = geometry;
    const zoom = zoomType[type];
    zoom({
      coordinates,
      data: [selectedObjCoords],
      map: mapRef,
      onPoint: setPointMarker,
      onLine: setLineMarker,
    });
  }, [mapRef, selectedObjCoords]);

  return (
    <Fragment>
      {selectedObjCoords && pointMarker?.source?.data && (
        <Source
          id={pointMarker?.source?.name}
          type={pointMarker?.source?.type}
          data={pointMarker?.source?.data}
        >
          <Layer {...pointMarker?.layer} />
        </Source>
      )}
      {selectedObjCoords && lineMarker?.source?.data && (
        <Source
          id={lineMarker?.source?.name}
          type={lineMarker?.source?.type}
          data={lineMarker?.source?.data}
        >
          <Layer {...lineMarker?.layer} />
        </Source>
      )}
    </Fragment>
  );
};

export default ZoomBound;
