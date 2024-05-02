import { useEffect, useState, useCallback } from "react";
import { useSelector } from "react-redux";
import HighchartsReact from "highcharts-react-official";
import HighchartsColumn from "highcharts";
import { Popup } from "react-map-gl";
import { initColumnChart } from "src/utils/columnChart";
import { removeValDuplicates } from "src/tools/removeDuplicates";
import { findMaximum } from "src/tools/findExtremum";
import { getStatistics } from "src/stores/data";
import {
  currentDistrict,
  currentLayer,
  getDistrictData,
} from "src/stores/home";

const PopupChart = () => {
  const selectedLayer = useSelector(currentLayer);
  const selectedDistrict = useSelector(currentDistrict);
  const thisStatistic = useSelector(getStatistics);
  const [districtsData] = useSelector(getDistrictData);
  const [chartSeries, setChartSeries] = useState(initColumnChart);
  const [chartFeatures, setChartFeatures] = useState();
  const [chartUnits, setChartUnits] = useState([]);

  useEffect(() => {
    const isNotDamageLayer =
      selectedLayer !== "Thiệt hại do thiên tai" && selectedDistrict !== "null";
    if (isNotDamageLayer) return;
    if (!thisStatistic || !thisStatistic?.length) return;
    const districtsLists = thisStatistic.map(
      (dt) => dt["phamvithongke"] ?? dt["tenhuyen"]
    );
    const featuresLists = thisStatistic.map((dt) => dt["doituongthiethai"]);
    const unitLists = thisStatistic.map((dt) => dt["dvtthiethai"]);
    const merFeatures = removeValDuplicates(featuresLists);
    const merDistricts = removeValDuplicates(districtsLists);
    const merUnits = removeValDuplicates(unitLists);

    const max = (u) => {
      const thisUnit = thisStatistic
        .filter((dt) => dt["dvtthiethai"] === u)
        .map((dt) => dt["soluong"]);
      const maxUnit = findMaximum(thisUnit, "soluong")["soluong"];
      return maxUnit;
    };

    const yAxis = merUnits.map((u) => ({
      labels: { format: `{value}${u} ` },
      title: { text: u },
      visible: false,
      max: max(u),
    }));

    const columns = (f) => {
      const thisFeature = thisStatistic.find(
        (dt) => dt["doituongthiethai"] === f
      );
      const thisUnit = thisFeature?.["dvtthiethai"];
      const thisYaxis = yAxis.findIndex((y) => y.title.text === thisUnit);
      return thisYaxis;
    };

    const colors = (f) => {
      const thisColor = thisStatistic.find(
        (dt) => dt["doituongthiethai"] === f
      );
      return thisColor?.["mamau"];
    };

    const quantities = (f, d) => {
      const defQuantity = null;
      const thisQuantities = thisStatistic.find((dt) => {
        const thisColumn = dt["phamvithongke"] ?? dt["tenhuyen"];
        return dt["doituongthiethai"] === f && thisColumn === d;
      });
      return thisQuantities?.["soluong"] || defQuantity;
    };

    const districts = (f, d) => {
      const thisDistrict = {
        name: d,
        y: quantities(f, d),
      };
      return [thisDistrict];
    };

    const features = (d) => {
      const thisFeature = merFeatures.map((f) => ({
        name: f,
        data: districts(f, d),
        color: colors(f),
        yAxis: columns(f),
        dataLabels: {
          enabled: true,
          allowOverlap: true,
          format: "{y}",
        },
      }));
      return thisFeature;
    };

    const series = merDistricts.map((d) => features(d));

    setChartSeries(series);
    setChartFeatures(merDistricts);
    setChartUnits(yAxis);
  }, [selectedLayer, selectedDistrict, thisStatistic]);

  const chartColumn = useCallback(
    (i) => {
      if (!thisStatistic || !thisStatistic?.length) return;
      const chartColumn = {
        ...initColumnChart,
        series: chartSeries[i],
        chart: {
          ...initColumnChart.chart,
          backgroundColor: "transparent",
          width: 100,
          height: 100,
        },
        xAxis: {
          ...initColumnChart.xAxis,
          categories: [chartFeatures[i]],
          visible: false,
        },
        yAxis: chartUnits,
        navigation: {
          buttonOptions: { enabled: false },
        },
        tooltip: {
          ...initColumnChart.tooltip,
          valueSuffix: `{value}`,
        },
      };
      return chartColumn;
    },
    [thisStatistic, chartSeries, chartFeatures, chartUnits]
  );

  return (
    thisStatistic?.length &&
    chartSeries.length &&
    selectedDistrict === "null" &&
    districtsData?.map((district, i) => (
      <Popup
        key={i}
        longitude={JSON.parse(district.centroid)?.coordinates[0]}
        latitude={JSON.parse(district.centroid)?.coordinates[1]}
        anchor="center"
        maxWidth="100px"
        className="mapbox-popup-chart"
        offset={[0, 0]}
        closeOnClick={false}
        closeOnMove={false}
        closeButton={false}
        focusAfterOpen={false}
      >
        <MiniChart
          options={chartColumn}
          series={chartSeries}
          value={district}
        />
      </Popup>
    ))
  );
};

export default PopupChart;

const MiniChart = ({ options, series, value }) => {
  const idxDistricts = series?.findIndex((seri) => {
    const thisColumn = value["phamvithongke"] ?? value["tenhuyen"];
    return seri[0].data[0].name === thisColumn;
  });

  return (
    <HighchartsReact
      highcharts={HighchartsColumn}
      options={options(idxDistricts)}
    />
  );
};
