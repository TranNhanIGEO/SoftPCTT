import "./Statistic.css";
import { Fragment, useCallback, useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import MaterialReactTable from "material-react-table";
import { MRT_Localization_VI } from "material-react-table/locales/vi";
import HighchartsColumn from "highcharts";
import HighchartsStock from "highcharts/highstock";
import HighchartsReact from "highcharts-react-official";
import enableExporting from "highcharts/modules/exporting";
import { ReportRole } from "src/middleware/Roles";
import { getTableColumns } from "src/utils/getTableColumns";
import { initColumnChart } from "src/utils/columnChart";
import { initLineChart } from "src/utils/lineChart";
import config from "src/config";
import { img } from "src/assets";
import { CSVLink } from "react-csv";
import { convertLanguages } from "src/tools/convertLanguages";
import { removeValDuplicates } from "src/tools/removeDuplicates";
import { generateDocument } from "src/utils/generateDocument";
import { reports } from "src/export/reports";
import { deleteSumRow } from "src/utils/deleteSumRow";
import {
  statisticInfos,
  currentLayer,
  setTabTable,
  toggleAttributeTable,
  currentDistrict,
  getViewRoleLists,
} from "src/stores/home";
import { getData, getStatistics } from "src/stores/data";
enableExporting(HighchartsColumn);
enableExporting(HighchartsStock);

export const StatisticTab = () => {
  return (
    <Fragment>
      <img src={img.statisticImg} alt="" />
      <span>Thống kê trong lớp dữ liệu</span>
    </Fragment>
  );
};

const Statistic = () => {
  const dispatch = useDispatch();
  const selectedLayer = useSelector(currentLayer);
  const selectedDistrict = useSelector(currentDistrict);
  const statisticInfo = useSelector(statisticInfos);
  const [viewRoleLists] = useSelector(getViewRoleLists);
  const [rows, setRows] = useState([]);
  const [columns, setColumns] = useState([]);
  const thisStatistic = useSelector(getStatistics);
  const data = useSelector(getData);
  const thisData = data[selectedLayer];
  const [dataExport, setDataExport] = useState([]);
  const [optionsLineChart, setOptionsLineChart] = useState(initLineChart);
  const [optionsColumnChart, setOptionsColumnChart] = useState(initColumnChart);

  const handleCloseTable = useCallback(() => {
    dispatch(toggleAttributeTable());
    dispatch(setTabTable(null));
  }, [dispatch]);

  const handleExportData = useCallback(() => {
    const dataExport = columns?.map((col) => ({
      label: col.header,
      key: col.accessorKey,
    }));
    setDataExport(dataExport);
  }, [columns]);

  const handleExportReport = useCallback(() => {
    const thisFrom = statisticInfo?.chartType;
    const thisSource =
      thisFrom === "bar" || thisFrom === "column"
        ? { ...optionsColumnChart, report: thisStatistic }
        : { ...optionsLineChart, report: thisData };
    const exportData = reports({
      type: statisticInfo.statisticType,
      data: thisSource,
    });
    generateDocument({
      layer: selectedLayer,
      data: exportData,
    });
  }, [
    selectedLayer,
    thisStatistic,
    thisData,
    optionsColumnChart,
    optionsLineChart,
    statisticInfo,
  ]);

  useEffect(() => {
    if (!thisStatistic?.length) return;
    const columnsCol = getTableColumns(thisStatistic, selectedLayer);
    setRows(thisStatistic);
    setColumns(columnsCol);
    return;
  }, [thisStatistic, thisData, statisticInfo, selectedLayer]);

  useEffect(() => {
    if (!thisStatistic?.length || !selectedLayer) return;
    const thisDistrict = viewRoleLists?.find(
      (r) => r.districtIDOrigin === selectedDistrict
    );
    const districtName =
      selectedDistrict !== "null"
        ? thisDistrict?.districtName
        : process.env.REACT_APP_FULLDISTRICTTITLE;
    const [formDate, toDate] = statisticInfo.time;
    const title = `Biểu đồ thống kê ${selectedLayer} - ${districtName}`;
    const subtitle = formDate && toDate ? `Từ ${formDate} đến ${toDate}` : "";

    const generalFts = config.statistics[selectedLayer];
    const yaxis = generalFts?.yaxis?.title;
    const unit = generalFts?.unit?.name;
    const unitAlt = generalFts?.unit?.alternate;
    const colors = generalFts?.colors;
    const features = generalFts?.features;
    const xaxis = generalFts?.xaxis?.name;
    const xaxisAlt = generalFts?.xaxis?.alternate;
    const element = generalFts?.element;
    const tags = { format: `{value} ${unit || unitAlt}` };
    const renewData = deleteSumRow({ layer: selectedLayer, data: thisStatistic });

    switch (statisticInfo.chartType) {
      case "column":
        const districtsLists = renewData?.map(
          (dt) => dt[xaxis] ?? dt[xaxisAlt]
        );
        const featuresLists = renewData?.map((dt) => dt[element?.name]);
        const merFeatures = removeValDuplicates(featuresLists);
        const merDistricts = removeValDuplicates(districtsLists);

        const yAxis = renewData[0][unit]
          ? removeValDuplicates(renewData?.map((dt) => dt[unit]))?.map((u) => ({
              labels: { format: `{value}${u} ` },
              title: { text: u },
              visible: false,
            }))
          : [
              {
                labels: !renewData[0][unit] && { format: `{value} ${unitAlt}` },
                title: { text: yaxis },
                visible: true,
              },
            ];

        const column = (f) => {
          const thisFeature = renewData?.find((dt) => dt[element?.name] === f);
          const thisUnit = thisFeature?.[unit];
          const thisYaxis = yAxis.findIndex((y) => y.title.text === thisUnit);
          return thisYaxis !== -1 ? thisYaxis : 0;
        };

        const color = (f) => {
          const thisColor = renewData?.find((dt) => dt[element?.name] === f);
          return thisColor?.[element?.color] || colors[0];
        };

        const quantity = (f, d) => {
          const defQuantity = null;
          const thisQuantity = renewData?.find((dt) => {
            const thisColumn = dt[xaxis] ?? dt[xaxisAlt];
            return dt[element?.name] === f && thisColumn === d;
          });
          return thisQuantity?.[element?.value] || defQuantity;
        };

        const district = (f) => {
          return merDistricts?.map((d) => ({
            name: d,
            y: quantity(f, d),
          }));
        };

        const feature = merFeatures?.map((f) => ({
          name: f ?? "Đang cập nhật",
          data: district(f),
          color: color(f),
          yAxis: column(f),
          dataLabels: {
            enabled: true,
            allowOverlap: true,
            format: "{y}",
          },
        }));

        setOptionsColumnChart((prev) => ({
          ...prev,
          series: feature,
          xAxis: { ...prev.xAxis, categories: merDistricts, opposite: false },
          yAxis: yAxis,
          title: { ...prev.title, text: title },
          subtitle: { ...prev.subtitle, text: subtitle },
          legend: { ...prev.legend, enabled: true },
        }));
        return;

      case "bar":
        const level = renewData?.map((dt) =>
          dt[xaxis] ? dt[xaxis] : "Đang cập nhật"
        );
        const columnSeries = features?.map((f) => ({
          name: f.label,
          data: renewData?.map((dt) => ({
            name: dt[xaxis],
            y: dt[f.id],
            color: dt[f.color],
          })),
        }));
        setOptionsColumnChart((prev) => ({
          ...prev,
          series: columnSeries,
          xAxis: { ...prev.xAxis, categories: level },
          yAxis: {
            ...prev.yAxis,
            title: { text: yaxis },
            labels: { ...tags },
            opposite: false,
          },
          title: { ...prev.title, text: title },
          subtitle: { ...prev.subtitle, text: subtitle },
          legend: { ...prev.legend, enabled: false },
          colors: colors,
        }));
        return;

      case "line":
        const realData =
          statisticInfo.statisticType === "detail" ? renewData : thisData;
        const times = realData?.map(
          (dt) => dt[xaxis]?.slice(0, 10) ?? dt[xaxisAlt]
        );
        const lineSeries = features
          ?.filter((f) => realData?.some((dt) => dt[f.id]))
          ?.map((f) => ({
            name: f.label,
            data: realData?.map((dt) => Number(dt[f.id])),
            color: f.color,
          }));
        setOptionsLineChart((prev) => ({
          ...prev,
          series: lineSeries,
          xAxis: { ...prev.xAxis, categories: times },
          yAxis: {
            ...prev.yAxis,
            title: { text: yaxis },
            labels: { ...tags },
            opposite: false,
          },
          title: { ...prev.title, text: title },
          subtitle: { ...prev.subtitle, text: subtitle },
          colors: colors,
        }));
        return;

      default:
        return;
    }
  }, [
    viewRoleLists,
    selectedLayer,
    selectedDistrict,
    thisData,
    thisStatistic,
    statisticInfo,
  ]);

  useEffect(() => {
    if (thisStatistic || thisData) return;
    setRows([]);
    setColumns([]);
    setOptionsLineChart(initLineChart);
    setOptionsColumnChart(initColumnChart);
  }, [thisStatistic, thisData, selectedLayer]);

  return (
    <div className="tab-statistic">
      <div className="statistic-bar">
        <div className="statistic-bar-title">
          <span>BẢNG THỐNG KÊ</span>
        </div>
        <div className="statistic-bar-action">
          <ReportRole
            _thisDistrict={selectedDistrict}
            _thisLayer={selectedLayer}
          >
            <CSVLink
              className="export-statistic"
              data={rows}
              headers={dataExport}
              asyncOnClick={true}
              onClick={handleExportData}
              filename={`baocaothongke_${convertLanguages(selectedLayer)}`}
            >
              <img src={img.excelImg} alt="" />
            </CSVLink>
            <button className="export-statistic" onClick={handleExportReport}>
              <img src={img.wordImg} alt="" />
            </button>
          </ReportRole>
          <Fragment>
            <button className="statistic-close" onClick={handleCloseTable}>
              <img src={img.closeImg} alt="" />
            </button>
          </Fragment>
        </div>
      </div>
      <div className="statistic-content">
        <div className="statistic-chart">
          {(statisticInfo.chartType === "bar" ||
            statisticInfo.chartType === "column") && (
            <HighchartsReact
              highcharts={HighchartsColumn}
              options={optionsColumnChart}
            />
          )}
          {statisticInfo.chartType === "line" && (
            <HighchartsReact
              highcharts={HighchartsStock}
              options={optionsLineChart}
              constructorType={"stockChart"}
            />
          )}
        </div>
        <div className="statistic-table">
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
  );
};

export default Statistic;
