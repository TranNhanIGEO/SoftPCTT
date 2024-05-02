import { createSlice } from "@reduxjs/toolkit";
import CryptoJS from "crypto-js";
import decodeToken from "src/utils/decodeToken";

export const initState = {
  token: null,
  refresh: null,
  uuid: null,
  error: null,
};

export const authSlice = createSlice({
  name: "auth",
  initialState: initState,
  reducers: {
    loginSuccess: (state, action) => {
      const accessToken = action.payload["at"];
      const refreshToken = action.payload["rt"];
      const realRefresh = `${refreshToken}${process.env.REACT_APP_SECRET_JWT}`;
      const hashRefresh = CryptoJS.SHA512(realRefresh);
      const hashedRefresh = hashRefresh.toString(CryptoJS.enc.Base64);
      state.token = accessToken;
      state.refresh = hashedRefresh;
      state.uuid = decodeToken(accessToken)?.["nameidentifier"];
      state.error = null;
    },
    loginFailed: (state, action) => {
      state.token = null;
      state.refresh = null;
      state.uuid = null;
      state.error = action.payload;
    },
    logoutSuccess: (state) => {
      state.token = null;
      state.refresh = null;
      state.uuid = null;
    },
    logoutFailed: (state, action) => {
      state.error = action.payload;
    },
  },
});

export const currentToken = (state) => state.auth.token;
export const currentRefresh = (state) => state.auth.refresh;
export const currentUser = (state) => state.auth.uuid;
export const errorStatus = (state) => state.auth.error;
export const { loginSuccess, loginFailed, logoutSuccess, logoutFailed } =
  authSlice.actions;
export default authSlice.reducer;
