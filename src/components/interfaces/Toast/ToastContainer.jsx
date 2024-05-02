import { Fragment } from "react";
import Toast from "./Toast";
import {
  setRemoveToast,
  useToastDispatch,
  useToastState,
} from "src/contexts/Toast";

const ToastContainer = () => {
  const { toasts } = useToastState();
  const dispatch = useToastDispatch();

  const handleCloseToast = (toastID) => {
    dispatch(setRemoveToast(toastID))
  };

  return toasts?.length ? (
    <div className="toast-container">
      {toasts?.map((toast) => (
        <Toast
          key={toast.id}
          id={toast.id}
          type={toast.type}
          title={toast.title}
          message={toast.message}
          onClose={() => handleCloseToast(toast.id)}
        />
      ))}
    </div>
  ) : (
    <Fragment />
  );
};

export default ToastContainer;
