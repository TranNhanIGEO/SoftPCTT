import { convertFormData } from "src/tools/convertFormData";
import { toggleLoading } from "src/stores/global";
import {
  apiGetDistrictData,
  apiGetTrafficData,
  apiGetWaterSystemData,
  apiGetDepressionData,
  apiGetStormData,
  apiGetTornadoData,
  apiGetFailureLineData,
  apiGetFailurePointData,
  apiGetSalinityData,
  apiGetHotData,
  apiGetRainData,
  apiGetWaterLevelData,
  apiGetLakeData,
  apiGetEmbankmentData,
  apiGetDikeData,
  apiGetSewerData,
  apiGetWarningMarkData,
  apiGetWarningSignData,
  apiGetDamageData,
  apiGetWeakPointData,
  apiGetSafePointData,
  apiGetDirectionRoute,
  apiGetForestFireData,
  apiGetParkZoneData,
  apiGetDirectionData,
  apiGetLakeSystemData,
  apiGetIslandData,
} from "src/api/home";
import {
  setDistrictData,
  setIslandData,
  setTrafficData,
  setWaterSystemData,
  setDataMaxID,
  setTabPanel,
  setSaveWhereClauses,
  setDirectionSystem,
} from "src/stores/home";
import { setStatistics, setData } from "src/stores/data";

const formType = {
  "application/json": (data) => {
    const keys = Object.keys(data);
    const newData = keys.reduce((acc, key) => {
      acc[key] = data[key] || null;
      return acc;
    }, {});
    return newData;
  },
  "multipart/form-data": (data) => {
    const newData = convertFormData(data);
    return newData;
  },
};

export const apiShowData = async ({ thisAPI, id, axiosJWT }) => {
  try {
    const onlySlugStr = "getonly";
    const res = await axiosJWT.get(`${thisAPI}/${onlySlugStr}/${id}`);
    const data = res.data;
    const dates = ["ngay", "ngayvb"];
    const date = dates.find((d) => data[d]);
    if (!data?.[date]) return data;
    const newDate = data?.[date]?.split("/")?.reverse()?.join("-");
    const newData = { ...data, [date]: newDate };
    return newData;
  } catch (err) {
    return {};
  }
};

export const apiMaxDataID = async ({ thisAPI, axiosJWT, dispatch }) => {
  try {
    const maxIDStr = "getmaxid";
    const res = await axiosJWT.get(`${thisAPI}/${maxIDStr}`);
    dispatch(setDataMaxID(res.data));
  } catch (err) {
    dispatch(setDataMaxID(null));
  }
};

export const apiCreateData = async ({
  thisAPI,
  thisForm,
  infoData,
  memberID,
  axiosJWT,
  navigate,
  toast,
}) => {
  try {
    const createStr = "add";
    const addedData = !Array.isArray(infoData) ? [infoData] : infoData;
    const multiCreate = Promise.all(
      addedData.map(async (data) => {
        const formData = formType[thisForm](data);
        const res = await axiosJWT.post(
          `${thisAPI}/${createStr}/${memberID}`,
          formData,
          { headers: { "Content-Type": thisForm } }
        );
        return res.data;
      })
    );
    multiCreate
      .then((res) => createSuccess(res))
      .catch((err) => createFailed(err));
    const createSuccess = (res) => {
      navigate("/management");
      toast.success({ title: "Thông báo!", message: res?.[0] });
    };
    const createFailed = (err) => {
      toast.error({ title: "Cảnh báo!", message: err?.response?.data });
    };
  } catch (err) {
    toast.error({ title: "Cảnh báo!", message: err?.response?.data });
  }
};

export const apiUpdateData = async ({
  thisAPI,
  thisForm,
  id,
  infoData,
  memberID,
  axiosJWT,
  navigate,
  toast,
}) => {
  try {
    const updateStr = "update";
    const formData = formType[thisForm](infoData);
    const res = await axiosJWT.put(
      `${thisAPI}/${updateStr}/${id}/${memberID}`,
      formData,
      { headers: { "Content-Type": thisForm } }
    );
    navigate("/management");
    toast.success({ title: "Thông báo!", message: res.data });
  } catch (err) {
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiDeleteData = async ({
  thisAPI,
  id,
  memberID,
  axiosJWT,
  navigate,
  toast,
}) => {
  try {
    const deleteStr = "delete";
    const res = await axiosJWT.delete(
      `${thisAPI}/${deleteStr}/${id}/${memberID}`
    );
    navigate("/management");
    toast.success({ title: "Thông báo!", message: res.data });
  } catch (err) {
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiDistrictData = async ({ axiosJWT, dispatch }) => {
  dispatch(toggleLoading());
  try {
    const res = await axiosJWT.get(apiGetDistrictData);
    dispatch(setDistrictData([res.data]));
  } catch (err) {
    dispatch(setDistrictData([]));
  }
};

export const apiIslandData = async ({ axiosJWT, dispatch }) => {
  dispatch(toggleLoading());
  try {
    const res = await axiosJWT.get(apiGetIslandData);
    dispatch(setIslandData([res.data]));
  } catch (err) {
    dispatch(setIslandData([]));
  }
};

export const apiDirectionRoute = async (
  type,
  startCoords,
  endCoords,
  params,
  axiosMAP,
  dispatch
) => {
  try {
    const res = await axiosMAP.get(
      `${apiGetDirectionRoute}/${type}/${startCoords};${endCoords}?${params}`
    );
    dispatch(setDirectionSystem(res.data));
  } catch (err) {
    dispatch(setDirectionSystem({}));
  }
};

export const apiTrafficData = async ({
  districtID,
  axiosJWT,
  dispatch,
  toast,
}) => {
  dispatch(toggleLoading());
  try {
    const res = await axiosJWT.get(`${apiGetTrafficData}/${districtID}`);
    dispatch(setTrafficData([res.data]));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(toggleLoading());
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiWaterSystemData = async ({
  districtID,
  axiosJWT,
  dispatch,
  toast,
}) => {
  dispatch(toggleLoading());
  try {
    const res = await axiosJWT.get(`${apiGetWaterSystemData}/${districtID}`);
    dispatch(setWaterSystemData([res.data]));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(toggleLoading());
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiDepressionData = async ({
  layer,
  districtID,
  query,
  axiosJWT,
  dispatch,
  toast,
}) => {
  dispatch(toggleLoading());
  try {
    const allSlugStr = "getall";
    const sqlquery = `sqlquery=${query}`;
    const res = await axiosJWT.get(
      `${apiGetDepressionData}/${allSlugStr}/${districtID}?${sqlquery}`
    );
    dispatch(setData({ [layer]: res.data }));
    dispatch(setSaveWhereClauses(query));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setData({ [layer]: [] }));
    dispatch(setSaveWhereClauses(undefined));
    dispatch(toggleLoading());
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiStormData = async ({
  layer,
  districtID,
  query,
  axiosJWT,
  dispatch,
  toast,
}) => {
  dispatch(toggleLoading());
  try {
    const allSlugStr = "getall";
    const sqlquery = `sqlquery=${query}`;
    const res = await axiosJWT.get(
      `${apiGetStormData}/${allSlugStr}/${districtID}?${sqlquery}`
    );
    dispatch(setData({ [layer]: res.data }));
    dispatch(setSaveWhereClauses(query));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setData({ [layer]: [] }));
    dispatch(setSaveWhereClauses(undefined));
    dispatch(toggleLoading());
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiTornadoData = async ({
  layer,
  districtID,
  query,
  axiosJWT,
  dispatch,
  toast,
}) => {
  dispatch(toggleLoading());
  try {
    const allSlugStr = "getall";
    const sqlquery = `sqlquery=${query}`;
    const res = await axiosJWT.get(
      `${apiGetTornadoData}/${allSlugStr}/${districtID}?${sqlquery}`
    );
    dispatch(setData({ [layer]: res.data }));
    dispatch(setSaveWhereClauses(query));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setData({ [layer]: [] }));
    dispatch(setSaveWhereClauses(undefined));
    dispatch(toggleLoading());
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiFailureLineData = async ({
  layer,
  districtID,
  query,
  axiosJWT,
  dispatch,
  toast,
}) => {
  dispatch(toggleLoading());
  try {
    const allSlugStr = "getall";
    const sqlquery = `sqlquery=${query}`;
    const res = await axiosJWT.get(
      `${apiGetFailureLineData}/${allSlugStr}/${districtID}?${sqlquery}`
    );
    dispatch(setData({ [layer]: res.data }));
    dispatch(setSaveWhereClauses(query));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setData({ [layer]: [] }));
    dispatch(setSaveWhereClauses(undefined));
    dispatch(toggleLoading());
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiFailurePointData = async ({
  layer,
  districtID,
  query,
  axiosJWT,
  dispatch,
  toast,
}) => {
  dispatch(toggleLoading());
  try {
    const allSlugStr = "getall";
    const sqlquery = `sqlquery=${query}`;
    const res = await axiosJWT.get(
      `${apiGetFailurePointData}/${allSlugStr}/${districtID}?${sqlquery}`
    );
    dispatch(setData({ [layer]: res.data }));
    dispatch(setSaveWhereClauses(query));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setData({ [layer]: [] }));
    dispatch(setSaveWhereClauses(undefined));
    dispatch(toggleLoading());
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiSalinityData = async ({
  layer,
  districtID,
  query,
  axiosJWT,
  dispatch,
  toast,
}) => {
  dispatch(toggleLoading());
  try {
    const allSlugStr = "getall";
    const sqlquery = `sqlquery=${query}`;
    const res = await axiosJWT.get(
      `${apiGetSalinityData}/${allSlugStr}/${districtID}?${sqlquery}`
    );
    dispatch(setData({ [layer]: res.data }));
    dispatch(setSaveWhereClauses(query));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setData({ [layer]: [] }));
    dispatch(setSaveWhereClauses(undefined));
    dispatch(toggleLoading());
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiHotData = async ({
  layer,
  districtID,
  query,
  axiosJWT,
  dispatch,
  toast,
}) => {
  dispatch(toggleLoading());
  try {
    const allSlugStr = "getall";
    const sqlquery = `sqlquery=${query}`;
    const res = await axiosJWT.get(
      `${apiGetHotData}/${allSlugStr}/${districtID}?${sqlquery}`
    );
    dispatch(setData({ [layer]: res.data }));
    dispatch(setSaveWhereClauses(query));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setData({ [layer]: [] }));
    dispatch(setSaveWhereClauses(undefined));
    dispatch(toggleLoading());
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiRainData = async ({
  layer,
  districtID,
  query,
  axiosJWT,
  dispatch,
  toast,
}) => {
  dispatch(toggleLoading());
  try {
    const allSlugStr = "getall";
    const sqlquery = `sqlquery=${query}`;
    const res = await axiosJWT.get(
      `${apiGetRainData}/${allSlugStr}/${districtID}?${sqlquery}`
    );
    dispatch(setData({ [layer]: res.data }));
    dispatch(setSaveWhereClauses(query));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setData({ [layer]: [] }));
    dispatch(setSaveWhereClauses(undefined));
    dispatch(toggleLoading());
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiWaterLevelData = async ({
  layer,
  districtID,
  query,
  axiosJWT,
  dispatch,
  toast,
}) => {
  dispatch(toggleLoading());
  try {
    const allSlugStr = "getall";
    const sqlquery = `sqlquery=${query}`;
    const res = await axiosJWT.get(
      `${apiGetWaterLevelData}/${allSlugStr}/${districtID}?${sqlquery}`
    );
    dispatch(setData({ [layer]: res.data }));
    dispatch(setSaveWhereClauses(query));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setData({ [layer]: [] }));
    dispatch(setSaveWhereClauses(undefined));
    dispatch(toggleLoading());
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiLakeData = async ({
  layer,
  districtID,
  query,
  axiosJWT,
  dispatch,
  toast,
}) => {
  dispatch(toggleLoading());
  try {
    const allSlugStr = "getall";
    const sqlquery = `sqlquery=${query}`;
    const resPoint = await axiosJWT.get(
      `${apiGetLakeData}/${allSlugStr}/${districtID}?${sqlquery}`
    );
    dispatch(setData({ [layer]: resPoint.data }));
    const resSystem = await axiosJWT.get(`${apiGetLakeSystemData}`);
    dispatch(setData({ "Hệ thống hồ chứa": resSystem.data }));
    dispatch(setSaveWhereClauses(query));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setData({ [layer]: [] }));
    dispatch(setSaveWhereClauses(undefined));
    dispatch(toggleLoading());
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiForestFireData = async ({
  layer,
  districtID,
  query,
  axiosJWT,
  dispatch,
  toast,
}) => {
  dispatch(toggleLoading());
  try {
    const allSlugStr = "getall";
    const sqlquery = `sqlquery=${query}`;
    const res = await axiosJWT.get(
      `${apiGetForestFireData}/${allSlugStr}/${districtID}?${sqlquery}`
    );
    dispatch(setData({ [layer]: res.data }));
    dispatch(setSaveWhereClauses(query));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setData({ [layer]: [] }));
    dispatch(setSaveWhereClauses(undefined));
    dispatch(toggleLoading());
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiEmbankmentData = async ({
  layer,
  districtID,
  query,
  axiosJWT,
  dispatch,
  toast,
}) => {
  dispatch(toggleLoading());
  try {
    const allSlugStr = "getall";
    const sqlquery = `sqlquery=${query}`;
    const res = await axiosJWT.get(
      `${apiGetEmbankmentData}/${allSlugStr}/${districtID}?${sqlquery}`
    );
    dispatch(setData({ [layer]: res.data }));
    dispatch(setSaveWhereClauses(query));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setData({ [layer]: [] }));
    dispatch(setSaveWhereClauses(undefined));
    dispatch(toggleLoading());
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiDikeData = async ({
  layer,
  districtID,
  query,
  axiosJWT,
  dispatch,
  toast,
}) => {
  dispatch(toggleLoading());
  try {
    const allSlugStr = "getall";
    const sqlquery = `sqlquery=${query}`;
    const res = await axiosJWT.get(
      `${apiGetDikeData}/${allSlugStr}/${districtID}?${sqlquery}`
    );
    dispatch(setData({ [layer]: res.data }));
    dispatch(setSaveWhereClauses(query));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setData({ [layer]: [] }));
    dispatch(setSaveWhereClauses(undefined));
    dispatch(toggleLoading());
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiSewerData = async ({
  layer,
  districtID,
  query,
  axiosJWT,
  dispatch,
  toast,
}) => {
  dispatch(toggleLoading());
  try {
    const allSlugStr = "getall";
    const sqlquery = `sqlquery=${query}`;
    const res = await axiosJWT.get(
      `${apiGetSewerData}/${allSlugStr}/${districtID}?${sqlquery}`
    );
    dispatch(setData({ [layer]: res.data }));
    dispatch(setSaveWhereClauses(query));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setData({ [layer]: [] }));
    dispatch(setSaveWhereClauses(undefined));
    dispatch(toggleLoading());
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiWarningMarkData = async ({
  layer,
  districtID,
  query,
  axiosJWT,
  dispatch,
  toast,
}) => {
  dispatch(toggleLoading());
  try {
    const allSlugStr = "getall";
    const sqlquery = `sqlquery=${query}`;
    const res = await axiosJWT.get(
      `${apiGetWarningMarkData}/${allSlugStr}/${districtID}?${sqlquery}`
    );
    dispatch(setData({ [layer]: res.data }));
    dispatch(setSaveWhereClauses(query));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setData({ [layer]: [] }));
    dispatch(setSaveWhereClauses(undefined));
    dispatch(toggleLoading());
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiParkZoneData = async ({
  layer,
  districtID,
  query,
  axiosJWT,
  dispatch,
  toast,
}) => {
  dispatch(toggleLoading());
  try {
    const allSlugStr = "getall";
    const sqlquery = `sqlquery=${query}`;
    const res = await axiosJWT.get(
      `${apiGetParkZoneData}/${allSlugStr}/${districtID}?${sqlquery}`
    );
    dispatch(setData({ [layer]: res.data }));
    dispatch(setSaveWhereClauses(query));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setData({ [layer]: [] }));
    dispatch(setSaveWhereClauses(undefined));
    dispatch(toggleLoading());
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiWarningSignData = async ({
  layer,
  districtID,
  query,
  axiosJWT,
  dispatch,
  toast,
}) => {
  dispatch(toggleLoading());
  try {
    const allSlugStr = "getall";
    const sqlquery = `sqlquery=${query}`;
    const res = await axiosJWT.get(
      `${apiGetWarningSignData}/${allSlugStr}/${districtID}?${sqlquery}`
    );
    dispatch(setData({ [layer]: res.data }));
    dispatch(setSaveWhereClauses(query));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setData({ [layer]: [] }));
    dispatch(setSaveWhereClauses(undefined));
    dispatch(toggleLoading());
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiDamageData = async ({
  layer,
  districtID,
  query,
  axiosJWT,
  dispatch,
  toast,
}) => {
  dispatch(toggleLoading());
  try {
    const allSlugStr = "getall";
    const sqlquery = `sqlquery=${query}`;
    const res = await axiosJWT.get(
      `${apiGetDamageData}/${allSlugStr}/${districtID}?${sqlquery}`
    );
    dispatch(setData({ [layer]: res.data }));
    dispatch(setSaveWhereClauses(query));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setData({ [layer]: [] }));
    dispatch(setSaveWhereClauses(undefined));
    dispatch(toggleLoading());
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiWeakPointData = async ({
  layer,
  districtID,
  query,
  axiosJWT,
  dispatch,
  toast,
}) => {
  dispatch(toggleLoading());
  try {
    const allSlugStr = "getall";
    const sqlquery = `sqlquery=${query}`;
    const res = await axiosJWT.get(
      `${apiGetWeakPointData}/${allSlugStr}/${districtID}?${sqlquery}`
    );
    dispatch(setData({ [layer]: res.data }));
    dispatch(setSaveWhereClauses(query));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setData({ [layer]: [] }));
    dispatch(setSaveWhereClauses(undefined));
    dispatch(toggleLoading());
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiSafePointData = async ({
  layer,
  districtID,
  query,
  axiosJWT,
  dispatch,
  toast,
}) => {
  dispatch(toggleLoading());
  try {
    const allSlugStr = "getall";
    const sqlquery = `sqlquery=${query}`;
    const res = await axiosJWT.get(
      `${apiGetSafePointData}/${allSlugStr}/${districtID}?${sqlquery}`
    );
    dispatch(setData({ [layer]: res.data }));
    dispatch(setSaveWhereClauses(query));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setData({ [layer]: [] }));
    dispatch(setSaveWhereClauses(undefined));
    dispatch(toggleLoading());
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiDirectionData = async ({
  districtID,
  isChecked,
  dispatch,
  toast,
}) => {
  try {
    if (!isChecked) return;
    const domain = process.env.REACT_APP_DOMAIN;
    const format = "pdf";
    window.open(
      `${domain}${apiGetDirectionData}/${format}/${districtID}.${format}`,
      "_blank"
    );
    const isDesktop = window.innerWidth > 1024;
    isDesktop && dispatch(setTabPanel("directionTab"));
  } catch (err) {
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiDepressionStatistic = async ({
  districtID,
  query,
  axiosJWT,
  dispatch,
  toast,
}) => {
  dispatch(toggleLoading());
  try {
    const statisticSlug = "statistics";
    const res = await axiosJWT.get(
      `${apiGetDepressionData}/${statisticSlug}/${districtID}?${query}`
    );
    dispatch(setStatistics(res.data));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setStatistics([]));
    dispatch(toggleLoading());
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiStormStatistic = async ({
  districtID,
  query,
  axiosJWT,
  dispatch,
  toast,
}) => {
  dispatch(toggleLoading());
  try {
    const statisticSlug = "statistics";
    const res = await axiosJWT.get(
      `${apiGetStormData}/${statisticSlug}/${districtID}?${query}`
    );
    dispatch(setStatistics(res.data));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setStatistics([]));
    dispatch(toggleLoading());
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiTornadoStatistic = async ({
  districtID,
  query,
  axiosJWT,
  dispatch,
  toast,
}) => {
  dispatch(toggleLoading());
  try {
    const statisticSlug = "statistics";
    const res = await axiosJWT.get(
      `${apiGetTornadoData}/${statisticSlug}/${districtID}?${query}`
    );
    dispatch(setStatistics(res.data));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setStatistics([]));
    dispatch(toggleLoading());
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiFailureLineStatistic = async ({
  type,
  districtID,
  query,
  axiosJWT,
  dispatch,
  toast,
}) => {
  dispatch(toggleLoading());
  try {
    const statisticSlug = "statistics";
    const res = await axiosJWT.get(
      `${apiGetFailureLineData}/${statisticSlug}/${type}/${districtID}?${query}`
    );
    dispatch(setStatistics(res.data));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setStatistics([]));
    dispatch(toggleLoading());
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiFailurePointStatistic = async ({
  type,
  districtID,
  query,
  axiosJWT,
  dispatch,
  toast,
}) => {
  dispatch(toggleLoading());
  try {
    const statisticSlug = "statistics";
    const res = await axiosJWT.get(
      `${apiGetFailurePointData}/${statisticSlug}/${type}/${districtID}?${query}`
    );
    dispatch(setStatistics(res.data));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setStatistics([]));
    dispatch(toggleLoading());
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiHotStatistic = async ({
  type,
  districtID,
  query,
  axiosJWT,
  dispatch,
  toast,
}) => {
  dispatch(toggleLoading());
  try {
    const statisticSlug = "statistics";
    const res = await axiosJWT.get(
      `${apiGetHotData}/${statisticSlug}/${type}/${districtID}?${query}`
    );
    dispatch(setStatistics(res.data));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setStatistics([]));
    dispatch(toggleLoading());
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiSalinityStatistic = async ({
  type,
  districtID,
  query,
  axiosJWT,
  dispatch,
  toast,
}) => {
  dispatch(toggleLoading());
  try {
    const statisticSlug = "statistics";
    const res = await axiosJWT.get(
      `${apiGetSalinityData}/${statisticSlug}/${type}/${districtID}?${query}`
    );
    dispatch(setStatistics(res.data));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setStatistics([]));
    dispatch(toggleLoading());
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiRainStatistic = async ({
  type,
  districtID,
  query,
  axiosJWT,
  dispatch,
  toast,
}) => {
  dispatch(toggleLoading());
  try {
    const statisticSlug = "statistics";
    const res = await axiosJWT.get(
      `${apiGetRainData}/${statisticSlug}/${type}/${districtID}?${query}`
    );
    dispatch(setStatistics(res.data));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setStatistics([]));
    dispatch(toggleLoading());
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiWaterLevelStatistic = async ({
  type,
  districtID,
  query,
  axiosJWT,
  dispatch,
  toast,
}) => {
  dispatch(toggleLoading());
  try {
    const statisticSlug = "statistics";
    const res = await axiosJWT.get(
      `${apiGetWaterLevelData}/${statisticSlug}/${type}/${districtID}?${query}`
    );
    dispatch(setStatistics(res.data));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setStatistics([]));
    dispatch(toggleLoading());
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiLakeStatistic = async ({
  type,
  districtID,
  query,
  axiosJWT,
  dispatch,
  toast,
}) => {
  dispatch(toggleLoading());
  try {
    const statisticSlug = "statistics";
    const res = await axiosJWT.get(
      `${apiGetLakeData}/${statisticSlug}/${type}/${districtID}?${query}`
    );
    dispatch(setStatistics(res.data));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setStatistics([]));
    dispatch(toggleLoading());
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiEmbankmentStatistic = async ({
  districtID,
  query,
  axiosJWT,
  dispatch,
  toast,
}) => {
  dispatch(toggleLoading());
  try {
    const statisticSlug = "statistics";
    const res = await axiosJWT.get(
      `${apiGetEmbankmentData}/${statisticSlug}/${districtID}?${query}`
    );
    dispatch(setStatistics(res.data));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setStatistics([]));
    dispatch(toggleLoading());
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiDikeStatistic = async ({
  districtID,
  query,
  axiosJWT,
  dispatch,
  toast,
}) => {
  dispatch(toggleLoading());
  try {
    const statisticSlug = "statistics";
    const res = await axiosJWT.get(
      `${apiGetDikeData}/${statisticSlug}/${districtID}?${query}`
    );
    dispatch(setStatistics(res.data));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setStatistics([]));
    dispatch(toggleLoading());
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiSewerStatistic = async ({
  type,
  districtID,
  query,
  axiosJWT,
  dispatch,
  toast,
}) => {
  dispatch(toggleLoading());
  try {
    const statisticSlug = "statistics";
    const res = await axiosJWT.get(
      `${apiGetSewerData}/${statisticSlug}/${type}/${districtID}?${query}`
    );
    dispatch(setStatistics(res.data));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setStatistics([]));
    dispatch(toggleLoading());
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiWarningMarkStatistic = async ({
  districtID,
  query,
  axiosJWT,
  dispatch,
  toast,
}) => {
  dispatch(toggleLoading());
  try {
    const statisticSlug = "statistics";
    const res = await axiosJWT.get(
      `${apiGetWarningMarkData}/${statisticSlug}/${districtID}?${query}`
    );
    dispatch(setStatistics(res.data));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setStatistics([]));
    dispatch(toggleLoading());
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiWarningSignStatistic = async ({
  districtID,
  query,
  axiosJWT,
  dispatch,
  toast,
}) => {
  dispatch(toggleLoading());
  try {
    const statisticSlug = "statistics";
    const res = await axiosJWT.get(
      `${apiGetWarningSignData}/${statisticSlug}/${districtID}?${query}`
    );
    dispatch(setStatistics(res.data));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setStatistics([]));
    dispatch(toggleLoading());
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiParkZoneStatistic = async ({
  districtID,
  query,
  axiosJWT,
  dispatch,
  toast,
}) => {
  dispatch(toggleLoading());
  try {
    const statisticSlug = "statistics";
    const res = await axiosJWT.get(
      `${apiGetParkZoneData}/${statisticSlug}/${districtID}?${query}`
    );
    dispatch(setStatistics(res.data));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setStatistics([]));
    dispatch(toggleLoading());
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiDamageStatistic = async ({
  type,
  districtID,
  query,
  axiosJWT,
  dispatch,
  toast,
}) => {
  dispatch(toggleLoading());
  try {
    const statisticSlug = "statistics";
    const res = await axiosJWT.get(
      `${apiGetDamageData}/${statisticSlug}/${type}/${districtID}?${query}`
    );
    dispatch(setStatistics(res.data));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setStatistics([]));
    dispatch(toggleLoading());
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiWeakPointStatistic = async ({
  districtID,
  query,
  axiosJWT,
  dispatch,
  toast,
}) => {
  dispatch(toggleLoading());
  try {
    const statisticSlug = "statistics";
    const res = await axiosJWT.get(
      `${apiGetWeakPointData}/${statisticSlug}/${districtID}?${query}`
    );
    dispatch(setStatistics(res.data));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setStatistics([]));
    dispatch(toggleLoading());
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiSafePointStatistic = async ({
  districtID,
  query,
  axiosJWT,
  dispatch,
  toast,
}) => {
  dispatch(toggleLoading());
  try {
    const statisticSlug = "statistics";
    const res = await axiosJWT.get(
      `${apiGetSafePointData}/${statisticSlug}/${districtID}?${query}`
    );
    dispatch(setStatistics(res.data));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setStatistics([]));
    dispatch(toggleLoading());
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};
