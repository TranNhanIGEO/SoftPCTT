import { createContext, useContext, useReducer } from "react";

const initState = {
  visibleSimpleSearch: false,
  visibleAdvancedSearch: false,
  columnQuery: [],
  valueSuggests: {},
  whereClause: {},
};

const SearchStateContext = createContext(null);
const SearchDispatchContext = createContext(null);

const SearchReducer = (state, action) => {
  switch (action.type) {
    case "TOGGLE_SIMPLE_SEARCH":
      return { ...state, visibleSimpleSearch: action.payload };
    case "TOGGLE_ADVANCED_SEARCH":
      return { ...state, visibleAdvancedSearch: action.payload };
    case "SET_COLUMN_QUERY":
      return { ...state, columnQuery: action.payload };
    case "SET_VALUE_SUGGETS":
      return { ...state, valueSuggests: {...state.valueSuggests, ...action.payload} };
    case "RESET_VALUE_SUGGETS":
      return { ...state, valueSuggests: {} };
    case "SET_WHERE_CLAUSE":
      return { ...state, whereClause: action.payload };
    case "RESET_WHERE_CLAUSE":
      return { ...state, whereClause: {} };

    default:
      break;
  }
};

export const SearchProvider = ({ children }) => {
  const [state, dispatch] = useReducer(SearchReducer, initState);

  return (
    <SearchStateContext.Provider value={state}>
      <SearchDispatchContext.Provider value={dispatch}>
        {children}
      </SearchDispatchContext.Provider>
    </SearchStateContext.Provider>
  );
};

export const useSearchState = () => useContext(SearchStateContext);
export const useSearchDispatch = () => useContext(SearchDispatchContext);

export const setToggleSimpleSearch = (payload) => {
  return {
    type: "TOGGLE_SIMPLE_SEARCH",
    payload: payload,
  };
};
export const setToggleAdvancedSearch = (payload) => {
  return {
    type: "TOGGLE_ADVANCED_SEARCH",
    payload: payload,
  };
};
export const setColumnQuery = (payload) => {
  return {
    type: "SET_COLUMN_QUERY",
    payload: payload,
  };
};
export const setValueSuggests = (payload) => {
  return {
    type: "SET_VALUE_SUGGETS",
    payload: payload,
  };
};
export const resetValueSuggests = (payload) => {
  return {
    type: "RESET_VALUE_SUGGETS",
    payload: payload,
  };
};
export const setWhereClauses = (payload) => {
  return {
    type: "SET_WHERE_CLAUSE",
    payload: payload,
  };
};
export const resetWhereClauses = (payload) => {
  return {
    type: "RESET_WHERE_CLAUSE",
    payload: payload,
  };
};
