import "./Toast.css";
import { img } from "src/assets";

const Toast = ({ type, title, message, onClose }) => {
  const toastIcons = {
    success: img.successImg,
    error: img.errorImg,
    info: img.infoImg,
  };
  const iconImg = toastIcons[type] || null;

  return (
    <div className={`toast toast--${type}`}>
      <div className="toast-icon">
        <img src={iconImg} alt="" />
      </div>
      <div className="toast-body">
        <h3 className="toast-title">{title}</h3>
        <p className="toast-message">{message}</p>
      </div>
      <div className="toast-close" onClick={onClose}>
        <img src={img.closeImg} alt="" />
      </div>
    </div>
  );
};

export default Toast;
