import { v4 as uuidv4 } from "uuid";
import {
  setAddToast,
  setRemoveToast,
  useToastDispatch,
} from "src/contexts/Toast";

const useToast = (autoClose = true, delayClose = 3000) => {
  const dispatch = useToastDispatch();

  const toast = ({ type, title, message }) => {
    const [toastID] = uuidv4().split("-");
    const newToast = {
      id: toastID,
      type: type,
      title: title,
      message: message,
    };
    dispatch(setAddToast(newToast));
    if (!autoClose) return;
    setTimeout(() => dispatch(setRemoveToast(toastID)), delayClose);
  };

  const types = {
    success: ({ title, message }) => {
      return toast({ type: "success", title: title, message: message });
    },
    error: ({ title, message }) => {
      return toast({ type: "error", title: title, message: message });
    },
    info: ({ title, message }) => {
      return toast({ type: "info", title: title, message: message });
    },
  };
  
  return types;
};

export default useToast;
