import { useDispatch } from "react-redux";
import useAxiosJWT from "./useAxiosJWT";
import useToast from "./useToast";
import config from "src/config";
import { convertQuery } from "src/tools/convertQuery";
import { findMaximum, findMinium } from "src/tools/findExtremum";

const extremumDate = (arrDate, type) => {
  const dateColumn = process.env.REACT_APP_DATECOLUMN;
  const formatedDateColumn = process.env.REACT_APP_FORMATEDDATECOLUMN;
  const maxDate = findMaximum(arrDate, formatedDateColumn);
  const minDate = findMinium(arrDate, formatedDateColumn);
  if (type === "detail") {
    const maxYear = new Date(maxDate?.[formatedDateColumn])?.getFullYear();
    const minYear = new Date(minDate?.[formatedDateColumn])?.getFullYear();
    return [minYear, maxYear];
  }
  const fromDay = minDate?.[dateColumn]?.slice(0, 10);
  const toDay = maxDate?.[dateColumn]?.slice(0, 10);
  return [fromDay, toDay];
};

const useStatistic = () => {
  const axiosJWT = useAxiosJWT();
  const dispatch = useDispatch();
  const toast = useToast();

  const statistic = (props) => {
    const { page, type, where, layer, districtID, source, setInfos } = props;
    const findedName = config.services[page][layer];
    const thisApi = findedName?.statisticApi;
    if (!thisApi) return;
    if (!source?.length || where === undefined) return;
    const matchName = config.statistics[layer];
    const queryObj = { sqlquery: where };
    const query = convertQuery(queryObj);
    thisApi({ type, districtID, query, axiosJWT, dispatch, toast });
    if (!source[0][process.env.REACT_APP_DATECOLUMN]) {
      const statisticInfo = {
        chartType: matchName.type,
        statisticType: type,
        time: [],
      };
      dispatch(setInfos(statisticInfo));
      return;
    }
    const arrDate = source
      .filter((dt) => {
        const defaultDate = dt[process.env.REACT_APP_DATECOLUMN];
        return defaultDate !== null;
      })
      .map((dt) => {
        const defaultDate = dt[process.env.REACT_APP_DATECOLUMN];
        const formatedDate = defaultDate.split("/").reverse().join("-");
        const newDate = {
          [process.env.REACT_APP_DATECOLUMN]: defaultDate,
          [process.env.REACT_APP_FORMATEDDATECOLUMN]: formatedDate,
        };
        return newDate;
      });
    const extremumTime = extremumDate(arrDate, type);
    const statisticInfo = {
      chartType: matchName.type,
      statisticType: type,
      time: extremumTime,
    };
    dispatch(setInfos(statisticInfo));
  };

  return statistic;
};

export default useStatistic;
