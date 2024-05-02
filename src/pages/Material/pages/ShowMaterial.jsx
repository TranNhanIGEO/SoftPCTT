import { useCallback, useEffect, useRef, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import MaterialReactTable from "material-react-table";
import { MRT_Localization_VI } from "material-react-table/locales/vi";
import { SimpleSearch } from "src/components/SimpleSearch";
import { EditRole } from "src/middleware/Roles";
import useAxiosJWT from "src/hooks/useAxiosJWT";
import useToken from "src/hooks/useToken";
import useSearch from "src/hooks/useSearch";
import config from "src/config";
import { Button, ButtonGroup } from "src/components/interfaces/Button";
import { getTableColumns } from "src/utils/getTableColumns";
import { queryValidation } from "src/utils/queryValidation";
import { getRoleLists, getUserRoleLists } from "src/stores/global";
import { getLayerRoles } from "src/utils/getLayerRoles";
import { getDataRef, setDataRef, getData, setData } from "src/stores/data";
import {
  getMaterialLayers,
  currentDistrict,
  setCurrentDistrict,
  setCurrentLayer,
  getMaterialDistricts,
  currentLayer,
} from "src/stores/material";
import Form from "src/components/interfaces/Form";
import useScreen from "src/hooks/useScreen";

const ShowMaterial = () => {
  const axiosJWT = useAxiosJWT();
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const currentUser = useToken();
  const screenSize = useScreen();
  const isDesktop = screenSize.isDesktop();
  const [materialLayers] = useSelector(getMaterialLayers);
  const [materialDistricts] = useSelector(getMaterialDistricts);
  const selectedLayer = useSelector(currentLayer);
  const selectedDistrict = useSelector(currentDistrict);
  const [roleLists] = useSelector(getRoleLists);
  const [userRoleLists] = useSelector(getUserRoleLists);
  const data = useSelector(getData);
  const dataRef = useSelector(getDataRef);
  const thisData = data[selectedLayer];
  const [rows, setRows] = useState([]);
  const [columns, setColumns] = useState([]);
  const { whereClause, onOpenSimpleSearch } = useSearch();
  const lengthDataRef = useRef();
  const configServices = config.services.material;

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
    },
    [dispatch]
  );

  useEffect(() => {
    if (!selectedLayer || !selectedDistrict) return;
    const thisAPI = configServices[selectedLayer]?.dataApi;
    if (!thisAPI) return;
    thisAPI({
      layer: selectedLayer,
      districtID: selectedDistrict,
      query: null,
      axiosJWT: axiosJWT,
      dispatch: dispatch,
    });
  }, [selectedDistrict, selectedLayer, configServices, axiosJWT, dispatch]);

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
      if (!hasLayerEditRole) return;
      navigate(`/material/update/${id}`);
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
  }, [selectedLayer, dataRef, dispatch]);

  const handleSimpleSearch = useCallback(() => {
    const findedName = config.services["material"][selectedLayer];
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
            <span>TƯ LIỆU PHÒNG CHỐNG THIÊN TAI</span>
            {isDesktop && (
              <EditRole
                _thisDistrict={selectedDistrict}
                _thisLayer={selectedLayer}
              >
                <Button
                  disabled={!selectedDistrict || !selectedLayer}
                  onClick={() => navigate("/material/create")}
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
              {materialDistricts?.map((d) => (
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
              {materialLayers?.map((lyr) => (
                <option key={lyr["layerName"]} value={lyr["layerName"]}>
                  {lyr["layerName"]}
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
              initialState={{ pagination: { pageSize: 5, pageIndex: 0 } }}
              enableColumnOrdering
              enableStickyHeader
              enableColumnDragging={false}
              localization={MRT_Localization_VI}
              muiTablePaginationProps={{ rowsPerPageOptions: [5, 10, 20] }}
              muiTablePaperProps={{ sx: { height: "100%" } }}
              muiTopToolbarProps={{ sx: { minHeight: "35px" } }}
              muiTableContainerProps={{ sx: { height: "calc(100% - 70px)" } }}
              muiBottomToolbarProps={{ sx: { minHeight: "35px" } }}
              muiTableBodyRowProps={({ row }) => ({
                onDoubleClick: () => goToRoute(row.original["objectid"]),
              })}
              renderTopToolbarCustomActions={() => (
                <Button
                  large={isDesktop}
                  outline
                  onClick={onOpenSimpleSearch}
                  disabled={!selectedDistrict || !selectedLayer}
                >
                  Tìm kiếm
                </Button>
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

export default ShowMaterial;
