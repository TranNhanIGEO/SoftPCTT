import { firstDay, lastDay } from "src/tools/findDay";
import EnterDDMMYYYY from "./EnterDDMMYYYY";
import EnterDDinYYYY from "./EnterMMinYYYY";

export const timeTypes = {
  ddmmyyyy: {
    title: "Ngày/tháng/năm",
    views: ["year", "month", "day"],
    format: "DD/MM/YYYY",
    component: EnterDDMMYYYY,
    compare: {
      only: "onlyTime",
      onCompare: (val) => {
        const dd = val["$D"];
        const mm = val["$M"];
        const yyyy = val["$y"];
        const date = `${yyyy}-${mm + 1}-${dd}`;
        return date;
      },
    },
    range: {
      start: "startTime",
      end: "endTime",
      onRange: (val) => {
        const date = timeTypes.ddmmyyyy.compare.onCompare(val);
        return { startDate: date, endDate: date };
      },
    },
  },
  mmyyyy: {
    title: "Tháng/năm",
    views: ["month", "year"],
    format: "MM/YYYY",
    component: EnterDDMMYYYY,
    compare: {
      only: "onlyTime",
      onCompare: (val) => {
        const mm = val["$M"];
        const yyyy = val["$y"];
        const date = `${yyyy}-${mm + 1}`;
        return date;
      },
    },
    range: {
      start: "startTime",
      end: "endTime",
      onRange: (val) => {
        const mm = val["$M"];
        const yyyy = val["$y"];
        const startDate = `${yyyy}-${mm + 1}-${firstDay(yyyy, mm)}`;
        const endDate = `${yyyy}-${mm + 1}-${lastDay(yyyy, mm)}`;
        return { startDate: startDate, endDate: endDate };
      },
    },
  },
  yyyy: {
    title: "Năm",
    views: ["year"],
    format: "YYYY",
    component: EnterDDMMYYYY,
    compare: {
      only: "onlyTime",
      onCompare: (val) => {
        const yyyy = val["$y"];
        const date = yyyy.toString();
        return date;
      },
    },
    range: {
      start: "startTime",
      end: "endTime",
      onRange: (val) => {
        const minDay = "01";
        const maxDay = "31";
        const minMonth = "01";
        const maxMonth = "12";
        const yyyy = val["$y"];
        const startDate = `${yyyy}-${minMonth}-${minDay}`;
        const endDate = `${yyyy}-${maxMonth}-${maxDay}`;
        return { startDate: startDate, endDate: endDate };
      },
    },
  },
  mminyyyy: {
    title: "Các tháng trong năm",
    component: EnterDDinYYYY,
  },
};
