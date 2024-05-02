import "./Loading.css";
import { useSelector } from "react-redux";
import { visibleLoading } from "src/stores/global";

const Loading = () => {
  const isLoading = useSelector(visibleLoading);

  return (
    isLoading && (
      <div className="loading">
        <div className="loading-dialog">
          <div className="loading-box" />
          <div className="loading-text">Đang tải dữ liệu...</div>
        </div>
      </div>
    )
  );
};

export default Loading;
