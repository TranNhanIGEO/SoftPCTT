import { useEffect, useState, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import MaterialReactTable from "material-react-table";
import { MRT_Localization_VI } from "material-react-table/locales/vi";
import useAxiosJWT from "src/hooks/useAxiosJWT";
import { getTableColumns } from "src/utils/getTableColumns";
import { getViewLayers } from "src/utils/getViewLayers";
import { apiShowAllHistories } from "src/services/admin";
import { getRoleLists } from "src/stores/global";
import {
  setCurrentLayer,
  currentLayer,
  historyLayers,
  allHistories,
  setHistoryLayers,
} from "src/stores/admin";
import Form from "src/components/interfaces/Form";

const HistoryUpdate = () => {
  const axiosJWT = useAxiosJWT();
  const dispatch = useDispatch();
  const selectedLayer = useSelector(currentLayer);
  const [roleLists] = useSelector(getRoleLists);
  const [historyLayer] = useSelector(historyLayers);
  const [historyLists] = useSelector(allHistories);
  const [rows, setRows] = useState([]);
  const [columns, setColumns] = useState([]);

  useEffect(() => {
    if (!roleLists?.length) return;
    const adminArgs = getViewLayers({
      pageType: process.env.REACT_APP_ADMINPAGE,
      roleLists: roleLists,
    });
    const adminLayers = adminArgs?.layers;
    dispatch(setHistoryLayers(adminLayers));
  }, [roleLists, dispatch]);

  useEffect(() => {
    if (!selectedLayer) return;
    apiShowAllHistories({ layer: selectedLayer, axiosJWT, dispatch });
  }, [selectedLayer, axiosJWT, dispatch]);

  useEffect(() => {
    if (!historyLists?.length) {
      setColumns([]);
      setRows([]);
      return;
    }
    const columns = getTableColumns(historyLists, "history");
    setColumns(columns);
    setRows(historyLists);
  }, [historyLists]);

  const handleSelectLayer = useCallback(
    (e) => {
      const { value } = e.target;
      dispatch(setCurrentLayer(value));
    },
    [dispatch]
  );

  return (
    <div className="history">
      <div className="data-wrap">
        <div className="data-container">
          <div className="data-title">
            <div className="data-bar-title">
              <span>LỊCH SỬ CHỈNH SỬA - CẬP NHẬT DỮ LIỆU</span>
            </div>
            <Form.Select
              value={selectedLayer ?? undefined}
              defaultValue="CHỌN LỚP DỮ LIỆU"
              isSelected={!selectedLayer}
              onChange={handleSelectLayer}
            >
              {historyLayer?.map((lyr) => (
                <option key={lyr["layerName"]} value={lyr["tableName"]}>
                  {lyr["layerName"]}
                </option>
              ))}
            </Form.Select>
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
              />
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default HistoryUpdate;
