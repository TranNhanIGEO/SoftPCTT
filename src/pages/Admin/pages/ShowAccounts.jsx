import { useCallback, useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import MaterialReactTable from "material-react-table";
import { MRT_Localization_VI } from "material-react-table/locales/vi";
import { allMembers } from "src/stores/admin";
import { getTableColumns } from "src/utils/getTableColumns";
import { Button } from "src/components/interfaces/Button";
import useScreen from "src/hooks/useScreen";

const ShowAccount = () => {
  const navigate = useNavigate();
  const screenSize = useScreen();
  const isDesktop = screenSize.isDesktop();
  const memberLists = useSelector(allMembers);
  const [rows, setRows] = useState([]);
  const [columns, setColumns] = useState([]);

  useEffect(() => {
    if (!memberLists?.length) return;
    const [data] = memberLists;
    if (!data?.length) return;
    const columns = getTableColumns(data, "account");
    setColumns(columns);
    setRows(data);
  }, [memberLists]);

  const goToRoute = useCallback(
    (row) => {
      if (!isDesktop) return;
      const id = row.original["memberid"];
      navigate(`/admin/update/${id}`);
    },
    [isDesktop, navigate]
  );

  return (
    <div className="data-wrap">
      <div className="data-container">
        <div className="data-title">
          <div className="data-bar-title">
            <span>DANH SÁCH TÀI KHOẢN NGƯỜI DÙNG</span>
          </div>
          <Button onClick={() => navigate("/admin/history")}>
            Lịch sử cập nhật
          </Button>
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
                onDoubleClick: () => goToRoute(row),
              })}
              renderTopToolbarCustomActions={() => (
                <div className="MuiBox-root">
                  {isDesktop && (
                    <Button outline onClick={() => navigate("/admin/create")}>
                      Thêm mới tài khoản
                    </Button>
                  )}
                </div>
              )}
            />
          </div>
        </div>
      </div>
    </div>
  );
};

export default ShowAccount;
