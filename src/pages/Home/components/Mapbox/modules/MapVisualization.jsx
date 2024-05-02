import { Fragment, useEffect } from "react";
import { useMap } from "react-map-gl/dist/esm/components/use-map";
import { useSelector } from "react-redux";
import { MapboxInterpolateHeatmapLayer } from "mapbox-gl-interpolate-heatmap";
import config from "src/config";
import { layerVisualization } from "src/utils/layerVisualization";
import { getData } from "src/stores/data";
import { allLayers, currentDistrict } from "src/stores/home";
import {
  hotLayers,
  salinityLayers,
  visualSalinityLayers,
  visualTemparatureLayers,
  moveLayer,
  removeLayer,
} from "src/utils/toggleLayer";

// (Xanh lá - Vàng - Đỏ)
const temparatureColor =
  "\n vec3 valueToColor(float value) {\n return vec3(1, 1.1-1.0*abs(value - 0.0), max((0.0-value)*2.0, 0.0));\n }\n";
// (Đỏ - xanh lá - xanh dương)
const salinityColor =
  "\n vec3 valueToColor(float value) {\n return vec3(max((value-0.5)*2.0, 0.0), 1.0 - 2.0*abs(value - 0.5), max((0.5-value)*2.0, 0.0));\n }\n ";

const MapVisualization = () => {
  const { mapbox } = useMap();
  const mapRef = mapbox?.getMap();
  const allLayer = useSelector(allLayers);
  const selectedDistrict = useSelector(currentDistrict);
  const data = useSelector(getData);
  const hotData = data["Nắng nóng"]
  const salinityData = data["Xâm nhập mặn (Độ mặn)"]
  const configData = config.data;

  useEffect(() => {
    if (!allLayer.includes("Mô hình hóa nhiệt độ")) return;
    if (!hotData?.length || selectedDistrict !== "null") return;
    const avgPositions = layerVisualization({
      data: hotData,
      name: "tentram",
      argument: "nhietdotb",
      shape: "shape",
    });
    removeLayer({ layers: visualTemparatureLayers, map: mapRef });
    const visualization = new MapboxInterpolateHeatmapLayer({
      data: avgPositions,
      id: "Mô hình hóa nhiệt độ",
      opacity: 1,
      aoi: configData.cityBoundaries,
      valueToColor: temparatureColor,
      minValue: 10,
      maxValue: 40,
    });
    mapRef.addLayer(visualization);
    moveLayer({ layers: ["district", "districtLabel"], map: mapRef });
    moveLayer({ layers: hotLayers, map: mapRef });
  }, [mapRef, allLayer, hotData, configData, selectedDistrict]);

  useEffect(() => {
    if (!allLayer.includes("Mô hình hóa độ mặn")) return;
    if (!salinityData?.length || selectedDistrict !== "null") return;
    const avgPositions = layerVisualization({
      data: salinityData,
      name: "tentram",
      argument: "doman",
      shape: "shape",
    });
    removeLayer({ layers: visualSalinityLayers, map: mapRef });
    visualSalinityLayers.forEach((lyr, idx) => {
      const visualization = new MapboxInterpolateHeatmapLayer({
        data: avgPositions,
        id: lyr,
        opacity: 1,
        aoi: configData.riverBoundaries[idx],
        valueToColor: salinityColor,
        minValue: 0,
        maxValue: 20,
      });
      mapRef.addLayer(visualization);
    });
    moveLayer({ layers: salinityLayers, map: mapRef });
  }, [mapRef, allLayer, salinityData, configData, selectedDistrict]);

  useEffect(() => {
    if (hotData?.length) return;
    removeLayer({ layers: visualTemparatureLayers, map: mapRef });
  }, [mapRef, hotData]);

  useEffect(() => {
    if (salinityData?.length) return;
    removeLayer({ layers: visualSalinityLayers, map: mapRef });
  }, [mapRef, salinityData]);

  return <Fragment />;
};

export default MapVisualization;
