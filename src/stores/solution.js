import { createSlice } from "@reduxjs/toolkit";

export const initState = {
  currentTabTable: null,
  viewRoleLists: [],
  solutionMaxID: null,
  currentDistrict: undefined,
  currentLayer: undefined,
  savedWhereClauses: null,
  statisticInfos: {},
};

export const solutionSlice = createSlice({
  name: "solution",
  initialState: initState,
  reducers: {
    setTabTable: (state, actions) => {
      state.currentTabTable = actions.payload;
    },
    setViewRoleLists: (state, actions) => {
      state.viewRoleLists = actions.payload;
    },
    setSolutionMaxID: (state, actions) => {
      state.solutionMaxID = actions.payload;
    },
    setCurrentDistrict: (state, actions) => {
      state.currentDistrict = actions.payload;
    },
    setCurrentLayer: (state, actions) => {
      state.currentLayer = actions.payload;
    },
    setSaveWhereClauses: (state, actions) => {
      state.savedWhereClauses = actions.payload;
    },
    setStatisticInfos: (state, actions) => {
      state.statisticInfos = actions.payload;
    },
  },
});

export const currentTabTable = (state) => state.solution.currentTabTable;
export const getViewRoleLists = (state) => state.solution.viewRoleLists;
export const getSolutionMaxID = (state) => state.solution.solutionMaxID;
export const currentDistrict = (state) => state.solution.currentDistrict;
export const currentLayer = (state) => state.solution.currentLayer;
export const savedWhereClauses = (state) => state.solution.savedWhereClauses;
export const statisticInfos = (state) => state.solution.statisticInfos;

export const {
  setTabTable,
  setViewRoleLists,
  setSolutionMaxID,
  setCurrentDistrict,
  setCurrentLayer,
  setSaveWhereClauses,
  setStatisticInfos,
} = solutionSlice.actions;
export default solutionSlice.reducer;
