import { createContext, useContext, useReducer } from "react";

const initState = { toasts: [] }

const ToastStateContext = createContext(null);
const ToastDispatchContext = createContext(null);

const ToastReducer = (state, action) => {
  switch (action.type) {
    case "ADD_TOAST":
      return { ...state, toasts: [...state.toasts, action.payload] };
    case "REMOVE_TOAST":
      return {
        ...state,
        toasts: state.toasts.filter((el) => el.id !== action.payload),
      };
    default:
      throw new Error("Invalid action");
  }
};

export const ToastProvider = ({ children }) => {
  const [state, dispatch] = useReducer(ToastReducer, initState);
  return (
    <ToastStateContext.Provider value={state}>
      <ToastDispatchContext.Provider value={dispatch}>
        {children}
      </ToastDispatchContext.Provider>
    </ToastStateContext.Provider>
  );
};

export const useToastState = () => useContext(ToastStateContext);
export const useToastDispatch = () => useContext(ToastDispatchContext);

export const setAddToast = (payload) => {
  return {
    type: "ADD_TOAST",
    payload: payload,
  }
}
export const setRemoveToast = (payload) => {
  return {
    type: "REMOVE_TOAST",
    payload: payload,
  }
}