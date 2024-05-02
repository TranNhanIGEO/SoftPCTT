import { toggleLoading } from "src/stores/global";
import { apiGetMemberRoles } from "src/api/global";
import {
  apiAddMember,
  apiAddMemberRole,
  apiGetMembers,
  apiEditMember,
  apiRemoveMember,
  apiRemoveMemberRole,
  apiEditMemberPassword,
  apiGetHistory,
} from "src/api/admin";
import {
  getAllHistories,
  getAllMembers,
  getUserAccount,
  getUserRoles,
  initState,
} from "src/stores/admin";

export const apiShowAllMembers = async ({ axiosJWT, dispatch }) => {
  dispatch(toggleLoading());
  try {
    const res = await axiosJWT.get(apiGetMembers);
    dispatch(getAllMembers([res.data]));
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(getAllMembers([]));
    dispatch(toggleLoading());
  }
};

export const apiShowMemberAccount = async ({ id, axiosJWT, dispatch }) => {
  try {
    const res = await axiosJWT.get(`${apiGetMembers}/${id}`);
    const resData = { ...res.data, password: null };
    dispatch(getUserAccount(resData));
  } catch (err) {
    const initAccount = initState.userAccount;
    dispatch(getUserAccount(initAccount));
  }
};

export const apiShowMemberRoles = async ({ id, axiosJWT, dispatch }) => {
  try {
    const res = await axiosJWT.get(`${apiGetMemberRoles}/${id}`);
    dispatch(getUserRoles(res.data));
  } catch (err) {
    dispatch(getUserRoles([]));
  }
};

export const apiCreateMember = async ({
  axiosJWT,
  userAccount,
  userRoleList,
  navigate,
  toast,
}) => {
  try {
    const res = await axiosJWT.post(apiAddMember, userAccount);
    userRoleList.forEach(async (userRole) => {
      await axiosJWT.post(apiAddMemberRole, userRole);
    });
    navigate("/admin");
    toast.success({ title: "Thông báo!", message: res.data });
  } catch (err) {
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiUpdateMember = async ({
  axiosJWT,
  userAccount,
  userRoleList,
  navigate,
  toast,
}) => {
  try {
    const id = userAccount["memberid"];
    const { password, isdeleted, ...userInfo } = userAccount;
    const res = await axiosJWT.put(`${apiEditMember}/${id}`, userInfo);
    await axiosJWT.delete(`${apiRemoveMemberRole}/${id}`, userInfo);
    userRoleList.forEach(async (userRole) => {
      await axiosJWT.post(apiAddMemberRole, userRole);
    });
    navigate("/admin");
    toast.success({ title: "Thông báo!", message: res.data });
  } catch (err) {
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiUpdateMemberPassword = async ({
  updatePassWord,
  axiosJWT,
  toast,
}) => {
  try {
    const res = await axiosJWT.post(apiEditMemberPassword, updatePassWord);
    toast.success({ title: "Thông báo!", message: res.data });
  } catch (err) {
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiDeleteMember = async ({ id, axiosJWT, navigate, toast }) => {
  try {
    const res = await axiosJWT.delete(`${apiRemoveMember}/${id}`);
    navigate("/admin");
    toast.success({ title: "Thông báo!", message: res.data });
  } catch (err) {
    toast.error({ title: "Cảnh báo!", message: err.response?.data });
  }
};

export const apiShowAllHistories = async ({ layer, axiosJWT, dispatch }) => {
  dispatch(toggleLoading());
  try {
    const res = await axiosJWT.get(`${apiGetHistory}/${layer}`);
    dispatch(getAllHistories([res.data]))
    dispatch(toggleLoading());
  } catch (err) {
    dispatch(getAllHistories([]))
    dispatch(toggleLoading());
  }
};
