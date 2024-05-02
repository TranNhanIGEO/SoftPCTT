import { createSlice } from "@reduxjs/toolkit";

export const initState = {
  visibleLoading: false,
  currentPage: null,
  countLogin: [],
  roleLists: [],
  userRoleLists: [],
};

export const globalSlice = createSlice({
  name: "global",
  initialState: initState,
  reducers: {
    toggleLoading: (state, action) => {
      state.visibleLoading = action.payload ?? !state.visibleLoading;
    },
    setPage: (state, action) => {
      state.currentPage = action.payload;
    },
    setCountLogin: (state, actions) => {
      state.countLogin = actions.payload;
    },
    setRoleLists: (state, action) => {
      state.roleLists = action.payload;
    },
    setUserRoleLists: (state, actions) => {
      state.userRoleLists = actions.payload;
    },
  },
});

export const visibleLoading = (state) => state.global.visibleLoading;
export const currentPage = (state) => state.global.currentPage;
export const getCountLogin = (state) => state.global.countLogin;
export const getRoleLists = (state) => state.global.roleLists;
export const getUserRoleLists = (state) => state.global.userRoleLists;
export const {
  toggleLoading,
  setPage,
  setCountLogin,
  setRoleLists,
  setUserRoleLists,
} = globalSlice.actions;
export default globalSlice.reducer;
