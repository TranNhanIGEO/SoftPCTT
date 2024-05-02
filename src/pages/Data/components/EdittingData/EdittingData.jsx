import "./EdittingData.css";
import { useCallback, useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { useNavigate, useParams } from "react-router-dom";
import { v4 as uuidv4 } from "uuid";
import config from "src/config";
import { img } from "src/assets";
import useAxiosJWT from "src/hooks/useAxiosJWT";
import useToast from "src/hooks/useToast";
import useToken from "src/hooks/useToken";
import { MapData } from "src/pages/Data/components";
import { apiDeleteData } from "src/services/home";
import { currentLayer } from "src/stores/home";
import proj4 from "proj4";
import { Button } from "src/components/interfaces/Button";
import {
  typeCoords,
  coordActions,
  shapeActions,
  mergeActions,
  edittingDataObj,
} from "./edittingDataObj";
import Form from "src/components/interfaces/Form";
import PreviewFile from "src/components/PreviewFile";
proj4.defs(config.projects);

const EdittingData = ({ pageType }) => {
  const edittingPage = edittingDataObj[pageType];
  const axiosJWT = useAxiosJWT();
  const navigate = useNavigate();
  const toast = useToast();
  const currentUser = useToken();
  const memberID = currentUser?.["nameidentifier"];
  const { id } = useParams();
  const selectedLayer = useSelector(currentLayer);
  const configColumns = config.columns[selectedLayer];
  const configServices = config.services.home[selectedLayer];
  const [dataFile, setDataFile] = useState(null);
  const [shapeInfo, setShapeInfo] = useState({});
  const [dataInfo, setDataInfo] = useState({});
  const [dataField] = useState(() => {
    const columns = configColumns.filter((col) => col.input);
    return columns;
  });

  useEffect(() => {
    const showOnlyData = async () => {
      const thisAPI = configServices?.editApi;
      const onGetData = edittingPage.data.onGet;
      const response = await onGetData({ dataField, thisAPI, id, axiosJWT });
      setDataInfo(response);
    };
    showOnlyData();
  }, [configServices, edittingPage, dataField, id, axiosJWT]);

  useEffect(() => {
    const randomID = uuidv4().replaceAll("-", "");
    const { shape } = dataInfo;
    if (!shape) {
      const thisType = configServices?.table?.type;
      setShapeInfo({ type: thisType, coordinates: [] });
      return;
    }
    const geometry = { ...JSON.parse(shape), id: randomID };
    setShapeInfo(geometry);
  }, [configServices, dataInfo]);

  const handleDeleteData = useCallback(() => {
    const thisAPI = configServices?.editApi;
    apiDeleteData({ thisAPI, id, memberID, axiosJWT, navigate, toast });
  }, [configServices, id, memberID, axiosJWT, navigate, toast]);

  const handleClearFile = useCallback(() => {
    const nameColumn = configServices?.file?.name;
    if (!nameColumn) return;
    setDataFile(null);
    setDataInfo((prev) => ({ ...prev, file: "", [nameColumn]: "" }));
  }, [configServices]);

  const handleEnterForm = useCallback(
    (e) => {
      const { name, value, accept, files } = e.target;
      const specialFields = ["hinhanh"];
      if (specialFields.includes(name)) {
        const file = files?.[0];
        if (!file) {
          setDataFile(null);
          setDataInfo((prev) => ({ ...prev, file: "", [name]: "" }));
          return;
        }
        const type = file?.type;
        if (!accept.includes(type)) {
          toast.error({
            title: "Cảnh báo",
            message: `Vui lòng chọn tệp có định dạng ${accept}`,
          });
          return;
        }
        setDataFile({ type: type, source: URL.createObjectURL(file) });
        setDataInfo((prev) => ({ ...prev, file: file, [name]: value }));
        return;
      }
      setDataInfo((prev) => ({ ...prev, [name]: value }));
    },
    [toast]
  );

  const handleShowShape = useCallback((feature) => {
    const { id, geometry } = feature;
    const { type } = geometry;
    const showShape = shapeActions[type]?.show;
    setDataInfo((prev) => showShape(prev, id));
  }, []);

  const handleUpdateShape = useCallback((feature) => {
    const { id, geometry } = feature;
    const { type, coordinates } = geometry;
    if (!coordinates) return;
    const updateShape = shapeActions[type]?.update;
    setDataInfo((prev) => updateShape(prev, coordinates, id));
  }, []);

  const handleDeleteShape = useCallback((feature) => {
    const { id, geometry } = feature;
    const { type } = geometry;
    const deleteShape = shapeActions[type]?.delete;
    setDataInfo((prev) => deleteShape(prev, id));
  }, []);

  const handleMergeShape = useCallback((oldFeatures, newFeatures) => {
    setDataInfo((prev) => mergeActions(prev, oldFeatures, newFeatures));
  }, []);

  const handleRefreshShape = useCallback(() => {
    const thisType = configServices?.table?.type;
    const keys = Object.keys(dataInfo);
    const isCoords = typeCoords.find((tcs) =>
      tcs.type.every((t) => keys.includes(t))
    );
    const getCoords = coordActions[isCoords?.value]?.get;
    if (!getCoords) {
      toast.error({ title: "Cảnh báo", message: "Tọa độ không hợp lệ!" });
      return;
    }
    const newShapeInfo = getCoords(thisType, dataInfo);
    setShapeInfo(newShapeInfo);
  }, [dataInfo, toast, configServices]);

  const handleSubmit = useCallback(
    (e) => {
      e.preventDefault();
      const thisAPI = configServices?.editApi;
      const thisForm = configServices?.table?.form;
      const onSubmit = edittingPage.submit.onSubmit;
      const { file, ...data } = dataInfo;
      const fields = Object.keys(data);

      const coordName = process.env.REACT_APP_COORDINATES;
      if (!data[coordName]) data[coordName] = "";
      const multi = Object.keys(data[coordName]);
      const lnglat = multi?.map((id) => data[coordName]?.[id])?.join(", ");
      data[coordName] = lnglat ? `(${lnglat})` : "";

      const initData = file ? { file } : {};
      const infoData = fields.reduce((acc, val) => {
        data[val] = data[val]?.toString() || null;
        return { ...acc, [val]: data[val] };
      }, initData);

      onSubmit({
        thisAPI,
        thisForm,
        id,
        infoData,
        memberID,
        axiosJWT,
        navigate,
        toast,
      });
    },
    [
      configServices,
      edittingPage,
      id,
      dataInfo,
      memberID,
      axiosJWT,
      navigate,
      toast,
    ]
  );

  return (
    <div className="edit-data">
      <div className="edit-data-container">
        <div className="edit-data-form">
          <div className="edit-data-title">
            <span>{edittingPage.title}</span>
            {pageType === "update" && (
              <Button outline onClick={handleDeleteData}>
                Xóa
              </Button>
            )}
          </div>
          <form className="edit-data-content">
            <div className="edit-data-argument">
              <Form>
                {dataField.map((d) => (
                  <Form.Group key={d.accessorKey} controlID={d.accessorKey}>
                    <Form.Label>{d.header}</Form.Label>
                    <Form.Control
                      type={d.input.type}
                      id={d.accessorKey}
                      name={d.accessorKey}
                      accept={d.input.format}
                      value={dataInfo[d.accessorKey] ?? ""}
                      onChange={handleEnterForm}
                    />
                  </Form.Group>
                ))}
              </Form>
              <PreviewFile file={dataFile} onClear={handleClearFile} />
            </div>
          </form>
          <div className="edit-data-action">
            <Button outline onClick={() => navigate("/management")}>
              Thoát
            </Button>
            <Button isSubmit outline onClick={handleSubmit}>
              {edittingPage.submit.text}
            </Button>
          </div>
        </div>
        {shapeInfo?.type && (
          <div className="edit-map-form">
            <div className="edit-map-title">
              <span>Bản đồ</span>
            </div>
            <div className="edit-map-content">
              <MapData
                shape={shapeInfo}
                onShow={handleShowShape}
                onUpdate={handleUpdateShape}
                onDelete={handleDeleteShape}
                onMerge={handleMergeShape}
              />
              {shapeInfo?.type?.includes("Point") && (
                <div className="mapboxgl-ctrl-custom">
                  <div className="mapboxgl-ctrl-top-left">
                    <div className="mapboxgl-ctrl mapboxgl-ctrl-group">
                      <button onClick={handleRefreshShape}>
                        <img src={img.refreshImg} alt="" />
                        <span>Làm mới tọa độ</span>
                      </button>
                    </div>
                  </div>
                </div>
              )}
            </div>
          </div>
        )}
      </div>
    </div>
  );
};

export default EdittingData;
