import "./Legend.css";
import { Fragment } from "react";
import config from "src/config";
import { img } from "src/assets";

export const LegendTab = () => {
  return (
    <Fragment>
      <img src={img.legendImg} alt="" />
      <span>Bảng chú giải</span>
    </Fragment>
  );
};

const Legend = () => {
  return (
    <div className="tab-legend">
      <div className="legend-title">
        <span>CHÚ GIẢI</span>
      </div>
      <div className="legend-container">
        <div className="temperature-legend">
          <span>Nhiệt độ</span>
          <div className="temperature-legend-color" />
          <div className="temperature-legend-label">
            {config.legends.temparature.map((item) => (
              <span key={item.legend}>{item.legend}</span>
            ))}
          </div>
        </div>
        <div className="salinity-legend">
          <span>Độ mặn</span>
          <div className="salinity-legend-color" />
          <div className="salinity-legend-label">
            {config.legends.salinity.map((item) => (
              <span key={item.legend}>{item.legend}</span>
            ))}
          </div>
        </div>
        <div className="items-legend">
          {config.legends.items.map((item) => (
            <div key={item.legend} className="item-legend">
              <img src={item.icon} alt="" />
              <span>{item.legend}</span>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
};

export default Legend;
