import { icon } from "src/assets";

const DirectionNotes = () => {
  return (
    <div className="direction-notes">
      <h3>Hướng dẫn sử dụng hệ thống:</h3>
      <span>1: Bật lớp "Hướng di chuyển sơ tán dân"</span>
      <span>2: Hiển thị lớp "Vị trí xung yếu" và "Vị trí an toàn"</span>
      <div className="direction-notes-click">
        <span>3: Nhấp chuột trái vào biểu tượng</span>
        <img src={icon.weakIcon} alt="" width="20px" height="20px" />
      </div>
      <div className="direction-notes-click">
        <span>4: Nhấp chuột phải vào biểu tượng</span>
        <img src={icon.safeIcon} alt="" width="28px" height="28px" />
      </div>
    </div>
  );
};

export default DirectionNotes;
