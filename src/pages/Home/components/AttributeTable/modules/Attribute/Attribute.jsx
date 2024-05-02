import "./Attribute.css";
import { Fragment, useCallback, useEffect, useRef, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import MaterialReactTable from "material-react-table";
import { MRT_Localization_VI } from "material-react-table/locales/vi";
import { CSVLink } from "react-csv";
import useAxiosJWT from "src/hooks/useAxiosJWT";
import useToast from "src/hooks/useToast";
import useSearch from "src/hooks/useSearch";
import useStatistic from "src/hooks/useStatistic";
import { SimpleSearch } from "src/components/SimpleSearch";
import { AdvancedSearch } from "src/components/AdvancedSearch";
import { img } from "src/assets";
import config from "src/config";
import { EditRole, ReportRole } from "src/middleware/Roles";
import { Button, ButtonGroup } from "src/components/interfaces/Button";
import { LayerStatistic } from "src/components/LayerStatistic";
import { convertLanguages } from "src/tools/convertLanguages";
import { getTableColumns } from "src/utils/getTableColumns";
import { queryValidation } from "src/utils/queryValidation";
import {
  setData,
  getDataRef,
  setDataRef,
  setStatistics,
  getData,
} from "src/stores/data";
import {
  allLayers,
  setTabTable,
  toggleAttributeTable,
  currentDistrict,
  currentLayer,
  setCurrentLayer,
  currentObjCoords,
  setCurrentObjCoords,
  savedWhereClauses,
  setStatisticInfos,
  setSaveWhereClauses,
} from "src/stores/home";
import Form from "src/components/interfaces/Form";
import useScreen from "src/hooks/useScreen";

export const AttributeTab = () => {
  return (
    <Fragment>
      <img src={img.tableImg} alt="" />
      <span>Bảng thông tin lớp dữ liệu</span>
    </Fragment>
  );
};

const Attribute = () => {
  const axiosJWT = useAxiosJWT();
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const toast = useToast();
  const statistic = useStatistic();
  const screenSize = useScreen();
  const isDesktop = screenSize.isDesktop();
  const allLayer = useSelector(allLayers);
  const selectedDistrict = useSelector(currentDistrict);
  const selectedLayer = useSelector(currentLayer);
  const selectedObjCoords = useSelector(currentObjCoords);
  const savedWhereClause = useSelector(savedWhereClauses);
  const [rows, setRows] = useState([]);
  const [columns, setColumns] = useState([]);
  const [dataExport, setDataExport] = useState([]);
  const data = useSelector(getData);
  const dataRef = useSelector(getDataRef);
  const thisData = data[selectedLayer];
  const lengthDataRef = useRef();
  const [isDisabledStatistic, setDisabledStatistic] = useState(true);
  const { whereClause, onOpenSimpleSearch, onOpenAdvancedSearch } = useSearch();
  const configServices = config.services.home;

  const handleExportData = useCallback(() => {
    const dataExport = columns?.map((col) => ({
      label: col.header,
      key: col.accessorKey,
    }));
    setDataExport(dataExport);
  }, [columns]);

  const handleCloseTable = useCallback(() => {
    dispatch(toggleAttributeTable());
    dispatch(setTabTable(null));
  }, [dispatch]);

  const handleSelectLayer = useCallback(
    (e) => {
      const { value } = e.target;
      dispatch(setCurrentLayer(value));
      dispatch(setCurrentObjCoords(null));
      dispatch(setSaveWhereClauses(null));
    },
    [dispatch]
  );

  const handleClearResult = useCallback(() => {
    if (!selectedLayer) return;
    const thisDataOrigin = dataRef?.[selectedLayer];
    dispatch(setData({ [selectedLayer]: thisDataOrigin }));
    dispatch(setCurrentObjCoords(null));
    dispatch(setSaveWhereClauses(null));
  }, [selectedLayer, dataRef, dispatch]);

  const handleZoomBound = useCallback(
    (row) => {
      const hasShape = row.original.shape;
      if (!hasShape) return;
      dispatch(setCurrentObjCoords(row.original));
    },
    [dispatch]
  );

  const handleZoomStorm = useCallback(
    (row, e) => {
      e.preventDefault();
      const hasLine = row.original.line;
      if (!hasLine) return;
      const { shape, ...ortherInfo } = row.original;
      dispatch(setCurrentObjCoords(ortherInfo));
    },
    [dispatch]
  );

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
        page: "home",
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
    if (!selectedLayer) return;
    setDisabledStatistic(true);
  }, [selectedLayer]);

  useEffect(() => {
    if (!thisData?.length) return;
    if (lengthDataRef.current === thisData?.length) return;
    lengthDataRef.current = thisData?.length;
    const hasDataRef = dataRef?.[selectedLayer];
    if (hasDataRef?.length) return;
    dispatch(setDataRef({ [selectedLayer]: thisData }));
  }, [thisData, selectedLayer, dataRef, dispatch]);

  useEffect(() => {
    if (!thisData?.length) {
      setRows([]);
      setColumns([]);
      return;
    }
    const columns = getTableColumns(thisData, selectedLayer);
    setColumns(columns);
    setRows(thisData);
  }, [thisData, selectedLayer]);

  useEffect(() => {
    if (selectedLayer) return;
    dispatch(setSaveWhereClauses(null));
  }, [selectedLayer, dispatch]);

  const handleSimpleSearch = useCallback(() => {
    const findedName = config.services["home"][selectedLayer];
    const thisApi = findedName?.dataApi;
    if (!thisApi) return;
    dispatch(setCurrentObjCoords(null));
    const query = queryValidation(whereClause);
    thisApi({
      layer: selectedLayer,
      districtID: selectedDistrict,
      query,
      axiosJWT,
      dispatch,
      toast,
    });
  }, [selectedLayer, selectedDistrict, whereClause, axiosJWT, dispatch, toast]);

  const handleAdvancedSearch = useCallback(() => {
    const findedName = config.services["home"][selectedLayer];
    const thisApi = findedName?.dataApi;
    if (!thisApi) return;
    dispatch(setCurrentObjCoords(null));
    const query = whereClause["where"];
    thisApi({
      layer: selectedLayer,
      districtID: selectedDistrict,
      query,
      axiosJWT,
      dispatch,
      toast,
    });
  }, [selectedLayer, selectedDistrict, whereClause, axiosJWT, dispatch, toast]);

  return (
    <div className="tab-attribute">
      <div className="attribute-bar">
        <div className="attribute-bar-title">
          <span>BẢNG THÔNG TIN</span>
          {isDesktop && (
            <EditRole
              _thisDistrict={selectedDistrict}
              _thisLayer={selectedLayer}
            >
              <Button
                onClick={() => navigate("/management")}
                disabled={
                  !selectedLayer ||
                  !allLayer.length ||
                  !configServices[selectedLayer]?.editAccept
                }
              >
                Cập nhật dữ liệu
              </Button>
            </EditRole>
          )}
        </div>
        <Form.Select
          value={selectedLayer ?? ""}
          defaultValue="Chọn lớp dữ liệu"
          isSelected={!selectedLayer || !allLayer.length}
          onChange={handleSelectLayer}
        >
          {allLayer
            ?.filter((layer) => configServices[layer])
            ?.map((layer) => (
              <option key={layer} id={layer} value={layer}>
                {layer}
              </option>
            ))}
        </Form.Select>
        <div className="attribute-bar-action">
          {isDesktop && (
            <ReportRole
              _thisDistrict={selectedDistrict}
              _thisLayer={selectedLayer}
            >
              <CSVLink
                className="attribute-export"
                data={rows}
                headers={dataExport}
                asyncOnClick={true}
                onClick={handleExportData}
                filename={`${convertLanguages(selectedLayer)}`}
              >
                <img src={img.exportImg} alt="" />
              </CSVLink>
            </ReportRole>
          )}
          <div className="attribute-close" onClick={handleCloseTable}>
            <img src={img.closeImg} alt="" />
          </div>
        </div>
      </div>
      <div className="attribute-content">
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
            onClick: () => handleZoomBound(row),
            onContextMenu: (e) => handleZoomStorm(row, e),
            selected:
              row.original?.["shape"] &&
              row.original?.["shape"] === selectedObjCoords?.["shape"],
          })}
          renderTopToolbarCustomActions={() => (
            <Fragment>
              <ButtonGroup>
                <Button
                  small
                  outline
                  onClick={onOpenSimpleSearch}
                  disabled={
                    !selectedLayer ||
                    !allLayer.length ||
                    !configServices[selectedLayer]?.editApi
                  }
                >
                  Tìm kiếm
                </Button>
                {isDesktop && (
                  <Button
                    small
                    outline
                    onClick={onOpenAdvancedSearch}
                    disabled={
                      !selectedLayer ||
                      !allLayer.length ||
                      !configServices[selectedLayer]?.editApi
                    }
                  >
                    Nâng cao
                  </Button>
                )}
              </ButtonGroup>
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
              small={!isDesktop}
              outline
              onClick={handleClearResult}
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
        <AdvancedSearch
          source={dataRef}
          currentLayer={selectedLayer}
          isFullZone={selectedDistrict === "null"}
          onSubmit={handleAdvancedSearch}
        />
      </div>
    </div>
  );
};

export default Attribute;
