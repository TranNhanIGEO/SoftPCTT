import { img } from "src/assets";

const SwitchStyle = ({ onClick }) => {
  return (
    <button onClick={onClick}>
      <img src={img.changeStyleImg} alt="" />
      <span>Chế độ xem bản đồ</span>
    </button>
  );
};

export default SwitchStyle;
