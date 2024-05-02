import { createSlice } from "@reduxjs/toolkit";

export const initState = {
  visibleCtrlPanel: true,
  visibleAttrTable: false,
  currentTabPanel: "layerTab",
  currentTabTable: null,
  mapTypes: [],
  allLayers: [],

  viewRoleLists: [],
  districtGeometry: null,
  currentDistrict: undefined,
  currentLayer: undefined,
  savedWhereClauses: null,
  currentObjCoords: null,
  directionSystem: {},
  currentRouteCoords: [],
  statisticInfos: {},

  dataMaxID: null,
  districtsData: [],
  islandData: [],
  data: {
    trafficData: [],
    waterSystemData: [],
  },
};

export const homeSlice = createSlice({
  name: "home",
  initialState: initState,
  reducers: {
    toggleControlPanel: (state, actions) => {
      state.visibleCtrlPanel = actions.payload ?? !state.visibleCtrlPanel;
    },
    toggleAttributeTable: (state, actions) => {
      state.visibleAttrTable = actions.payload ?? !state.visibleAttrTable;
    },
    setTabPanel: (state, actions) => {
      state.currentTabPanel = actions.payload;
    },
    setTabTable: (state, actions) => {
      state.currentTabTable = actions.payload;
    },
    toggleMapTypes: (state, actions) => {
      const hasThisType = state.mapTypes.includes(actions.payload);
      !hasThisType
        ? (state.mapTypes = [...state.mapTypes, actions.payload])
        : (state.mapTypes = state.mapTypes.filter(
            (type) => type !== actions.payload
          ));
    },
    toggleAllLayers: (state, actions) => {
      const hasThisLayer = state.allLayers.includes(actions.payload);
      !hasThisLayer
        ? (state.allLayers = [...state.allLayers, actions.payload])
        : (state.allLayers = state.allLayers.filter(
            (type) => type !== actions.payload
          ));
    },
    removeAllLayers: (state) => {
      state.allLayers = [];
    },
    removeAllBasicData: (state) => {
      const allData = Object.keys(state.data);
      allData.forEach((dt) => {
        state.data[dt] = [];
      });
      state.dataRef = [[]];
    },

    setViewRoleLists: (state, action) => {
      state.viewRoleLists = action.payload;
    },
    setDistrictGeometry: (state, actions) => {
      state.districtGeometry = actions.payload;
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
    setCurrentObjCoords: (state, actions) => {
      state.currentObjCoords = actions.payload;
    },
    setDirectionSystem: (state, actions) => {
      state.directionSystem = actions.payload;
    },
    setCurrentRouteCoords: (state, actions) => {
      state.currentRouteCoords = actions.payload;
    },
    setStatisticInfos: (state, actions) => {
      state.statisticInfos = actions.payload;
    },

    setDataMaxID: (state, actions) => {
      state.dataMaxID = actions.payload;
    },
    setDistrictData: (state, actions) => {
      state.districtsData = actions.payload;
    },
    setIslandData: (state, actions) => {
      state.islandData = actions.payload;
    },
    setTrafficData: (state, actions) => {
      state.data.trafficData = actions.payload;
    },
    setWaterSystemData: (state, actions) => {
      state.data.waterSystemData = actions.payload;
    },
  },
});

export const visibleCtrlPanel = (state) => state.home.visibleCtrlPanel;
export const visibleAttrTable = (state) => state.home.visibleAttrTable;
export const currentTabPanel = (state) => state.home.currentTabPanel;
export const currentTabTable = (state) => state.home.currentTabTable;
export const mapTypes = (state) => state.home.mapTypes;
export const allLayers = (state) => state.home.allLayers;

export const getViewRoleLists = (state) => state.home.viewRoleLists;
export const districtGeometry = (state) => state.home.districtGeometry;
export const currentDistrict = (state) => state.home.currentDistrict;
export const currentLayer = (state) => state.home.currentLayer;
export const savedWhereClauses = (state) => state.home.savedWhereClauses;
export const currentObjCoords = (state) => state.home.currentObjCoords;
export const directionSystem = (state) => state.home.directionSystem;
export const currentRouteCoords = (state) => state.home.currentRouteCoords;
export const statisticInfos = (state) => state.home.statisticInfos;

export const getDataMaxID = (state) => state.home.dataMaxID;
export const getDistrictData = (state) => state.home.districtsData;
export const getIslandData = (state) => state.home.islandData;
export const getTrafficData = (state) => state.home.data.trafficData;
export const getWaterSystemData = (state) => state.home.data.waterSystemData;

export const {
  toggleControlPanel,
  toggleAttributeTable,
  toggleAdvancedSearch,
  toggleSimpleSearch,
  setTabPanel,
  setTabTable,
  toggleMapTypes,
  toggleAllLayers,
  removeAllLayers,
  removeAllBasicData,

  setViewRoleLists,
  setDistrictGeometry,
  setCurrentDistrict,
  setCurrentLayer,
  setSaveWhereClauses,
  setCurrentObjCoords,
  setDirectionSystem,
  setCurrentRouteCoords,
  setStatisticInfos,

  setDataMaxID,
  setDistrictData,
  setIslandData,
  setTrafficData,
  setWaterSystemData,
} = homeSlice.actions;
export default homeSlice.reducer;
