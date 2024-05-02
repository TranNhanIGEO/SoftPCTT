import { Fragment, useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { Layer, Source, useMap } from "react-map-gl";
import { removeObjDuplicates } from "src/tools/removeDuplicates";
import {
  depressionLayers,
  removeLayer,
  stormLayers,
} from "src/utils/toggleLayer";
import {
  polygonSrc,
  pointSrc,
  polylineSrc,
  setImageSrc,
} from "src/utils/mapSource";
import { polylineLyr, labelLyr, symbolLyr } from "src/utils/mapLayer";
import { icon } from "src/assets";
import {
  getDistrictData,
  getIslandData,
  getTrafficData,
  getWaterSystemData,
  currentDistrict,
  currentObjCoords,
  allLayers,
} from "src/stores/home";
import { getData } from "src/stores/data";

const SourceLayer = () => {
  const { mapbox } = useMap();
  const mapRef = mapbox?.getMap();
  const allLayer = useSelector(allLayers);
  const selectedDistrict = useSelector(currentDistrict);
  const selectedObjCoords = useSelector(currentObjCoords);
  const [districtsData] = useSelector(getDistrictData);
  const [islandData] = useSelector(getIslandData);
  const [trafficData] = useSelector(getTrafficData);
  const [waterSystemData] = useSelector(getWaterSystemData);

  const data = useSelector(getData);
  const depressionData = data["Áp thấp nhiệt đới"];
  const stormData = data["Bão"];
  const tornadoData = data["Lốc"];
  const failureLineData = data["Tuyến sạt lở"];
  const failurePointData = data["Điểm sạt lở"];
  const salinityData = data["Xâm nhập mặn (Độ mặn)"];
  const hotData = data["Nắng nóng"];
  const rainData = data["Mưa"];
  const waterLevelData = data["Mực nước"];
  const lakeData = data["Hồ chứa"];
  const lakeSystemData = data["Hệ thống hồ chứa"];
  const forestFireData = data["Cháy rừng tự nhiên"];
  const embankmentData = data["Kè"];
  const dikeData = data["Đê bao, bờ bao"];
  const sewerData = data["Cống, đập"];
  const warningMarkData = data["Mốc cảnh báo triều cường"];
  const warningSignData = data["Biển cảnh báo sạt lở"];
  const parkZoneData = data["Khu neo đậu tàu thuyền"];
  const weakPointData = data["Vị trí xung yếu"];
  const safePointData = data["Vị trí an toàn"];

  const [districtLine, setDistrictLine] = useState({});
  const [districtPoint, setDistrictPoint] = useState({});
  const [island, setIsland] = useState({});
  const [traffic, setTraffic] = useState({});
  const [water, setWater] = useState({});

  const [depressionLine, setDepressionLine] = useState({});
  const [depressionPoint, setDepressionPoint] = useState({});
  const [stormPoint, setStormPoint] = useState({});
  const [stormLine, setStormLine] = useState({});
  const [tornado, setTornado] = useState({});
  const [failureLine, setFailureLine] = useState({});
  const [failurePoint, setFailurePoint] = useState({});
  const [salinity, setSalinity] = useState({});
  const [hot, setHot] = useState({});
  const [rain, setRain] = useState({});
  const [waterLevel, setWaterLevel] = useState({});
  const [lake, setLake] = useState({});
  const [lakeSystem, setLakeSystem] = useState({});
  const [forestFire, setForestFire] = useState({});
  const [embankment, setEmbankment] = useState({});
  const [dike, setDike] = useState({});
  const [sewer, setSewer] = useState({});
  const [warningMark, setWarningMark] = useState({});
  const [warningSign, setWarningSign] = useState({});
  const [parkZone, setParkZone] = useState({});
  const [weakPoint, setWeakPoint] = useState({});
  const [safePoint, setSafePoint] = useState({});

  useEffect(() => {
    if (!districtsData) return;
    const districtGeojson = {
      name: "districtBoundaries",
      data: districtsData,
    };
    const districtLine = {
      name: "district",
      color: "#222222",
      width: 2,
      stroke: 0,
      dash: [],
      opacity: 1,
    };
    setDistrictLine({
      source: polygonSrc(districtGeojson),
      layer: polylineLyr(districtLine),
    });

    const districtLabelGeojson = {
      name: "districtCenter",
      data: districtsData,
      attributes: ["tenhuyen"],
    };
    const districtLabel = {
      name: "districtLabel",
      attributes: ["tenhuyen"],
      color: "#016a87",
      shadow: "#ffffff",
      size: 14,
      offset: [0, 0],
    };
    setDistrictPoint({
      source: pointSrc(districtLabelGeojson),
      label: labelLyr(districtLabel),
    });
  }, [districtsData]);

  useEffect(() => {
    if (!islandData) return;
    const islandGeojson = {
      name: "islandBoundaries",
      data: islandData,
      attributes: ["quandao"],
    };
    const islandLine = {
      name: "Quần đảo",
      color: "#222222",
      width: 2,
      stroke: 0,
      dash: [],
      opacity: 1,
    };
    setIsland({
      source: polygonSrc(islandGeojson),
      layer: polylineLyr(islandLine),
    });
  }, [islandData]);

  useEffect(() => {
    if (!trafficData) return;
    const trafficGeojson = {
      name: "trafficBoundaries",
      data: trafficData,
    };
    const trafficLine = {
      name: "Bản đồ giao thông",
      color: "#d9b454",
      width: 1,
      stroke: 0,
      dash: [],
      opacity: 1,
    };
    setTraffic({
      source: polygonSrc(trafficGeojson),
      layer: polylineLyr(trafficLine),
    });
  }, [trafficData]);

  useEffect(() => {
    if (!waterSystemData) return;
    const waterSystemGeojson = {
      name: "waterSystemBoundaries",
      data: waterSystemData,
    };
    const waterSystemLine = {
      name: "Bản đồ thủy hệ",
      color: "#00CCFF",
      width: 1,
      stroke: 0,
      dash: [],
      opacity: 1,
    };
    setWater({
      source: polygonSrc(waterSystemGeojson),
      layer: polylineLyr(waterSystemLine),
    });
  }, [waterSystemData]);

  useEffect(() => {
    const hasLayer = mapRef.getLayer("Áp thấp nhiệt đới");
    if (
      (!selectedObjCoords && !depressionData?.[0]?.shape && hasLayer) ||
      !allLayer.includes("Áp thấp nhiệt đới")
    ) {
      removeLayer({ layers: depressionLayers, map: mapRef });
      setDepressionPoint({});
      setDepressionLine({});
      return;
    }

    if (!depressionData || !depressionData?.[0]?.shape) return;
    const hasDepression = selectedObjCoords?.["idapthap"];
    const thisDepression = !hasDepression
      ? depressionData.map((dt) => ({
          idapthap: dt["idapthap"],
          line: dt["line"],
        }))
      : depressionData.find(
          (dt) =>
            dt["idapthap"] === selectedObjCoords?.["idapthap"] &&
            dt["line"] === selectedObjCoords?.["line"]
        );
    const shapeData = !hasDepression
      ? depressionData.map((dt) => ({
          shape: dt.shape,
          ngay: `${dt["ngay"].slice(0, 10)} (${dt["gio"]}h)`,
        }))
      : depressionData
          .filter(
            (dt) =>
              dt["idapthap"] === thisDepression?.["idapthap"] &&
              dt["line"] === thisDepression?.["line"]
          )
          .map((dt) => ({
            shape: dt.shape,
            ngay: `${dt["ngay"].slice(0, 10)} (${dt["gio"]}h)`,
          }));
    const depressionGeojson = {
      name: "depressionPoints-pctt",
      data: shapeData,
      attributes: [process.env.REACT_APP_DATECOLUMN, "shape"],
    };
    const depressionImage = {
      name: "tropical",
      image: icon.tropicalIcon,
      map: mapRef,
    };
    const depressionPoint = {
      name: "Áp thấp nhiệt đới",
      image: "tropical",
      size: 1,
    };
    const depressionLabel = {
      name: "Ngày xảy ra áp thấp",
      attributes: [process.env.REACT_APP_DATECOLUMN],
      color: "#000000",
      shadow: "#ffffff",
      size: 10,
      offset: [0, 2.5],
    };
    setImageSrc(depressionImage);
    setDepressionPoint({
      source: pointSrc(depressionGeojson),
      layer: symbolLyr(depressionPoint),
      label: labelLyr(depressionLabel),
    });

    if (!thisDepression) return;
    const depressionLineGeojson = {
      name: "depressionLines-pctt",
      data: !hasDepression
        ? thisDepression
        : [{ line: thisDepression?.["line"] }],
    };
    const depressionLine = {
      name: "Đường đi của áp thấp",
      color: "#540075",
      width: 2.5,
      stroke: 0,
      dash: [],
      opacity: 1,
    };
    setDepressionLine({
      source: polylineSrc(depressionLineGeojson),
      layer: polylineLyr(depressionLine),
    });
  }, [mapRef, allLayer, depressionData, selectedObjCoords]);

  useEffect(() => {
    const hasLayer = mapRef.getLayer("Bão");
    if (
      (!selectedObjCoords && !stormData?.[0]?.shape && hasLayer) ||
      !allLayer.includes("Bão")
    ) {
      removeLayer({ layers: stormLayers, map: mapRef });
      setStormPoint({});
      setStormLine({});
      return;
    }

    if (!stormData || !stormData?.[0]?.shape) return;
    const hasStorm = selectedObjCoords?.["tenbao"];
    const thisStorm = !hasStorm
      ? stormData.map((dt) => ({
          tenbao: dt["tenbao"],
          line: dt["line"],
        }))
      : stormData.find(
          (dt) =>
            dt["tenbao"] === selectedObjCoords?.["tenbao"] &&
            dt["line"] === selectedObjCoords?.["line"]
        );
    const shapeData = !hasStorm
      ? stormData.map((dt) => ({
          shape: dt.shape,
          ngay: `${dt["ngay"].slice(0, 10)} (${dt["gio"]}h)`,
        }))
      : stormData
          .filter(
            (dt) =>
              dt["tenbao"] === thisStorm?.["tenbao"] &&
              dt["line"] === thisStorm?.["line"]
          )
          .map((dt) => ({
            shape: dt.shape,
            ngay: `${dt["ngay"].slice(0, 10)} (${dt["gio"]}h)`,
          }));
    const stormGeojson = {
      name: "stormPoints-pctt",
      data: shapeData,
      attributes: [process.env.REACT_APP_DATECOLUMN, "shape"],
    };
    const stormImage = {
      name: "storm",
      image: icon.stormIcon,
      map: mapRef,
    };
    const stormPoint = {
      name: "Bão",
      image: "storm",
      size: 1,
    };
    const stormLabel = {
      name: "Ngày xảy ra bão",
      attributes: [process.env.REACT_APP_DATECOLUMN],
      color: "#000000",
      shadow: "#ffffff",
      size: 10,
      offset: [0, 2.5],
    };
    setImageSrc(stormImage);
    setStormPoint({
      source: pointSrc(stormGeojson),
      layer: symbolLyr(stormPoint),
      label: labelLyr(stormLabel),
    });

    if (!thisStorm) return;
    const stormLineGeojson = {
      name: "stormLines-pctt",
      data: !hasStorm ? thisStorm : [{ line: thisStorm?.["line"] }],
    };
    const stormLine = {
      name: "Đường đi của bão",
      color: "#0d2eff",
      width: 2.5,
      stroke: 0,
      dash: [],
      opacity: 1,
    };
    setStormLine({
      source: polylineSrc(stormLineGeojson),
      layer: polylineLyr(stormLine),
    });
  }, [mapRef, allLayer, stormData, selectedObjCoords]);

  useEffect(() => {
    if (!tornadoData) return;
    const shapeData = tornadoData.map((dt) => ({
      shape: dt.shape,
      vitri: dt.vitri,
    }));
    const tornadoGeojson = {
      name: "tornadoPoints-pctt",
      data: shapeData,
      attributes: ["vitri", "shape"],
    };
    const tornadoImage = {
      name: "tornado",
      image: icon.tornadoIcon,
      map: mapRef,
    };
    const tornadoPoint = {
      name: "Lốc",
      image: "tornado",
      size: 1,
    };
    setImageSrc(tornadoImage);
    setTornado({
      source: pointSrc(tornadoGeojson),
      layer: symbolLyr(tornadoPoint),
    });
  }, [mapRef, tornadoData]);

  useEffect(() => {
    if (!failureLineData) return;
    const shapeData = failureLineData.map((dt) => ({
      shape: dt.shape,
      vitri: dt.vitri,
    }));
    const failureLineGeojson = {
      name: "failureLines-pctt",
      data: shapeData,
      attributes: ["vitri", "shape"],
    };
    const failureLine = {
      name: "Tuyến sạt lở",
      color: "#FF0000",
      width: 2,
      stroke: 0,
      dash: [],
      opacity: 1,
    };
    setFailureLine({
      source: polylineSrc(failureLineGeojson),
      layer: polylineLyr(failureLine),
    });
  }, [failureLineData]);

  useEffect(() => {
    if (!failurePointData) return;
    const shapeData = failurePointData.map((dt) => ({
      shape: dt.shape,
      vitri: dt.vitri,
    }));
    const failurePointGeojson = {
      name: "failurePoints-pctt",
      data: shapeData,
      attributes: ["vitri", "shape"],
    };
    const failurePointImage = {
      name: "failurePoint",
      image: icon.triangleIcon,
      map: mapRef,
    };
    const failurePoint = {
      name: "Điểm sạt lở",
      image: "failurePoint",
      size: 0.8,
    };
    setImageSrc(failurePointImage);
    setFailurePoint({
      source: pointSrc(failurePointGeojson),
      layer: symbolLyr(failurePoint),
    });
  }, [mapRef, failurePointData]);

  useEffect(() => {
    if (!salinityData) return;
    const shapeData = salinityData.map((dt) => ({
      shape: dt.shape,
      tentram: dt.tentram,
    }));
    const data = removeObjDuplicates(shapeData, "shape");
    const salinityGeojson = {
      name: "salinityPoints-pctt",
      data: data,
      attributes: ["tentram", "shape"],
    };
    const salinityImage = {
      name: "salinity",
      image: icon.circleIcon,
      map: mapRef,
    };
    const salinityPoint = {
      name: "Xâm nhập mặn (Độ mặn)",
      image: "salinity",
      size: 1,
    };
    const salinityLabel = {
      name: "Trạm đo mặn",
      attributes: ["tentram"],
      color: "#000000",
      shadow: "#ffffff",
      size: 10,
      offset: [0, 2.8],
    };
    setImageSrc(salinityImage);
    setSalinity({
      source: pointSrc(salinityGeojson),
      layer: symbolLyr(salinityPoint),
      label: labelLyr(salinityLabel),
    });
  }, [mapRef, salinityData]);

  useEffect(() => {
    if (!hotData) return;
    const shapeData = hotData.map((dt) => ({
      shape: dt.shape,
      tentram: dt.tentram,
    }));
    const data = removeObjDuplicates(shapeData, "shape");
    const hotGeojson = {
      name: "hotPoints-pctt",
      data: data,
      attributes: ["tentram", "shape"],
    };
    const hotImage = {
      name: "hot",
      image: icon.sunIcon,
      map: mapRef,
    };
    const hotPoint = {
      name: "Nắng nóng",
      image: "hot",
      size: 1,
    };
    const hotLabel = {
      name: "Trạm đo nhiệt độ",
      attributes: ["tentram"],
      color: "#000000",
      shadow: "#ffffff",
      size: 10,
      offset: [0, 2.5],
    };
    setImageSrc(hotImage);
    setHot({
      source: pointSrc(hotGeojson),
      layer: symbolLyr(hotPoint),
      label: labelLyr(hotLabel),
    });
  }, [mapRef, hotData]);

  useEffect(() => {
    if (!rainData) return;
    const shapeData = rainData.map((dt) => ({
      shape: dt.shape,
      tentram: dt.tentram,
    }));
    const data = removeObjDuplicates(shapeData, "shape");
    const rainGeojson = {
      name: "rainPoints-pctt",
      data: data,
      attributes: ["tentram", "shape"],
    };
    const rainImage = {
      name: "rain",
      image: icon.rainIcon,
      map: mapRef,
    };
    const rainPoint = {
      name: "Mưa",
      image: "rain",
      size: 1,
    };
    const rainLabel = {
      name: "Trạm đo mưa",
      attributes: ["tentram"],
      color: "#000000",
      shadow: "#ffffff",
      size: 10,
      offset: [0, 2.5],
    };
    setImageSrc(rainImage);
    setRain({
      source: pointSrc(rainGeojson),
      layer: symbolLyr(rainPoint),
      label: labelLyr(rainLabel),
    });
  }, [mapRef, rainData]);

  useEffect(() => {
    if (!waterLevelData) return;
    const shapeData = waterLevelData.map((dt) => ({
      shape: dt.shape,
      tentram: dt.tentram,
    }));
    const data = removeObjDuplicates(shapeData, "shape");
    const waterLevelGeojson = {
      name: "waterLevelPoints-pctt",
      data: data,
      attributes: ["tentram", "shape"],
    };
    const waterLevelImage = {
      name: "waterLevel",
      image: icon.waterLevelIcon,
      map: mapRef,
    };
    const waterLevelPoint = {
      name: "Mực nước",
      image: "waterLevel",
      size: 1,
    };
    const waterLevelLabel = {
      name: "Trạm đo mực nước",
      attributes: ["tentram"],
      color: "#000000",
      shadow: "#ffffff",
      size: 10,
      offset: [0, 2.5],
    };
    setImageSrc(waterLevelImage);
    setWaterLevel({
      source: pointSrc(waterLevelGeojson),
      layer: symbolLyr(waterLevelPoint),
      label: labelLyr(waterLevelLabel),
    });
  }, [mapRef, waterLevelData]);

  useEffect(() => {
    if (!lakeData) return;
    const shapeData = lakeData.map((dt) => ({ shape: dt.shape, ten: dt.ten }));
    const data = removeObjDuplicates(shapeData, "shape");
    const lakeGeojson = {
      name: "lakePoints-pctt",
      data: data,
      attributes: ["ten", "shape"],
    };
    const lakeImage = {
      name: "lake",
      image: icon.lakeIcon,
      map: mapRef,
    };
    const lakePoint = {
      name: "Hồ chứa",
      image: "lake",
      size: 1,
    };
    const lakeLabel = {
      name: "Hồ chứa nước",
      attributes: ["ten"],
      color: "#000000",
      shadow: "#ffffff",
      size: 10,
      offset: [0, 2.8],
    };
    setImageSrc(lakeImage);
    setLake({
      source: pointSrc(lakeGeojson),
      layer: symbolLyr(lakePoint),
      label: labelLyr(lakeLabel),
    });
  }, [mapRef, lakeData]);

  useEffect(() => {
    if (!lakeSystemData || selectedDistrict !== "null") {
      setLakeSystem({});
      return;
    }
    const lakeSystemGeojson = {
      name: "lakeSystemBoundaries",
      data: lakeSystemData,
      attributes: ["tenthuyhehochua"],
    };
    const lakeSystemLine = {
      name: "Hệ thống hồ chứa",
      color: "#47a1d8",
      width: 2,
      stroke: 0,
      dash: [],
      opacity: 1,
    };
    setLakeSystem({
      source: polygonSrc(lakeSystemGeojson),
      layer: polylineLyr(lakeSystemLine),
    });
  }, [lakeSystemData, selectedDistrict]);

  useEffect(() => {
    if (!forestFireData) return;
    const forestFireGeojson = {
      name: "forestFirePoints-pctt",
      data: forestFireData,
    };
    const forestFireImage = {
      name: "forestFire",
      image: icon.fireIcon,
      map: mapRef,
    };
    const forestFirePoint = {
      name: "Cháy rừng tự nhiên",
      image: "forestFire",
      size: 1,
    };
    setImageSrc(forestFireImage);
    setForestFire({
      source: pointSrc(forestFireGeojson),
      layer: symbolLyr(forestFirePoint),
    });
  }, [mapRef, forestFireData]);

  useEffect(() => {
    if (!embankmentData) return;
    const shapeData = embankmentData.map((dt) => ({
      shape: dt.shape,
      tenkenhmuong: dt.tenkenhmuong,
    }));
    const embankmentGeojson = {
      name: "embankmentLines-pctt",
      data: shapeData,
      attributes: ["tenkenhmuong", "shape"],
    };
    const embankmentLine = {
      name: "Kè",
      color: "#852a2b",
      width: 2,
      stroke: 0,
      dash: [4, 2],
      opacity: 1,
    };
    setEmbankment({
      source: polylineSrc(embankmentGeojson),
      layer: polylineLyr(embankmentLine),
    });
  }, [embankmentData]);

  useEffect(() => {
    if (!dikeData) return;
    const shapeData = dikeData.map((dt) => ({
      shape: dt.shape,
      tenkenhmuong: dt.tenkenhmuong,
    }));
    const dikeGeojson = {
      name: "dikeLines-pctt",
      data: shapeData,
      attributes: ["tenkenhmuong", "shape"],
    };
    const dikeLine = {
      name: "Đê bao, bờ bao",
      color: "#1E1623",
      width: 2,
      stroke: 0,
      dash: [2, 2],
      opacity: 1,
    };
    setDike({ source: polylineSrc(dikeGeojson), layer: polylineLyr(dikeLine) });
  }, [dikeData]);

  useEffect(() => {
    if (!sewerData) return;
    const shapeData = sewerData.map((dt) => ({
      shape: dt.shape,
      tencongdap: dt.tencongdap,
    }));
    const sewerGeojson = {
      name: "sewerPoints-pctt",
      data: shapeData,
      attributes: ["tencongdap", "shape"],
    };
    const sewerImage = {
      name: "sewer",
      image: icon.sewerIcon,
      map: mapRef,
    };
    const sewerPoint = {
      name: "Cống, đập",
      image: "sewer",
      size: 0.7,
    };
    setImageSrc(sewerImage);
    setSewer({ source: pointSrc(sewerGeojson), layer: symbolLyr(sewerPoint) });
  }, [mapRef, sewerData]);

  useEffect(() => {
    if (!warningMarkData) return;
    const shapeData = warningMarkData.map((dt) => ({
      shape: dt.shape,
      vitri: dt.vitri,
    }));
    const warningMarkGeojson = {
      name: "warningMarkPoints-pctt",
      data: shapeData,
      attributes: ["vitri", "shape"],
    };
    const warningMarkImage = {
      name: "warningMark",
      image: icon.warningMarkIcon,
      map: mapRef,
    };
    const warningMarkPoint = {
      name: "Mốc cảnh báo triều cường",
      image: "warningMark",
      size: 1,
    };
    setImageSrc(warningMarkImage);
    setWarningMark({
      source: pointSrc(warningMarkGeojson),
      layer: symbolLyr(warningMarkPoint),
    });
  }, [mapRef, warningMarkData]);

  useEffect(() => {
    if (!warningSignData) return;
    const shapeData = warningSignData.map((dt) => ({
      shape: dt.shape,
      vitrisatlo: dt.vitrisatlo,
    }));
    const warningSignGeojson = {
      name: "warningSignPoints-pctt",
      data: shapeData,
      attributes: ["vitrisatlo", "shape"],
    };
    const warningSignImage = {
      name: "warningSign",
      image: icon.warningSignIcon,
      map: mapRef,
    };
    const warningSignPoint = {
      name: "Biển cảnh báo sạt lở",
      image: "warningSign",
      size: 1,
    };
    setImageSrc(warningSignImage);
    setWarningSign({
      source: pointSrc(warningSignGeojson),
      layer: symbolLyr(warningSignPoint),
    });
  }, [mapRef, warningSignData]);

  useEffect(() => {
    if (!parkZoneData) return;
    const shapeData = parkZoneData.map((dt) => ({
      shape: dt.shape,
      ten: dt.ten,
    }));
    const parkZoneGeojson = {
      name: "parkZonePoints-pctt",
      data: shapeData,
      attributes: ["ten", "shape"],
    };
    const parkZoneImage = {
      name: "parkZone",
      image: icon.anchorIcon,
      map: mapRef,
    };
    const parkZonePoint = {
      name: "Khu neo đậu tàu thuyền",
      image: "parkZone",
      size: 1,
    };
    setImageSrc(parkZoneImage);
    setParkZone({
      source: pointSrc(parkZoneGeojson),
      layer: symbolLyr(parkZonePoint),
    });
  }, [mapRef, parkZoneData]);

  useEffect(() => {
    if (!weakPointData) return;
    const shapeData = weakPointData.map((dt) => ({
      shape: dt.shape,
      vitri: dt.vitri,
    }));
    const weakPointGeojson = {
      name: "weakPoints-pctt",
      data: shapeData,
      attributes: ["vitri", "shape"],
    };
    const weakPointImage = {
      name: "weakPoint",
      image: icon.weakIcon,
      map: mapRef,
    };
    const weakPoint = {
      name: "Vị trí xung yếu",
      image: "weakPoint",
      size: 0.8,
    };
    setImageSrc(weakPointImage);
    setWeakPoint({
      source: pointSrc(weakPointGeojson),
      layer: symbolLyr(weakPoint),
    });
  }, [mapRef, weakPointData]);

  useEffect(() => {
    if (!safePointData) return;
    const shapeData = safePointData.map((dt) => ({
      shape: dt.shape,
      vitri: dt.vitri,
    }));
    const safePointGeojson = {
      name: "safePoints-pctt",
      data: shapeData,
      attributes: ["vitri", "shape"],
    };
    const safePointImage = {
      name: "safePoint",
      image: icon.safeIcon,
      map: mapRef,
    };
    const safePoint = {
      name: "Vị trí an toàn",
      image: "safePoint",
      size: 1,
    };
    setImageSrc(safePointImage);
    setSafePoint({
      source: pointSrc(safePointGeojson),
      layer: symbolLyr(safePoint),
    });
  }, [mapRef, safePointData]);

  return (
    <Fragment>
      {districtsData && districtLine?.source?.data && (
        <Source
          id={districtLine?.source?.name}
          type={districtLine?.source?.type}
          data={districtLine?.source?.data}
        >
          <Layer {...districtLine?.layer} />
        </Source>
      )}
      {districtsData && districtPoint?.source?.data && (
        <Source
          id={districtPoint?.source?.name}
          type={districtPoint?.source?.type}
          data={districtPoint?.source?.data}
        >
          <Layer {...districtPoint?.label} />
        </Source>
      )}
      {islandData && island?.source?.data && (
        <Source
          id={island?.source?.name}
          type={island?.source?.type}
          data={island?.source?.data}
        >
          <Layer {...island?.layer} />
        </Source>
      )}
      {trafficData && traffic?.source?.data && (
        <Source
          id={traffic?.source?.name}
          type={traffic?.source?.type}
          data={traffic?.source?.data}
        >
          <Layer {...traffic?.layer} />
        </Source>
      )}
      {waterSystemData && water?.source?.data && (
        <Source
          id={water?.source?.name}
          type={water?.source?.type}
          data={water?.source?.data}
        >
          <Layer {...water?.layer} />
        </Source>
      )}
      {depressionData && depressionPoint?.source?.data && (
        <Source
          id={depressionPoint?.source?.name}
          type={depressionPoint?.source?.type}
          data={depressionPoint?.source?.data}
        >
          <Layer {...depressionPoint?.layer} />
          <Layer {...depressionPoint?.label} />
        </Source>
      )}
      {depressionData && depressionLine?.source?.data && (
        <Source
          id={depressionLine?.source?.name}
          type={depressionLine?.source?.type}
          data={depressionLine?.source?.data}
        >
          <Layer {...depressionLine?.layer} />
        </Source>
      )}
      {stormData && stormPoint?.source?.data && (
        <Source
          id={stormPoint?.source?.name}
          type={stormPoint?.source?.type}
          data={stormPoint?.source?.data}
        >
          <Layer {...stormPoint?.layer} />
          <Layer {...stormPoint?.label} />
        </Source>
      )}
      {stormData && stormLine?.source?.data && (
        <Source
          id={stormLine?.source?.name}
          type={stormLine?.source?.type}
          data={stormLine?.source?.data}
        >
          <Layer {...stormLine?.layer} />
        </Source>
      )}
      {tornadoData && tornado?.source?.data && (
        <Source
          id={tornado?.source?.name}
          type={tornado?.source?.type}
          data={tornado?.source?.data}
        >
          <Layer {...tornado?.layer} />
        </Source>
      )}
      {failureLineData && failureLine?.source?.data && (
        <Source
          id={failureLine?.source?.name}
          type={failureLine?.source?.type}
          data={failureLine?.source?.data}
        >
          <Layer {...failureLine?.layer} />
        </Source>
      )}
      {failurePointData && failurePoint?.source?.data && (
        <Source
          id={failurePoint?.source?.name}
          type={failurePoint?.source?.type}
          data={failurePoint?.source?.data}
        >
          <Layer {...failurePoint?.layer} />
        </Source>
      )}
      {salinityData && salinity?.source?.data && (
        <Source
          id={salinity?.source?.name}
          type={salinity?.source?.type}
          data={salinity?.source?.data}
        >
          <Layer {...salinity?.layer} />
          <Layer {...salinity?.label} />
        </Source>
      )}
      {hotData && hot?.source?.data && (
        <Source
          id={hot?.source?.name}
          type={hot?.source?.type}
          data={hot?.source?.data}
        >
          <Layer {...hot?.layer} />
          <Layer {...hot?.label} />
        </Source>
      )}
      {rainData && rain.source?.data && (
        <Source
          id={rain.source?.name}
          type={rain.source?.type}
          data={rain.source?.data}
        >
          <Layer {...rain?.layer} />
          <Layer {...rain?.label} />
        </Source>
      )}
      {waterLevelData && waterLevel?.source?.data && (
        <Source
          id={waterLevel?.source.name}
          type={waterLevel?.source.type}
          data={waterLevel?.source.data}
        >
          <Layer {...waterLevel?.layer} />
          <Layer {...waterLevel?.label} />
        </Source>
      )}
      {lakeData && lake?.source?.data && (
        <Source
          id={lake?.source?.name}
          type={lake?.source?.type}
          data={lake?.source?.data}
        >
          <Layer {...lake?.layer} />
          <Layer {...lake?.label} />
        </Source>
      )}
      {lakeSystemData && lakeSystem?.source?.data && (
        <Source
          id={lakeSystem?.source?.name}
          type={lakeSystem?.source?.type}
          data={lakeSystem?.source?.data}
        >
          <Layer {...lakeSystem?.layer} />
        </Source>
      )}
      {forestFireData && forestFire?.source?.data && (
        <Source
          id={forestFire?.source?.name}
          type={forestFire?.source?.type}
          data={forestFire?.source?.data}
        >
          <Layer {...forestFire?.layer} />
        </Source>
      )}
      {embankmentData && embankment?.source?.data && (
        <Source
          id={embankment?.source?.name}
          type={embankment?.source?.type}
          data={embankment?.source?.data}
        >
          <Layer {...embankment?.layer} />
        </Source>
      )}
      {dikeData && dike?.source?.data && (
        <Source
          id={dike?.source?.name}
          type={dike?.source?.type}
          data={dike?.source?.data}
        >
          <Layer {...dike?.layer} />
        </Source>
      )}
      {sewerData && sewer?.source?.data && (
        <Source
          id={sewer?.source?.name}
          type={sewer?.source?.type}
          data={sewer?.source?.data}
        >
          <Layer {...sewer?.layer} />
        </Source>
      )}
      {warningMarkData && warningMark?.source?.data && (
        <Source
          id={warningMark?.source?.name}
          type={warningMark?.source?.type}
          data={warningMark?.source?.data}
        >
          <Layer {...warningMark?.layer} />
        </Source>
      )}
      {warningSignData && warningSign?.source?.data && (
        <Source
          id={warningSign?.source?.name}
          type={warningSign?.source?.type}
          data={warningSign?.source?.data}
        >
          <Layer {...warningSign?.layer} />
        </Source>
      )}
      {parkZoneData && parkZone?.source?.data && (
        <Source
          id={parkZone?.source?.name}
          type={parkZone?.source?.type}
          data={parkZone?.source?.data}
        >
          <Layer {...parkZone?.layer} />
        </Source>
      )}
      {weakPointData && weakPoint?.source?.data && (
        <Source
          id={weakPoint?.source?.name}
          type={weakPoint?.source?.type}
          data={weakPoint?.source?.data}
        >
          <Layer {...weakPoint?.layer} />
        </Source>
      )}
      {safePointData && safePoint?.source?.data && (
        <Source
          id={safePoint?.source?.name}
          type={safePoint?.source?.type}
          data={safePoint?.source?.data}
        >
          <Layer {...safePoint?.layer} />
        </Source>
      )}
    </Fragment>
  );
};

export default SourceLayer;
