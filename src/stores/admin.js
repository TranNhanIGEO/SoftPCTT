import { createSlice } from "@reduxjs/toolkit";

export const initState = {
  isAssignRole: false,
  allRoles: [],
  userAccount: {
    memberid: "",
    username: "",
    password: "",
    fullname: "",
    unit: "",
    department: "",
    phone: "",
    email: "",
  },
  userRoles: {
    memberid: "",
    roleid: "",
    mahuyen: "",
    malopdulieu: [],
  },
  userRoleLists: [],
  allMembers: [],
  historyLayers: [],
  currentLayer: undefined,
  allHistories: [],
};

export const adminSlice = createSlice({
  name: "admin",
  initialState: initState,
  reducers: {
    toggleAssignRole: (state, actions) => {
      state.isAssignRole = actions.payload ?? !state.isAssignRole;
    },
    setAllRoles: (state, actions) => {
      state.allRoles = actions.payload;
    },
    setUserAccount: (state, actions) => {
      state.userAccount = actions.payload;
    },
    setUserRole: (state, action) => {
      state.userRoles = action.payload;
    },
    toggleDistrictRoles: (state, actions) => {
      const hasDistrict = state.userRoleLists.some(
        (role) => role["mahuyen"] === actions.payload["mahuyen"]
      );
      hasDistrict
        ? (state.userRoleLists = state.userRoleLists.filter(
            (role) => role["mahuyen"] !== actions.payload["mahuyen"]
          ))
        : (state.userRoleLists = [
            ...state.userRoleLists,
            { ...state.userRoles, ...actions.payload },
          ]);
    },
    changeSelectAllDistricts: (state, actions) => {
      const clearDistrictRoles = state.userRoleLists.filter((role) =>
        actions.payload.some((r) => r.roleid !== role.roleid)
      );
      state.userRoleLists = [...clearDistrictRoles, ...actions.payload];
    },
    unChangeSelectAllDistricts: (state, actions) => {
      const clearDistrictRoles = state.userRoleLists.filter((role) =>
        actions.payload.some((r) => r.roleid !== role.roleid)
      );
      state.userRoleLists = clearDistrictRoles;
    },
    toggleLayerRoles: (state, actions) => {
      const thisDistrict = state.userRoleLists.find(
        (role) => role["mahuyen"] === actions.payload["mahuyen"]
      );
      const layerRoles = thisDistrict["malopdulieu"];
      const hasLayer = layerRoles.includes(actions.payload["malopdulieu"]);
      hasLayer
        ? (thisDistrict["malopdulieu"] = layerRoles.filter(
            (role) => role !== actions.payload["malopdulieu"]
          ))
        : (thisDistrict["malopdulieu"] = [
            ...layerRoles,
            actions.payload["malopdulieu"],
          ]);
    },
    changeSelectAllLayers: (state, actions) => {
      const thisDistrict = state.userRoleLists.find(
        (role) => role["mahuyen"] === actions.payload["mahuyen"]
      );
      const clearLayerRoles = [];
      thisDistrict["malopdulieu"] = [
        ...clearLayerRoles,
        ...actions.payload["malopdulieu"],
      ];
    },
    unChangeSelectAllLayers: (state, actions) => {
      const thisDistrict = state.userRoleLists.find(
        (role) => role["mahuyen"] === actions.payload["mahuyen"]
      );
      const clearLayerRoles = [];
      thisDistrict["malopdulieu"] = clearLayerRoles;
    },
    toggleSelectAdminRole: (state, actions) => {
      const hasAdminRole = state.userRoleLists.find(
        (r) => r.roleid.toString() === process.env.REACT_APP_ADMINROLE
      );
      !hasAdminRole
        ? (state.userRoleLists = actions.payload)
        : (state.userRoleLists = []);
    },
    getAllMembers: (state, actions) => {
      state.allMembers = actions.payload;
    },
    getUserAccount: (state, actions) => {
      state.userAccount = actions.payload;
    },
    getUserRoles: (state, actions) => {
      state.userRoleLists = actions.payload;
    },
    setHistoryLayers: (state, actions) => {
      state.historyLayers = actions.payload;
    },
    setCurrentLayer: (state, actions) => {
      state.currentLayer = actions.payload;
    },
    getAllHistories: (state, actions) => {
      state.allHistories = actions.payload;
    },
  },
});
export const isAssignRole = (state) => state.admin.isAssignRole;
export const allRoles = (state) => state.admin.allRoles;
export const userAccount = (state) => state.admin.userAccount;
export const userRoles = (state) => state.admin.userRoles;
export const userRoleLists = (state) => state.admin.userRoleLists;
export const allMembers = (state) => state.admin.allMembers;
export const historyLayers = (state) => state.admin.historyLayers;
export const currentLayer = (state) => state.admin.currentLayer;
export const allHistories = (state) => state.admin.allHistories;

export const {
  toggleAssignRole,
  setAllRoles,
  setUserAccount,
  setUserRole,
  toggleDistrictRoles,
  changeSelectAllDistricts,
  unChangeSelectAllDistricts,
  toggleLayerRoles,
  changeSelectAllLayers,
  unChangeSelectAllLayers,
  toggleSelectAdminRole,
  getAllMembers,
  getUserAccount,
  getUserRoles,
  setHistoryLayers,
  setCurrentLayer,
  getAllHistories,
} = adminSlice.actions;
export default adminSlice.reducer;
