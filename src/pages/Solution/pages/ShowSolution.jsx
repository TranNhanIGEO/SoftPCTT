import { Fragment, useCallback, useEffect, useRef, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import MaterialReactTable from "material-react-table";
import { MRT_Localization_VI } from "material-react-table/locales/vi";
import { SimpleSearch } from "src/components/SimpleSearch";
import { EditRole } from "src/middleware/Roles";
import useAxiosJWT from "src/hooks/useAxiosJWT";
import useToken from "src/hooks/useToken";
import useSearch from "src/hooks/useSearch";
import useStatistic from "src/hooks/useStatistic";
import config from "src/config";
import { Button, ButtonGroup } from "src/components/interfaces/Button";
import { LayerStatistic } from "src/components/LayerStatistic";
import { getTableColumns } from "src/utils/getTableColumns";
import { queryValidation } from "src/utils/queryValidation";
import { apiMaxSolutionID } from "src/services/solution";
import { getRoleLists, getUserRoleLists } from "src/stores/global";
import { getLayerRoles } from "src/utils/getLayerRoles";
import {
  getDataRef,
  setDataRef,
  getData,
  setData,
  setStatistics,
} from "src/stores/data";
import {
  currentDistrict,
  currentLayer,
  getViewRoleLists,
  setCurrentDistrict,
  setCurrentLayer,
  setSaveWhereClauses,
  savedWhereClauses,
  setStatisticInfos,
  setTabTable,
} from "src/stores/solution";
import Form from "src/components/interfaces/Form";
import useScreen from "src/hooks/useScreen";

const ShowSolution = () => {
  const axiosJWT = useAxiosJWT();
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const currentUser = useToken();
  const statistic = useStatistic();
  const screenSize = useScreen();
  const isDesktop = screenSize.isDesktop();
  const [viewRoleLists] = useSelector(getViewRoleLists);
  const selectedLayer = useSelector(currentLayer);
  const selectedDistrict = useSelector(currentDistrict);
  const savedWhereClause = useSelector(savedWhereClauses);
  const [roleLists] = useSelector(getRoleLists);
  const [userRoleLists] = useSelector(getUserRoleLists);
  const data = useSelector(getData);
  const dataRef = useSelector(getDataRef);
  const thisData = data[selectedLayer];
  const [rows, setRows] = useState([]);
  const [columns, setColumns] = useState([]);
  const [isDisabledStatistic, setDisabledStatistic] = useState(true);
  const { whereClause, onOpenSimpleSearch } = useSearch();
  const lengthDataRef = useRef();
  const configServices = config.services.solution;

  const handleSelectDistrict = useCallback(
    (e) => {
      const { value } = e.target;
      dispatch(setCurrentDistrict(value));
    },
    [dispatch]
  );

  const handleSelectLayer = useCallback(
    (e) => {
      const { value } = e.target;
      dispatch(setCurrentLayer(value));
      dispatch(setSaveWhereClauses(null));
      const thisAPI = configServices[value]?.editApi;
      if (!thisAPI) return;
      apiMaxSolutionID({ thisAPI, axiosJWT, dispatch });
    },
    [configServices, axiosJWT, dispatch]
  );

  useEffect(() => {
    if (!selectedLayer || !selectedDistrict) return;
    const findedLayer = configServices[selectedLayer];
    const thisAPI = findedLayer?.dataApi;
    if (!thisAPI) return;
    thisAPI({
      layer: selectedLayer,
      districtID: selectedDistrict,
      query: null,
      axiosJWT: axiosJWT,
      dispatch: dispatch,
    });
    setDisabledStatistic(true);
  }, [selectedDistrict, selectedLayer, configServices, axiosJWT, dispatch]);

  useEffect(() => {
    if (!isDisabledStatistic) return;
    const enabledStatistic = setTimeout(() => {
      setDisabledStatistic(false);
    }, 1500);
    return () => clearTimeout(enabledStatistic);
  }, [isDisabledStatistic]);

  const handleLayerStatistic = useCallback(
    (e) => {
      setDisabledStatistic(true);
      dispatch(setStatistics([]));
      dispatch(setTabTable("statisticTab"));
      const { value } = e.target;
      statistic({
        page: "solution",
        type: value,
        where: savedWhereClause,
        layer: selectedLayer,
        districtID: selectedDistrict,
        source: thisData,
        setInfos: setStatisticInfos,
      });
    },
    [
      thisData,
      selectedDistrict,
      selectedLayer,
      savedWhereClause,
      dispatch,
      statistic,
    ]
  );

  useEffect(() => {
    if (!thisData?.length) {
      setColumns([]);
      setRows([]);
      return;
    }
    const columns = getTableColumns(thisData, selectedLayer);
    setColumns(columns);
    setRows(thisData);
  }, [thisData, selectedLayer]);

  const goToRoute = useCallback(
    (id) => {
      if (!isDesktop) return;
      const hasLayerEditRole = getLayerRoles({
        currentUser,
        roleLists,
        userRoleLists,
        _thisDistrict: selectedDistrict,
        _thisLayer: selectedLayer,
        _thisRole: process.env.REACT_APP_EDITROLE,
      });
      console.log(hasLayerEditRole);
      if (!hasLayerEditRole) return;
      navigate(`/solution/update/${id}`);
    },
    [
      isDesktop,
      currentUser,
      roleLists,
      userRoleLists,
      selectedDistrict,
      selectedLayer,
      navigate,
    ]
  );

  useEffect(() => {
    if (!thisData?.length) return;
    if (lengthDataRef.current === thisData?.length) return;
    lengthDataRef.current = thisData?.length;
    const hasLayerRef = dataRef?.[selectedLayer];
    if (hasLayerRef?.length) return;
    const hasDataRef = dataRef?.[selectedLayer]?.length === thisData?.length;
    if (hasDataRef) return;
    dispatch(setDataRef({ [selectedLayer]: thisData }));
  }, [thisData, selectedLayer, dataRef, dispatch]);

  const handleClearResult = useCallback(() => {
    if (!selectedLayer) return;
    const thisDataOrigin = dataRef?.[selectedLayer];
    dispatch(setData({ [selectedLayer]: thisDataOrigin }));
    dispatch(setSaveWhereClauses(null));
  }, [selectedLayer, dataRef, dispatch]);

  useEffect(() => {
    if (selectedLayer) return;
    dispatch(setSaveWhereClauses(null));
  }, [selectedLayer, dispatch]);

  const handleSimpleSearch = useCallback(() => {
    const findedName = config.services["solution"][selectedLayer];
    const thisApi = findedName?.dataApi;
    if (!thisApi) return;
    const query = queryValidation(whereClause);
    thisApi({
      layer: selectedLayer,
      districtID: selectedDistrict,
      query,
      axiosJWT,
      dispatch,
    });
  }, [selectedLayer, selectedDistrict, whereClause, axiosJWT, dispatch]);

  return (
    <div className="data-wrap">
      <div className="data-container">
        <div className="data-title">
          <div className="data-bar-title">
            <span>PHƯƠNG ÁN - KẾ HOẠCH PHÒNG CHỐNG THIÊN TAI</span>
            {isDesktop && (
              <EditRole
                _thisDistrict={selectedDistrict}
                _thisLayer={selectedLayer}
              >
                <Button
                  onClick={() =>
                    navigate("/solution/create", {
                      state: { layer: selectedLayer },
                    })
                  }
                  disabled={!selectedDistrict || !selectedLayer}
                >
                  Cập nhật dữ liệu
                </Button>
              </EditRole>
            )}
          </div>
          <ButtonGroup>
            <Form.Select
              value={selectedDistrict ?? undefined}
              defaultValue="CHỌN QUẬN/HUYỆN"
              isSelected={!selectedDistrict}
              onChange={handleSelectDistrict}
            >
              {viewRoleLists?.map((d) => (
                <option key={d["districtID"]} value={d["districtID"]}>
                  {d["districtName"]}
                </option>
              ))}
            </Form.Select>
            <Form.Select
              value={selectedLayer ?? undefined}
              defaultValue="CHỌN LỚP DỮ LIỆU"
              isSelected={!selectedLayer}
              disabled={!selectedDistrict}
              onChange={handleSelectLayer}
            >
              {viewRoleLists
                ?.find((lyr) => lyr.districtID === selectedDistrict)
                ?.layers?.filter((lyr) =>
                  userRoleLists
                    ?.filter((r) =>
                      r["roleid"]
                        .toString()
                        .includes(process.env.REACT_APP_VIEWROLE)
                    )
                    ?.some((r) => r["malopdulieu"].includes(lyr.layerID))
                )
                ?.map((lyr) => (
                  <option key={lyr.layerName} value={lyr.layerName}>
                    {lyr.layerName}
                  </option>
                ))}
            </Form.Select>
          </ButtonGroup>
        </div>
        <div className="data-content">
          <div className="data-table">
            <MaterialReactTable
              columns={columns}
              data={rows}
              enableColumnOrdering
              enableStickyHeader
              enableColumnDragging={false}
              localization={MRT_Localization_VI}
              muiTablePaperProps={{ sx: { height: "100%" } }}
              muiTopToolbarProps={{ sx: { minHeight: "35px" } }}
              muiTableContainerProps={{ sx: { height: "calc(100% - 70px)" } }}
              muiBottomToolbarProps={{ sx: { minHeight: "35px" } }}
              muiTableBodyRowProps={({ row }) => ({
                onDoubleClick: () => goToRoute(row.original["objectid"]),
              })}
              renderTopToolbarCustomActions={() => (
                <Fragment>
                  <Button
                    large={isDesktop}
                    outline
                    onClick={onOpenSimpleSearch}
                    disabled={!selectedDistrict || !selectedLayer}
                  >
                    Tìm kiếm
                  </Button>
                  {isDesktop && (
                    <LayerStatistic
                      pageServices={configServices}
                      currentLayer={selectedLayer}
                      currentDistrict={selectedDistrict}
                      isDisabled={isDisabledStatistic}
                      onClick={handleLayerStatistic}
                    />
                  )}
                </Fragment>
              )}
              renderBottomToolbarCustomActions={() => (
                <Button
                  large={isDesktop}
                  outline
                  onClick={handleClearResult}
                  disabled={!selectedDistrict || !selectedLayer}
                >
                  Reset bảng
                </Button>
              )}
            />
            <SimpleSearch
              source={dataRef}
              currentLayer={selectedLayer}
              isFullZone={selectedDistrict === "null"}
              onSubmit={handleSimpleSearch}
            />
          </div>
        </div>
      </div>
    </div>
  );
};

export default ShowSolution;
