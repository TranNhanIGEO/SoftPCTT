import "./ShowData.css";
import { useCallback, useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import MaterialReactTable from "material-react-table";
import { MRT_Localization_VI } from "material-react-table/locales/vi";
import { EditRole } from "src/middleware/Roles";
import useAxiosJWT from "src/hooks/useAxiosJWT";
import useToken from "src/hooks/useToken";
import config from "src/config";
import { getTableColumns } from "src/utils/getTableColumns";
import { getRoleLists, getUserRoleLists } from "src/stores/global";
import { getLayerRoles } from "src/utils/getLayerRoles";
import { currentDistrict, currentLayer } from "src/stores/home";
import { Button } from "src/components/interfaces/Button";
import { getData } from "src/stores/data";
import useScreen from "src/hooks/useScreen";

const ShowData = () => {
  const axiosJWT = useAxiosJWT();
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const currentUser = useToken();
  const screenSize = useScreen();
  const isDesktop = screenSize.isDesktop();
  const selectedLayer = useSelector(currentLayer);
  const selectedDistrict = useSelector(currentDistrict);
  const configServices = config.services.home[selectedLayer];
  const [roleLists] = useSelector(getRoleLists);
  const [userRoleLists] = useSelector(getUserRoleLists);
  const data = useSelector(getData);
  const thisData = data[selectedLayer];
  const [rows, setRows] = useState([]);
  const [columns, setColumns] = useState([]);

  useEffect(() => {
    if (!configServices) return navigate("/");
    const thisShowAPI = configServices?.dataApi;
    thisShowAPI({
      layer: selectedLayer,
      districtID: selectedDistrict,
      query: null,
      axiosJWT: axiosJWT,
      dispatch: dispatch,
    });
  }, [
    configServices,
    selectedDistrict,
    selectedLayer,
    axiosJWT,
    dispatch,
    navigate,
  ]);

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
      navigate(`/management/update/${id}`);
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

  return (
    <div className="data-wrap">
      <div className="data-container">
        <div className="data-title">
          <div className="data-bar-title">
            <span>CẬP NHẬT DỮ LIỆU</span>
          </div>
          <Button onClick={() => navigate("/")}>Quay lại</Button>
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
              renderTopToolbarCustomActions={() =>
                isDesktop && (
                  <EditRole
                    _thisDistrict={selectedDistrict}
                    _thisLayer={selectedLayer}
                  >
                    <Button
                      outline
                      onClick={() =>
                        navigate("/management/create", {
                          state: { layer: selectedLayer },
                        })
                      }
                      disabled={!selectedDistrict || !selectedLayer}
                    >
                      Cập nhật dữ liệu
                    </Button>
                  </EditRole>
                )
              }
            />
          </div>
        </div>
      </div>
    </div>
  );
};

export default ShowData;
