import "./UploadExcel.css";
import { Fragment, useCallback, useState } from "react";
import { useLocation } from "react-router-dom";
import * as XLSX from "xlsx";
import MaterialReactTable from "material-react-table";
import { MRT_Localization_VI } from "material-react-table/locales/vi";
import { v4 as uuidv4 } from "uuid";
import clsx from "clsx";
import useToast from "src/hooks/useToast";
import config from "src/config";
import { getTableColumns } from "src/utils/getTableColumns";
import { convertLanguages } from "src/tools/convertLanguages";
import { img } from "src/assets";
import { Button, ButtonGroup } from "../interfaces/Button";
import Form from "src/components/interfaces/Form";

const excelType =
  "application/vnd.ms-excel, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, .xls, .xlsx";

const UploadExcel = ({ data, onView, onDoubleClick, onSubmit }) => {
  const toast = useToast();
  const [excelFile, setExcelFile] = useState(null);
  const [excelData, setExcelData] = useState([]);
  const [columnSelection, setColumnSelection] = useState([]);
  const [hadColumnSelection, setHadColumnSelection] = useState(false);
  const [fields, setFields] = useState([]);
  const [rows, setRows] = useState([]);
  const [columns, setColumns] = useState([]);
  const { state } = useLocation();
  const { layer } = state;
  const configColumns = config.columns[layer];

  const resetFile = useCallback(() => {
    setExcelData([]);
    setColumnSelection([]);
    setHadColumnSelection(false);
    setFields([]);
    setRows([]);
    setColumns([]);
    onView([]);
  }, [onView]);

  const handleChangeFile = useCallback(
    (e) => {
      const { files, accept } = e.target;
      const file = files?.[0];
      if (!file) {
        resetFile();
        return setExcelFile(null);
      }
      const type = file.type;
      if (!accept.includes(type)) {
        toast.error({ title: "Cảnh báo", message: "Vui lòng chọn tệp Excel" });
        resetFile();
        return setExcelFile(null);
      }
      const reader = new FileReader();
      reader.readAsBinaryString(file);
      reader.onload = (e) => {
        const { result } = e.target;
        setExcelFile(result);
      };
    },
    [toast, resetFile]
  );

  const handleDownloadFile = useCallback(() => {
    const format = "xlsx";
    window.open(
      `${process.env.REACT_APP_DOMAIN}${
        process.env.REACT_APP_API_TEMPLATE
      }/${convertLanguages(layer)}.${format}`
    );
  }, [layer]);

  const handleUploadFile = useCallback(() => {
    if (!excelFile) return;
    const readOptions = { type: "binary", cellText: false, cellDates: true };
    const workbook = XLSX.read(excelFile, readOptions);
    const worksheetName = workbook.SheetNames?.[0];
    const worksheet = workbook.Sheets?.[worksheetName];
    const dateOptions = { defval: null, raw: false, dateNF: "yyyy-mm-dd" };
    const data = XLSX.utils.sheet_to_json(worksheet, dateOptions);
    if (!data?.length) {
      toast.error({ title: "Cảnh báo", message: "Dữ liệu nhập vào rỗng!" });
      return;
    }
    setExcelData(data);
    const fields = Object.keys(data?.[0]);
    setFields(fields);
    const columns = configColumns?.filter((col) => col.input);
    setColumnSelection(columns);
  }, [excelFile, configColumns, toast]);

  const handleColumnSelection = useCallback(
    (e) => {
      const { id, value: key } = e.target;
      const hadColumn = columnSelection.some((col) => col.input?.key === key);
      if (hadColumn) {
        toast.error({ title: "Cảnh báo", message: "Trường này đã được chọn!" });
        return;
      }
      const newData = excelData.map((row) => {
        const value = row[key] ?? null;
        const newRow = { ...row, [id]: value };
        return newRow;
      });
      setExcelData(newData);
      const idxColumn = columnSelection.findIndex(
        (col) => col.accessorKey === id
      );
      columnSelection[idxColumn] = {
        ...columnSelection[idxColumn],
        input: { ...columnSelection[idxColumn].input, key: key },
      };
      setColumnSelection(columnSelection);
      setHadColumnSelection(true);
    },
    [excelData, columnSelection, toast]
  );

  const handleViewFile = useCallback(() => {
    if (!excelData?.length) return;
    const newColumns = getTableColumns(excelData, layer);
    setColumns(newColumns);
    const keys = newColumns.map((col) => col.accessorKey);
    const newRows = excelData.map((row) => {
      const initRow = { id: uuidv4().replaceAll("-", "") };
      const newRow = keys.reduce((acc, val) => {
        acc[val] = row[val];
        return acc;
      }, initRow);
      return newRow;
    });
    setRows(newRows);
    onView(newRows);
  }, [excelData, layer, onView]);

  const handleDoubleClick = useCallback(
    (row) => {
      onDoubleClick && onDoubleClick(row);
    },
    [onDoubleClick]
  );

  const handleSubmit = useCallback(() => {
    if (columnSelection?.length !== columns?.length) {
      setColumns([]);
      setRows([]);
      onView([]);
      toast.info({ title: "Nhắc nhở", message: "Hãy chọn đầy đủ tham số!" });
      return;
    }
    onSubmit(data);
  }, [onSubmit, onView, data, columnSelection, columns, toast]);

  return (
    <div className="upload-excel">
      <div className="upload-excel-title">
        <span>UPLOAD & VIEW EXCEL SHEET</span>
      </div>
      <div className="upload-excel-form">
        <div className="upload-excel-action">
          <input
            type="file"
            id="upload"
            accept={excelType}
            onChange={handleChangeFile}
          />
          {!excelFile ? (
            <Button
              outline
              className="upload-excel-btn"
              onClick={handleDownloadFile}
            >
              <img src={img.downloadImg} alt="" />
              Mẫu Excel
            </Button>
          ) : (
            <ButtonGroup>
              <Button
                outline
                className="upload-excel-btn"
                disabled={
                  !excelFile ||
                  hadColumnSelection ||
                  columns?.length ||
                  rows?.length
                }
                onClick={handleUploadFile}
              >
                <img src={img.uploadImg} alt="" />
                Tải lên
              </Button>
              <Button
                outline
                className="upload-excel-btn"
                disabled={!excelFile || !hadColumnSelection}
                onClick={handleViewFile}
              >
                <img src={img.viewExcelImg} alt="" />
                Xem tệp
              </Button>
            </ButtonGroup>
          )}
        </div>
        <div className="upload-excel-submit">
          <Button
            className="upload-excel-btn"
            disabled={!columns?.length || !rows?.length}
            onClick={handleSubmit}
          >
            <img src={img.createImg} alt="" />
            Thêm mới
          </Button>
        </div>
      </div>
      <div className="upload-excel-bar">
        <div className="upload-excel-selection">
          {columnSelection?.map((col) => (
            <div className="upload-excel-columns" key={col.accessorKey}>
              <div
                className={clsx("upload-excel-status", {
                  checked: col.input.key,
                })}
              >
                <img src={img.successImg} alt="" />
              </div>
              <div className="upload-excel-column">
                <span>{col.header}</span>
                <Form.Select
                  id={col.accessorKey}
                  value={col.input.key ?? ""}
                  defaultValue="---Chọn cột---"
                  isSelected={!col.input.key}
                  onChange={handleColumnSelection}
                >
                  {fields?.map((opt) => (
                    <option key={opt} value={opt}>
                      {opt}
                    </option>
                  ))}
                </Form.Select>
              </div>
            </div>
          ))}
        </div>
      </div>
      {columns?.length && rows?.length ? (
        <div className="upload-excel-table">
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
              onDoubleClick: () => handleDoubleClick(row.original),
            })}
            // editingMode={'row'}
            // enableEditing={true}
            // getRowId={(row) => row.id}
          />
        </div>
      ) : (
        <Fragment />
      )}
    </div>
  );
};

export default UploadExcel;
