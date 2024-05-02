import { createSlice } from "@reduxjs/toolkit";

export const initState = {
  data: {},
  dataRef: {},
  statistics: [],
};

export const dataSlice = createSlice({
  name: "data",
  initialState: initState,
  reducers: {
    setData: (state, actions) => {
      state.data = { ...state.data, ...actions.payload };
    },
    setDataRef: (state, actions) => {
      state.dataRef = { ...state.dataRef, ...actions.payload };
    },
    setStatistics: (state, actions) => {
      state.statistics = actions.payload;
    },

    removeAllThematicData: (state) => {
      state.data = {};
      state.dataRef = {};
      state.statistics = [];
    },
  },
});

export const getData = (state) => state.data.data;
export const getDataRef = (state) => state.data.dataRef;
export const getStatistics = (state) => state.data.statistics;

export const { setData, setDataRef, setStatistics, removeAllThematicData } =
  dataSlice.actions;
export default dataSlice.reducer;
