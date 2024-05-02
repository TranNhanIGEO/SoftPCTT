import { img } from "src/assets";

const TogglePanel = ({ isVisible, onClick }) => {
  return (
    <button onClick={onClick}>
      <img src={isVisible ? img.closeImg : img.openImg} alt="" />
      <span>Đóng/Mở</span>
    </button>
  );
};

export default TogglePanel;
