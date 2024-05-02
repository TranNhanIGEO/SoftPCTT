import { toggleLoading } from "src/stores/global";
import { setSaveWhereClauses, setSolutionMaxID } from "src/stores/solution";
import { setData, setStatistics } from "src/stores/data";
import {
  apiGetWeakPointData,
  apiGetSafePointData,
  apiGetDirectionData,
  apiGetMigrationData,
  apiGetForceData,
  apiGetTransportData,
  apiGetContactData,
} from "src/api/solution";

export const apiShowSolution = async ({ thisAPI, id, axiosJWT }) => {
  try {
    const onlySlugStr = "getonly";
    const res = await axiosJWT.get(`${thisAPI}/${onlySlugStr}/${id}`);
    const data = res.data;
    const date = "ngayvb";
    if (!data?.[date]) return data;
    const newDate = data?.[date]?.split("/")?.reverse()?.join("-");
    const newData = { ...data, [date]: newDate };
    return newData;
  } catch (err) {
    return {};
  }
};

export const apiMaxSolutionID = async ({ thisAPI, axiosJWT, dispatch }) => {
  try {
    const maxIDStr = "getmaxid";
    const res = await axiosJWT.get(`${thisAPI}/${maxIDStr}`);
    dispatch(setSolutionMaxID(res.data));
  } catch (err) {
    dispatch(setSolutionMaxID(null));
  }
};

export const apiCreateSolution = async ({
  thisAPI,
  addedSolutions,
  memberID,
  axiosJWT,
  navigate,
  toast,
}) => {
  try {
    const createStr = "add";
    const multiCreate = Promise.all(
      addedSolutions.map(async (solution) => {
        const res = await axiosJWT.post(
          `${thisAPI}/${createStr}/${memberID}`,
          solution
        );
        return res.data;
      })
    );
    multiCreate
      .then((res) => createSuccess(res))
      .catch((err) => createFailed(err));
    const createSuccess = (res) => {
      navigate("/solution");
      toast.success({ title: "Thông báo!", message: res?.[0] });
    };
    const createFailed = (err) => {
      toast.error({ title: "Cảnh báo!", message: err?.response?.data });
    };
  } catch (err) {
    toast.error({ title: "Cảnh báo!", message: err?.response?.data });
  }
};

export const apiUpdateSolution = async ({
  thisAPI,
  id,
  infoSolution,
  memberID,
  axiosJWT,
  navigate,
  toast,
}) => {
  try {
    const updateStr = "update";
    const res = await axiosJWT.put(
      `${thisAPI}/${updateStr}/${id}/${memberID}`,
      infoSolution
    );
    navigate("/solution");
    toast.success({ title: "Thông báo!", message: res.data });
  } catch (err) {
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiDeleteSolution = async ({
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
    navigate("/solution");
    toast.success({ title: "Thông báo!", message: res.data });
  } catch (err) {
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiWeakPoint = async ({
  layer,
  districtID,
  query,
  axiosJWT,
  dispatch,
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
  }
};

export const apiSafePoint = async ({
  layer,
  districtID,
  query,
  axiosJWT,
  dispatch,
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
  }
};

export const apiDirection = async ({
  layer,
  districtID,
  query,
  axiosJWT,
  dispatch,
}) => {
  dispatch(toggleLoading());
  try {
    const allSlugStr = "getall";
    const sqlquery = `sqlquery=${query}`;
    const res = await axiosJWT.get(
      `${apiGetDirectionData}/${allSlugStr}/${districtID}?${sqlquery}`
    );
    dispatch(setData({ [layer]: res.data }));
    dispatch(setSaveWhereClauses(query));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setData({ [layer]: [] }));
    dispatch(setSaveWhereClauses(undefined));
    dispatch(toggleLoading());
  }
};

export const apiMigration = async ({
  layer,
  districtID,
  query,
  axiosJWT,
  dispatch,
}) => {
  dispatch(toggleLoading());
  try {
    const allSlugStr = "getall";
    const sqlquery = `sqlquery=${query}`;
    const res = await axiosJWT.get(
      `${apiGetMigrationData}/${allSlugStr}/${districtID}?${sqlquery}`
    );
    dispatch(setData({ [layer]: res.data }));
    dispatch(setSaveWhereClauses(query));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setData({ [layer]: [] }));
    dispatch(setSaveWhereClauses(undefined));
    dispatch(toggleLoading());
  }
};

export const apiForce = async ({
  layer,
  districtID,
  query,
  axiosJWT,
  dispatch,
}) => {
  dispatch(toggleLoading());
  try {
    const allSlugStr = "getall";
    const sqlquery = `sqlquery=${query}`;
    const res = await axiosJWT.get(
      `${apiGetForceData}/${allSlugStr}/${districtID}?${sqlquery}`
    );
    dispatch(setData({ [layer]: res.data }));
    dispatch(setSaveWhereClauses(query));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setData({ [layer]: [] }));
    dispatch(setSaveWhereClauses(undefined));
    dispatch(toggleLoading());
  }
};

export const apiTransport = async ({
  layer,
  districtID,
  query,
  axiosJWT,
  dispatch,
}) => {
  dispatch(toggleLoading());
  try {
    const allSlugStr = "getall";
    const sqlquery = `sqlquery=${query}`;
    const res = await axiosJWT.get(
      `${apiGetTransportData}/${allSlugStr}/${districtID}?${sqlquery}`
    );
    dispatch(setData({ [layer]: res.data }));
    dispatch(setSaveWhereClauses(query));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setData({ [layer]: [] }));
    dispatch(setSaveWhereClauses(undefined));
    dispatch(toggleLoading());
  }
};

export const apiContact = async ({
  layer,
  districtID,
  query,
  axiosJWT,
  dispatch,
}) => {
  dispatch(toggleLoading());
  try {
    const allSlugStr = "getall";
    const sqlquery = `sqlquery=${query}`;
    const res = await axiosJWT.get(
      `${apiGetContactData}/${allSlugStr}/${districtID}?${sqlquery}`
    );
    dispatch(setData({ [layer]: res.data }));
    dispatch(setSaveWhereClauses(query));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setData({ [layer]: [] }));
    dispatch(setSaveWhereClauses(undefined));
    dispatch(toggleLoading());
  }
};

export const apiMigrationStatistic = async ({
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
      `${apiGetMigrationData}/${statisticSlug}/${districtID}?${query}`
    );
    dispatch(setStatistics(res.data));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setStatistics([]));
    dispatch(toggleLoading());
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiForceStatistic = async ({
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
      `${apiGetForceData}/${statisticSlug}/${type}/${districtID}?${query}`
    );
    dispatch(setStatistics(res.data));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setStatistics([]));
    dispatch(toggleLoading());
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiTransportStatistic = async ({
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
      `${apiGetTransportData}/${statisticSlug}/${type}/${districtID}?${query}`
    );
    dispatch(setStatistics(res.data));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setStatistics([]));
    dispatch(toggleLoading());
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};
