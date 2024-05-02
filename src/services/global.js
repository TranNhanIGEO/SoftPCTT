import { axiosAPI } from "src/tools/axiosAPI";
import { apiGetCountLogin } from "src/api/global";
import { setCountLogin } from "src/stores/global";
import { apiGetRoleLists, apiGetMemberRoles } from "src/api/global";
import { setRoleLists, setUserRoleLists } from "src/stores/global";

export const apiCountLogin = async ({ dispatch }) => {
  try {
    const res = await axiosAPI.get(apiGetCountLogin);
    dispatch(setCountLogin(res.data));
  } catch (err) {
    dispatch(setCountLogin([]));
  }
};

export const apiRoleLists = async ({ page, axiosJWT, dispatch }) => {
  try {
    const res = await axiosJWT.get(`${apiGetRoleLists}/${page}`);
    dispatch(setRoleLists([res.data]));
  } catch (err) {
    dispatch(setRoleLists([]));
  }
};

export const apiShowUserRoles = async ({ memberid, axiosJWT, dispatch }) => {
  try {
    const res = await axiosJWT.get(`${apiGetMemberRoles}/${memberid}`);
    dispatch(setUserRoleLists([res.data]));
  } catch (err) {
    dispatch(setUserRoleLists([]));
  }
};
