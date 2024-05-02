import { convertFormData } from "src/tools/convertFormData";
import { toggleLoading } from "src/stores/global";
import { setData } from "src/stores/data";
import {
  apiGetPhotoMaterialData,
  apiGetVideoMaterialData,
  apiGetOrtherMaterialData,
} from "src/api/material";

export const apiShowMaterial = async ({ thisAPI, id, axiosJWT }) => {
  try {
    const onlySlugStr = "getonly";
    const res = await axiosJWT.get(`${thisAPI}/${onlySlugStr}/${id}`);
    const data = res.data;
    const dates = ["ngayhinhanh", "ngayvideo", "ngaytulieu"];
    const date = dates.find((d) => data[d]);
    if (!data?.[date]) return data;
    const newDate = data?.[date]?.split("/")?.reverse()?.join("-");
    const newData = { ...data, [date]: newDate };
    return newData;
  } catch (err) {
    return {};
  }
};

export const apiCreateMaterial = async ({
  thisAPI,
  materialInfo,
  memberID,
  axiosJWT,
  navigate,
  toast,
}) => {
  try {
    const createStr = "add";
    const formData = convertFormData(materialInfo);
    // console.log(...formData)
    const res = await axiosJWT.post(
      `${thisAPI}/${createStr}/${memberID}`,
      formData,
      { headers: { "Content-Type": "multipart/form-data" } }
    );
    navigate("/material");
    toast.success({ title: "Thông báo!", message: res?.data });
  } catch (err) {
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiUpdateMaterial = async ({
  thisAPI,
  id,
  materialInfo,
  memberID,
  axiosJWT,
  navigate,
  toast,
}) => {
  try {
    const updateStr = "update";
    const formData = convertFormData(materialInfo);
    const res = await axiosJWT.put(
      `${thisAPI}/${updateStr}/${id}/${memberID}`,
      formData,
      { headers: { "Content-Type": "multipart/form-data" } }
    );
    navigate("/material");
    toast.success({ title: "Thông báo!", message: res.data });
  } catch (err) {
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiDeleteMaterial = async ({
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
    navigate("/material");
    toast.success({ title: "Thông báo!", message: res.data });
  } catch (err) {
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiPhotoMaterial = async ({
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
      `${apiGetPhotoMaterialData}/${allSlugStr}/${districtID}?${sqlquery}`
    );
    dispatch(setData({ [layer]: res.data }));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setData({ [layer]: [] }));
    dispatch(toggleLoading());
  }
};

export const apiVideoMaterial = async ({
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
      `${apiGetVideoMaterialData}/${allSlugStr}/${districtID}?${sqlquery}`
    );
    dispatch(setData({ [layer]: res.data }));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setData({ [layer]: [] }));
    dispatch(toggleLoading());
  }
};

export const apiOrtherMaterial = async ({
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
      `${apiGetOrtherMaterialData}/${allSlugStr}/${districtID}?${sqlquery}`
    );
    dispatch(setData({ [layer]: res.data }));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(setData({ [layer]: [] }));
    dispatch(toggleLoading());
  }
};
