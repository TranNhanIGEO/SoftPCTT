import "./Theme.css";
import { img } from "src/assets";

const Theme = () => {
  return (
    <div className="theme-background">
      <img
        className="theme-image"
        loading="lazy"
        role="presentation"
        fetchprioriy="high"
        alt=""
        src={img.wallpaperImg}
      />
    </div>
  );
};

export default Theme;
