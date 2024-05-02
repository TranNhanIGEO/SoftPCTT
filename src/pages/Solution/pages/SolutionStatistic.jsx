import { Fragment, useCallback, useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import MaterialReactTable from "material-react-table";
import { MRT_Localization_VI } from "material-react-table/locales/vi";
import HighchartsColumn from "highcharts";
import HighchartsReact from "highcharts-react-official";
import enableExporting from "highcharts/modules/exporting";
import config from "src/config";
import { ReportRole } from "src/middleware/Roles";
import { img } from "src/assets";
import { CSVLink } from "react-csv";
import { convertLanguages } from "src/tools/convertLanguages";
import { getTableColumns } from "src/utils/getTableColumns";
import { initColumnChart } from "src/utils/columnChart";
import { removeValDuplicates } from "src/tools/removeDuplicates";
import { generateDocument } from "src/utils/generateDocument";
import { reports } from "src/export/reports";
import { deleteSumRow } from "src/utils/deleteSumRow";
import { getStatistics } from "src/stores/data";
import {
  statisticInfos,
  currentLayer,
  currentDistrict,
  setTabTable,
  getViewRoleLists,
} from "src/stores/solution";
enableExporting(HighchartsColumn);

const SolutionStatistic = () => {
  const dispatch = useDispatch();
  const selectedLayer = useSelector(currentLayer);
  const selectedDistrict = useSelector(currentDistrict);
  const statisticInfo = useSelector(statisticInfos);
  const thisStatistic = useSelector(getStatistics);
  const [viewRoleLists] = useSelector(getViewRoleLists);
  const [rows, setRows] = useState([]);
  const [columns, setColumns] = useState([]);
  const [dataExport, setDataExport] = useState([]);
  const [optionsColumnChart, setOptionsColumnChart] = useState(initColumnChart);
  const configStatistics = config.statistics[selectedLayer];

  const handleCloseStatistic = useCallback(() => {
    dispatch(setTabTable("solutionTab"));
  }, [dispatch]);

  const handleExportData = useCallback(() => {
    const dataExport = columns?.map((col) => ({
      label: col.header,
      key: col.accessorKey,
    }));
    setDataExport(dataExport);
  }, [columns]);

  const handleExportReport = useCallback(() => {
    const thisData = { ...optionsColumnChart, report: thisStatistic };
    const exportData = reports({
      type: statisticInfo.statisticType,
      data: thisData,
    });
    generateDocument({
      layer: selectedLayer,
      data: exportData,
    });
  }, [selectedLayer, thisStatistic, optionsColumnChart, statisticInfo]);

  useEffect(() => {
    if (!thisStatistic?.length) return;
    const columnsCol = getTableColumns(thisStatistic, selectedLayer);
    setRows(thisStatistic);
    setColumns(columnsCol);
  }, [thisStatistic, statisticInfo, selectedLayer]);

  useEffect(() => {
    if (!thisStatistic?.length || !selectedLayer) return;
    const thisDistrict = viewRoleLists?.find(
      (r) => r.districtID === selectedDistrict
    );
    const districtName =
      selectedDistrict !== "null"
        ? thisDistrict?.districtName
        : process.env.REACT_APP_FULLDISTRICTTITLE;
    const [formDate, toDate] = statisticInfo.time;
    const title = `Biểu đồ thống kê ${selectedLayer} - ${districtName}`;
    const subtitle = formDate && toDate ? `Từ ${formDate} đến ${toDate}` : "";

    const yaxis = configStatistics?.yaxis?.title;
    const unit = configStatistics?.unit?.name;
    const unitAlt = configStatistics?.unit?.alternate;
    const colors = configStatistics?.colors;
    const features = configStatistics?.features;
    const xaxis = configStatistics?.xaxis?.name;
    const xaxisAlt = configStatistics?.xaxis?.alternate;
    const element = configStatistics?.element;
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
          const randomColor = Math.floor(Math.random() * 16777215).toString(16);
          return thisColor?.[element?.color] || "#" + randomColor;
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
          xAxis: { ...prev.xAxis, categories: merDistricts },
          yAxis: yAxis,
          title: { ...prev.title, text: title },
          subtitle: { ...prev.subtitle, text: subtitle },
          legend: { ...prev.legend, enabled: true },
        }));
        return;

      case "bar":
        const level = renewData?.map((dt) => dt[xaxis]);
        const columnSeries = features?.map((f) => ({
          name: f.label,
          data: renewData?.map((dt) => ({
            name: dt[xaxis],
            y: dt[f.id],
          })),
        }));
        setOptionsColumnChart((prev) => ({
          ...prev,
          series: columnSeries,
          xAxis: { ...prev.xAxis, categories: level },
          yAxis: { ...prev.yAxis, title: { text: yaxis }, labels: { ...tags } },
          title: { ...prev.title, text: title },
          subtitle: { ...prev.subtitle, text: subtitle },
          legend: { ...prev.legend, enabled: false },
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
    configStatistics,
    thisStatistic,
    statisticInfo,
  ]);

  return (
    <div className="solution-statistic">
      <div className="solution-statistic-bar">
        <div className="solution-statistic-title">
          <span>BẢNG THỐNG KÊ</span>
        </div>
        <div className="solution-statistic-action">
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
            <button className="statistic-close" onClick={handleCloseStatistic}>
              <img src={img.closeImg} alt="" />
            </button>
          </Fragment>
        </div>
      </div>
      <div className="solution-statistic-content">
        <div className="solution-statistic-chart">
          <HighchartsReact
            highcharts={HighchartsColumn}
            options={optionsColumnChart}
          />
        </div>
        <div className="solution-statistic-table">
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

export default SolutionStatistic;
