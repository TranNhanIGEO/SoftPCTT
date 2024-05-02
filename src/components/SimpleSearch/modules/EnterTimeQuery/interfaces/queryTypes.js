import EnterCompareTime from "./EnterCompareTime";
import EnterRangeTime from "./EnterRangeTime";

export const queryTypes = {
  compareTime: {
    text: "Thời gian",
    component: EnterCompareTime,
  },
  rangeTime: {
    text: "Khoảng thời gian",
    component: EnterRangeTime,
  },
};
