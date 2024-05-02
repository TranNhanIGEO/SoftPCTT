import { createSlice } from "@reduxjs/toolkit";

export const initState = {
  materialLayers: [],
  materialDistricts: [],
  currentDistrict: undefined,
  currentLayer: undefined,
};

export const materialSlice = createSlice({
  name: "material",
  initialState: initState,
  reducers: {
    setAllMaterialLayers: (state, actions) => {
      state.materialLayers = actions.payload;
    },
    setAllMaterialDistricts: (state, actions) => {
      state.materialDistricts = actions.payload;
    },
    setCurrentDistrict: (state, actions) => {
      state.currentDistrict = actions.payload;
    },
    setCurrentLayer: (state, actions) => {
      state.currentLayer = actions.payload;
    },
  },
});

export const getMaterialLayers = (state) => state.material.materialLayers;
export const getMaterialDistricts = (state) => state.material.materialDistricts;
export const currentDistrict = (state) => state.material.currentDistrict;
export const currentLayer = (state) => state.material.currentLayer;

export const {
  setAllMaterialLayers,
  setAllMaterialDistricts,
  setCurrentDistrict,
  setCurrentLayer,
} = materialSlice.actions;
export default materialSlice.reducer;
