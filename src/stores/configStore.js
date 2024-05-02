import { combineReducers, configureStore } from "@reduxjs/toolkit";
import storage from "redux-persist/lib/storage";
import { persistReducer, persistStore } from "redux-persist";
import {
  authSlice,
  globalSlice,
  adminSlice,
  homeSlice,
  materialSlice,
  solutionSlice,
  dataSlice,
  adminState,
  globalState,
  homeState,
  materialState,
  solutionState,
  dataState,
} from "./";
import {
  FLUSH,
  REHYDRATE,
  PAUSE,
  PERSIST,
  PURGE,
  REGISTER,
} from "redux-persist";

const authPersistConfig = {
  key: "auth",
  version: 1,
  storage: storage,
  whitelist: [],
  blacklist: ["token", "refresh", "uuid", "error"],
};

const appReducer = combineReducers({
  auth: persistReducer(authPersistConfig, authSlice),
  global: globalSlice,
  admin: adminSlice,
  home: homeSlice,
  material: materialSlice,
  solution: solutionSlice,
  data: dataSlice,
});

const rootReducer = (state, action) => {
  if (action.type === "auth/logoutSuccess") {
    state.admin = adminState
    state.global = globalState
    state.home = homeState
    state.material = materialState
    state.solution = solutionState
    state.data = dataState
  }
  if (action.type === "global/setPage") {
    switch (action.payload) {
      case process.env.REACT_APP_HOMEPAGE:
        state.admin = adminState
        state.material = materialState
        state.solution = solutionState
        state.data = dataState
        break;
    
      case process.env.REACT_APP_SOLUTIONPAGE:
        state.admin = adminState
        state.home = homeState
        state.material = materialState
        state.data = dataState
        break;
    
      case process.env.REACT_APP_MATERIALPAGE:
        state.admin = adminState
        state.home = homeState
        state.solution = solutionState
        state.data = dataState
        break;
    
      case process.env.REACT_APP_ADMINPAGE:
        state.home = homeState
        state.material = materialState
        state.solution = solutionState
        state.data = dataState
        break;
    
      default:
    }
  }
  return appReducer(state, action);
};

const rootPersistConfig = {
  key: "root",
  version: 1,
  storage: storage,
  whitelist: [],
  blacklist: ["auth", "admin", "home", "material", "solution", "data"],
};

const persistedReducer = persistReducer(rootPersistConfig, rootReducer);

const middlewareStore = (getDefaultMiddleware) =>
  getDefaultMiddleware({
    serializableCheck: {
      ignoredActions: [FLUSH, REHYDRATE, PAUSE, PERSIST, PURGE, REGISTER],
      ignoredActionPaths: ["meta.arg", "payload.timestamp"],
      ignoredPaths: ["some.nested.path"],
    },
    immutableCheck: {
      ignoredPaths: ["items.data"],
    },
  });

const configStore = configureStore({
  reducer: persistedReducer,
  middleware: middlewareStore,
});

const persistor = persistStore(configStore);

export { configStore, persistor };
