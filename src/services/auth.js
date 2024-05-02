import { axiosAPI } from "src/tools/axiosAPI";
import { toggleLoading } from "src/stores/global";
import { apiLoginUser, apiLogoutUser, apiRefresh } from "src/api/auth";
import {
  loginSuccess,
  loginFailed,
  logoutSuccess,
  logoutFailed,
} from "src/stores/auth";

export const loginUser = async ({ userAccount, dispatch, navigate }) => {
  try {
    const res = await axiosAPI.post(apiLoginUser, userAccount);
    dispatch(loginSuccess(res.data));
    navigate("/");
    dispatch(toggleLoading(true));
  } catch (err) {
    dispatch(loginFailed(err.response?.data));
  }
};

export const logoutUser = async ({ refresh, axiosJWT, dispatch, navigate }) => {
  try {
    await axiosJWT.post(apiLogoutUser, { rt: refresh });
    dispatch(logoutSuccess());
    navigate("/login");
  } catch (err) {
    dispatch(logoutFailed(err.response?.data));
  }
};

export const refreshUser = async ({ token, refresh, dispatch, navigate }) => {
  try {
    const res = await axiosAPI.post(apiRefresh, { at: token, rt: refresh });
    dispatch(loginSuccess(res.data));
  } catch (err) {
    dispatch(loginFailed(err.response?.data));
    navigate("/login");
  }
};
