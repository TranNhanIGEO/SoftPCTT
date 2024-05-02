import EnterCompareValue from "./EnterCompareValue";
import EnterRangeValue from "./EnterRangeValue";

export const queryTypes = {
  compareValue: {
    type: ["string", "number"],
    text: "Giá trị",
    component: EnterCompareValue,
  },
  rangeValue: {
    type: ["number"],
    text: "Khoảng giá trị",
    component: EnterRangeValue,
  },
};
