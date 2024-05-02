import "./Layer.css";
import { Fragment, useCallback, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useMap } from "react-map-gl";
import useAxiosJWT from "src/hooks/useAxiosJWT";
import useToast from "src/hooks/useToast";
import config from "src/config";
import {
  BaseMaps,
  SelectDistrict,
  ThematicMaps,
  VisualizationMaps,
} from "./interfaces";
import { img } from "src/assets";
import { toggleLayer } from "src/utils/toggleLayer";
import { removeAllThematicData, setStatistics } from "src/stores/data";
import {
  toggleAllLayers,
  allLayers,
  getViewRoleLists,
  setCurrentDistrict,
  currentDistrict,
  setCurrentLayer,
  removeAllLayers,
  removeAllBasicData,
  getDistrictData,
  setDistrictGeometry,
  currentLayer,
  mapTypes,
  toggleMapTypes,
} from "src/stores/home";

export const LayerTab = () => {
  return (
    <Fragment>
      <img src={img.mapLayerImg} alt="" />
      <span>Hiển thị lớp dữ liệu</span>
    </Fragment>
  );
};

const Layer = () => {
  const { mapbox } = useMap();
  const mapRef = mapbox?.getMap();
  const dispatch = useDispatch();
  const axiosJWT = useAxiosJWT();
  const toast = useToast();
  const mapType = useSelector(mapTypes);
  const allLayer = useSelector(allLayers);
  const [viewRoleLists] = useSelector(getViewRoleLists);
  const [districtData] = useSelector(getDistrictData);
  const selectedDistrict = useSelector(currentDistrict);
  const selectedLayer = useSelector(currentLayer);
  const configServices = config.services.home;
  const configBaseMaps = config.services.basemaps;
  const configVisulization = config.visualization;

  const handleChangeDistrict = useCallback(
    (e) => {
      const { value } = e.target;
      const thisDistrict = districtData?.find((d) => d["mahuyen"] === value);
      const districtGeom = thisDistrict?.shape;
      dispatch(removeAllThematicData([]));
      dispatch(removeAllBasicData([]));
      dispatch(removeAllLayers());
      dispatch(setCurrentLayer(null));
      dispatch(setCurrentDistrict(value));
      dispatch(setDistrictGeometry(districtGeom));
    },
    [districtData, dispatch]
  );

  const handleDropMapType = useCallback(
    (e) => {
      const { value } = e.target.parentNode;
      dispatch(toggleMapTypes(value));
    },
    [dispatch]
  );

  const handleToggleLayer = useCallback(
    (e, groupID) => {
      const { value, checked } = e.target;
      const districts = viewRoleLists?.find(
        (r) => r.districtIDOrigin === selectedDistrict
      );
      const groups = districts?.groups?.find((gr) => gr.groupID === groupID);
      const layers = groups?.layers.find((l) => l.layerID === value);
      const layerName = layers?.layerName ?? value;
      const findedName = configServices[layerName] ?? configBaseMaps[layerName];
      const thisApi = findedName?.dataApi;
      const hasLayer = mapRef?.getLayer(layerName);
      dispatch(toggleAllLayers(layerName));
      if (!checked && layerName === selectedLayer) {
        dispatch(setCurrentLayer(null));
        dispatch(setStatistics([]));
      }
      if (!thisApi) return;
      toggleLayer({
        layerName: layerName,
        isChecked: checked,
        hasLayer: hasLayer,
        map: mapRef,
      });
      if (hasLayer) return;
      thisApi({
        layer: layerName,
        districtID: selectedDistrict,
        query: null,
        isChecked: checked,
        axiosJWT: axiosJWT,
        dispatch: dispatch,
        toast: toast,
      });
    },
    [
      viewRoleLists,
      selectedDistrict,
      selectedLayer,
      configServices,
      configBaseMaps,
      mapRef,
      axiosJWT,
      dispatch,
      toast,
    ]
  );

  const handleToggleVisualization = useCallback(
    (e, source) => {
      const { value, checked } = e.target;
      const hasLayer = mapRef?.getLayer(value);
      dispatch(toggleAllLayers(value));
      toggleLayer({
        layerName: value,
        isChecked: checked,
        hasLayer: hasLayer,
        map: mapRef,
      });
      if (!checked || allLayer.includes(source)) return;
      toast.info({ title: "Thông báo", message: `Vui lòng bật lớp ${source}` });
    },
    [mapRef, allLayer, dispatch, toast]
  );

  useEffect(() => {
    if (allLayer.length) return;
    dispatch(setCurrentLayer(null));
  }, [allLayer, dispatch]);

  return (
    <div className="tab-layer">
      <SelectDistrict
        districtLists={viewRoleLists}
        currentDistrict={selectedDistrict}
        onChange={handleChangeDistrict}
      />
      <div className="basemap">
        <div className="basemap-title">
          <span>BẢN ĐỒ NỀN</span>
        </div>
        <div className="basemap-container">
          {selectedDistrict && (
            <BaseMaps allLayer={allLayer} onChange={handleToggleLayer} />
          )}
        </div>
      </div>
      <div className="thematicmap">
        <div className="thematicmap-title">
          <span>BẢN ĐỒ CHUYÊN ĐỀ</span>
        </div>
        <div className="thematicmap-container">
          {viewRoleLists
            ?.find((r) => r.districtIDOrigin === selectedDistrict)
            ?.groups?.map((group, idx) => (
              <ThematicMaps
                key={idx}
                group={group}
                allType={mapType}
                allLayer={allLayer}
                onDrop={handleDropMapType}
                onChange={handleToggleLayer}
              />
            ))}
          {selectedDistrict &&
            configVisulization.map((group, idx) => (
              <VisualizationMaps
                key={idx}
                group={group}
                allType={mapType}
                allLayer={allLayer}
                onDrop={handleDropMapType}
                onChange={handleToggleVisualization}
              />
            ))}
        </div>
      </div>
    </div>
  );
};

export default Layer;
